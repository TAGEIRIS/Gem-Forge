using System.Collections.Generic;
using UnityEngine;
using GameData;

public class EnemyPool : MonoBehaviour
{
    [Header("池容量配置")]
    [SerializeField] private int defaultPoolSize = 5;      // 每种类型默认预创建数量
    [SerializeField] private int maxPoolSize = 20;         // 每种类型最大池容量

    // Prefab缓存：敌人类型ID → Prefab
    private Dictionary<string, GameObject> prefabCache = new Dictionary<string, GameObject>();

    // 对象池：敌人类型ID → 空闲实例队列
    private Dictionary<string, Queue<GameObject>> pools = new Dictionary<string, Queue<GameObject>>();

    // 活跃实例追踪（用于调试和回收）
    private Dictionary<GameObject, string> activeInstances = new Dictionary<GameObject, string>();

    private static EnemyPool _instance;
    public static EnemyPool Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<EnemyPool>();
                if (_instance == null)
                {
                    GameObject go = new GameObject("EnemyPool");
                    _instance = go.AddComponent<EnemyPool>();
                    DontDestroyOnLoad(go);
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // ==================== 方案A：Preload 一次性加载 ====================

    /// <summary>
    /// 预加载本关所有敌人类型
    /// 在 LevelManager 加载地图时调用
    /// </summary>
    public void PreloadEnemies(List<string> enemyIds)
    {
        if (enemyIds == null || enemyIds.Count == 0) return;

        foreach (string id in enemyIds)
        {
            if (string.IsNullOrEmpty(id)) continue;
            if (prefabCache.ContainsKey(id)) continue;

            // 从 GameConfig 加载 Prefab
            EnemyConfig config = GameConfig.Instance.GetEnemyConfigById(id);
            if (config == null || config.prefab == null)
            {
                Debug.LogWarning($"[EnemyPool] 未找到敌人配置或Prefab: {id}");
                continue;
            }

            prefabCache[id] = config.prefab;

            // 初始化该类型的池子
            if (!pools.ContainsKey(id))
            {
                pools[id] = new Queue<GameObject>();
            }

            // 预热：预创建 defaultPoolSize 个实例
            for (int i = 0; i < defaultPoolSize; i++)
            {
                GameObject instance = CreateNewEnemy(id);
                if (instance != null)
                {
                    instance.SetActive(false);
                    pools[id].Enqueue(instance);
                }
            }

            Debug.Log($"[EnemyPool] 预加载敌人类型: {id}，预创建 {defaultPoolSize} 个实例");
        }
    }

    // ==================== 生成与回收 ====================

    /// <summary>
    /// 请求一个敌人实例
    /// </summary>
    public GameObject RequestEnemy(string enemyId, Vector3 position)
    {
        if (string.IsNullOrEmpty(enemyId))
        {
            Debug.LogError("[EnemyPool] 敌人ID为空");
            return null;
        }

        // 确保缓存中有该类型
        if (!prefabCache.ContainsKey(enemyId))
        {
            // 如果没预加载，尝试现场加载
            EnemyConfig config = GameConfig.Instance.GetEnemyConfigById(enemyId);
            if (config == null || config.prefab == null)
            {
                Debug.LogError($"[EnemyPool] 无法加载敌人类型: {enemyId}");
                return null;
            }
            prefabCache[enemyId] = config.prefab;
            pools[enemyId] = new Queue<GameObject>();
        }

        Queue<GameObject> pool = pools[enemyId];
        GameObject instance = null;

        // 从池中取一个空闲实例
        while (pool.Count > 0)
        {
            instance = pool.Dequeue();
            if (instance != null)
            {
                break;
            }
        }

        // 池中没有可用实例，新建一个
        if (instance == null)
        {
            // 检查是否超过最大容量
            int currentCount = GetActiveCount(enemyId);
            if (currentCount >= maxPoolSize)
            {
                Debug.LogWarning($"[EnemyPool] {enemyId} 活跃实例已达上限 {maxPoolSize}，强制复用最后一个");
                // 从池中拿一个旧实例（如果有的话）
                if (pool.Count > 0)
                {
                    instance = pool.Dequeue();
                }
                else
                {
                    // 极端情况：池空且达到上限，被迫新建（不做严格限制）
                    instance = CreateNewEnemy(enemyId);
                }
            }
            else
            {
                instance = CreateNewEnemy(enemyId);
            }
        }

        if (instance == null)
        {
            Debug.LogError($"[EnemyPool] 无法创建敌人: {enemyId}");
            return null;
        }

        // 重置状态
        ResetEnemy(instance, enemyId, position);

        // 装配策略
        ApplyBehaviors(instance, enemyId);

        // 激活
        instance.SetActive(true);

        // 记录活跃实例
        activeInstances[instance] = enemyId;

        return instance;
    }

    /// <summary>
    /// 回收敌人实例
    /// </summary>
    public void ReturnEnemy(GameObject enemyInstance)
    {
        if (enemyInstance == null) return;

        // 从活跃追踪中移除
        if (activeInstances.ContainsKey(enemyInstance))
        {
            string enemyId = activeInstances[enemyInstance];
            activeInstances.Remove(enemyInstance);

            // 停用并重置
            enemyInstance.SetActive(false);
            enemyInstance.transform.position = Vector3.zero;

            // 清除策略（方案A：回收时销毁策略）
            BehaviorContainer container = enemyInstance.GetComponent<BehaviorContainer>();
            if (container != null)
            {
                container.ClearBehaviors();
            }

            // 放回池中
            if (pools.ContainsKey(enemyId))
            {
                pools[enemyId].Enqueue(enemyInstance);
            }
            else
            {
                // 安全兜底
                Debug.LogWarning($"[EnemyPool] 回收时找不到类型 {enemyId} 的池子");
                Destroy(enemyInstance);
            }
        }
        else
        {
            // 如果实例不在追踪中，直接销毁
            Destroy(enemyInstance);
        }
    }

    /// <summary>
    /// 清空所有池子（关卡切换时调用）
    /// </summary>
    public void ClearAll()
    {
        // 销毁所有池中实例
        foreach (var kvp in pools)
        {
            foreach (GameObject instance in kvp.Value)
            {
                if (instance != null) Destroy(instance);
            }
            kvp.Value.Clear();
        }
        pools.Clear();

        // 销毁所有活跃实例
        foreach (var kvp in activeInstances)
        {
            if (kvp.Key != null) Destroy(kvp.Key);
        }
        activeInstances.Clear();

        // 保留缓存（Preload 不用重复加载）
        // 如果也要清缓存，取消下面注释：
        // prefabCache.Clear();

        Debug.Log("[EnemyPool] 已清空所有池子");
    }

    /// <summary>
    /// 完全清空（包括缓存），用于场景完全卸载
    /// </summary>
    public void ClearAllWithCache()
    {
        ClearAll();
        prefabCache.Clear();
    }

    // ==================== 私有辅助方法 ====================

    /// <summary>
    /// 创建一个新的敌人实例（不激活）
    /// </summary>
    private GameObject CreateNewEnemy(string enemyId)
    {
        if (!prefabCache.ContainsKey(enemyId)) return null;

        GameObject prefab = prefabCache[enemyId];
        GameObject instance = Instantiate(prefab);
        instance.name = $"{enemyId}_Enemy";

        // 确保有 BehaviorContainer
        if (instance.GetComponent<BehaviorContainer>() == null)
        {
            instance.AddComponent<BehaviorContainer>();
        }

        return instance;
    }

    /// <summary>
    /// 重置敌人状态（位置、血量、速度等）
    /// </summary>
    private void ResetEnemy(GameObject instance, string enemyId, Vector3 position)
    {
        instance.transform.position = position;
        instance.transform.rotation = Quaternion.identity;

        // 重置敌人的基础属性
        EnemyBase enemyBase = instance.GetComponent<EnemyBase>();
        if (enemyBase != null)
        {
            EnemyConfig config = GameConfig.Instance.GetEnemyConfigById(enemyId);
            if (config != null)
            {
                enemyBase.ResetToConfig(config);
            }
        }

        // 重置其他组件状态（如 Rigidbody）
        Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
    }

    /// <summary>
    /// 装配策略
    /// </summary>
    private void ApplyBehaviors(GameObject instance, string enemyId)
    {
        BehaviorContainer container = instance.GetComponent<BehaviorContainer>();
        if (container == null) return;

        // 策略在回收时已被 ClearBehaviors 清理，所以直接添加即可
        List<IBehavior> behaviors = BehaviorFactory.GetEnemyBehaviors(enemyId);
        if (behaviors != null && behaviors.Count > 0)
        {
            container.AddBehaviors(behaviors);
        }
    }

    /// <summary>
    /// 获取某类型的活跃实例数量
    /// </summary>
    private int GetActiveCount(string enemyId)
    {
        int count = 0;
        foreach (var kvp in activeInstances)
        {
            if (kvp.Value == enemyId && kvp.Key != null && kvp.Key.activeSelf)
            {
                count++;
            }
        }
        return count;
    }

    // ==================== 调试与工具 ====================

    /// <summary>
    /// 获取池状态信息
    /// </summary>
    public string GetPoolInfo()
    {
        string info = "=== EnemyPool 状态 ===\n";
        info += $"缓存类型数: {prefabCache.Count}\n";
        info += $"活跃实例数: {activeInstances.Count}\n";
        foreach (var kvp in pools)
        {
            info += $"  {kvp.Key}: 空闲 {kvp.Value.Count}，活跃 {GetActiveCount(kvp.Key)}\n";
        }
        return info;
    }

    private void OnDestroy()
    {
        ClearAllWithCache();
    }
}