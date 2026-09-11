using System.Collections.Generic;
using UnityEngine;
using GameData;

public class SingleGemWeapon : MonoBehaviour
{
    [Header("运行时状态（由策略读写）")]
    [SerializeField] private Vector2 currentAimDirection = Vector2.right;
    [SerializeField] private float currentCooldown = 0f;
    [SerializeField] private bool isReadyToFire = true;

    [Header("内部引用")]
    [SerializeField] private Transform firePoint;

    // 当前宝石信息
    private string gemId;
    private GemConfig gemConfig;
    private GameObject bulletPrefab;

    // 策略容器
    private BehaviorContainer container;

    private void Awake()
    {
        container = GetComponent<BehaviorContainer>();
        if (container == null)
        {
            container = gameObject.AddComponent<BehaviorContainer>();
        }

        if (firePoint == null)
        {
            firePoint = transform;
        }
    }

    /// <summary>
    /// 初始化单宝石武器（由 GemWeapon 调用）
    /// </summary>
    public void Initialize(string id, GemConfig config)
    {
        gemId = id;
        gemConfig = config;

        // 从配置读取子弹预制体
        if (!string.IsNullOrEmpty(gemConfig.GemBulletId))
        {
            BulletConfig projConfig = GameConfig.Instance.GetBulletConfigById(gemConfig.GemBulletId);
            if (projConfig != null && projConfig.bulletPrefab != null)
            {
                bulletPrefab = projConfig.bulletPrefab;
            }
            else
            {
                Debug.LogWarning($"[SingleGemWeapon] 宝石 {gemId} 的子弹配置未找到或Prefab为空");
            }
        }
        // 如果 GembulletId 为空，则该宝石不发射子弹（如回血宝石）

        // 装配宝石自身的策略（瞄准、开火等）
        List<IBehavior> behaviors = BehaviorFactory.GetGemBehaviors(gemId);
        if (behaviors != null && behaviors.Count > 0)
        {
            container.AddBehaviors(behaviors);
        }

        Debug.Log($"[SingleGemWeapon] 宝石 {gemId} 初始化完成，装配了 {behaviors?.Count ?? 0} 个策略，子弹: {(bulletPrefab != null ? bulletPrefab.name : "无")}");
    }

    // ==================== 供策略调用的公共方法 ====================

    /// <summary>
    /// 发射子弹（由 FireBehavior 调用）
    /// </summary>
    public void Fire()
    {
        // 不发射子弹的宝石（如回血宝石）直接返回
        if (bulletPrefab == null)
        {
            return;
        }

        if (!isReadyToFire) return;

        // 1. 实例化子弹
        GameObject bulletObj = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Bullet bullet = bulletObj.GetComponent<Bullet>();
        if (bullet == null)
        {
            Debug.LogError("[SingleGemWeapon] 子弹预制体缺少 Bullet 组件");
            Destroy(bulletObj);
            return;
        }

        // 2. 从武器容器中筛选出"可附着到子弹"的策略
        List<IBehavior> bulletBehaviors = new List<IBehavior>();
        var allBehaviors = container.GetBehaviors();
        foreach (var behavior in allBehaviors)
        {
            if (behavior is IBulletBehavior)
            {
                bulletBehaviors.Add(behavior);
            }
        }

        // 3. 初始化子弹
        bullet.Initialize(currentAimDirection, bulletBehaviors);

        Debug.Log($"[SingleGemWeapon] 宝石 {gemId} 发射子弹，方向 {currentAimDirection}，携带 {bulletBehaviors.Count} 个子弹策略");
    }

    /// <summary>
    /// 获取当前瞄准方向
    /// </summary>
    public Vector2 GetAimDirection() => currentAimDirection;

    /// <summary>
    /// 设置瞄准方向（由 AimBehavior 调用）
    /// </summary>
    public void SetAimDirection(Vector2 direction)
    {
        currentAimDirection = direction.normalized;
    }

    /// <summary>
    /// 获取冷却
    /// </summary>
    public float GetCooldown() => currentCooldown;

    /// <summary>
    /// 设置冷却
    /// </summary>
    public void SetCooldown(float value)
    {
        currentCooldown = value;
        isReadyToFire = currentCooldown <= 0f;
    }

    /// <summary>
    /// 是否可发射
    /// </summary>
    public bool IsReadyToFire() => isReadyToFire;

    /// <summary>
    /// 获取发射点
    /// </summary>
    public Transform GetFirePoint() => firePoint;

    /// <summary>
    /// 获取当前宝石ID
    /// </summary>
    public string GetGemId() => gemId;

    /// <summary>
    /// 获取当前宝石配置
    /// </summary>
    public GemConfig GetGemConfig() => gemConfig;

    /// <summary>
    /// 获取子弹预制体
    /// </summary>
    public GameObject GetBulletPrefab() => bulletPrefab;

    /// <summary>
    /// 停止武器（玩家死亡/胜利时调用）
    /// </summary>
    public void Stop()
    {
        // 清除所有策略，停止一切行为
        container.ClearBehaviors();
    }
}