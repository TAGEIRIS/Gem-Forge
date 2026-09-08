using System.Collections.Generic;
using UnityEngine;

public class MapLoader : MonoBehaviour
{
    [Header("地图挂载点")]
    [SerializeField] private Transform mapSpawnPoint;

    [Header("玩家引用（用于安全距离）")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float safeDistance = 5f;

    [Header("地图映射表（在 Inspector 中拖拽配置）")]
    [SerializeField] private List<MapEntry> mapEntries = new List<MapEntry>();

    [Header("运行时状态")]
    [SerializeField] private GameObject currentMap;
    [SerializeField] private string currentMapId;
    [SerializeField] private Bounds currentMapBounds;



    // 运行时字典缓存
    private Dictionary<string, GameObject> mapPrefabDict = new Dictionary<string, GameObject>();

    private static MapLoader _instance;
    public static MapLoader Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<MapLoader>();
                if (_instance == null)
                {
                    GameObject go = new GameObject("MapLoader");
                    _instance = go.AddComponent<MapLoader>();
                }
            }
            return _instance;
        }
    }

    [System.Serializable]
    public class MapEntry
    {
        public string mapId;            // 地图唯一ID，如 "Meadow_01"
        public GameObject mapPrefab;    // 地图预制体
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;

        // 构建字典缓存
        mapPrefabDict.Clear();
        foreach (var entry in mapEntries)
        {
            if (!string.IsNullOrEmpty(entry.mapId) && entry.mapPrefab != null)
            {
                mapPrefabDict[entry.mapId] = entry.mapPrefab;
            }
        }

        // 如果没有挂载点，自动创建
        if (mapSpawnPoint == null)
        {
            GameObject spawnPoint = new GameObject("MapSpawnPoint");
            spawnPoint.transform.SetParent(transform);
            spawnPoint.transform.localPosition = Vector3.zero;
            mapSpawnPoint = spawnPoint.transform;
        }

        // 如果没有玩家引用，尝试自动查找
        if (playerTransform == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null) playerTransform = player.transform;
        }
    }

    /// <summary>
    /// 加载指定地图
    /// </summary>
    public void LoadMap(string mapId)
    {
        if (string.IsNullOrEmpty(mapId))
        {
            Debug.LogError("[MapLoader] 地图ID为空");
            return;
        }

        // 如果已经加载了相同地图，不重复加载
        if (currentMap != null && currentMapId == mapId)
        {
            Debug.Log($"[MapLoader] 地图 {mapId} 已加载，跳过");
            return;
        }

        // 销毁当前地图
        UnloadCurrentMap();

        // 检查字典中是否存在
        if (!mapPrefabDict.ContainsKey(mapId))
        {
            Debug.LogError($"[MapLoader] 未找到地图ID: {mapId}");
            return;
        }

        GameObject prefab = mapPrefabDict[mapId];

        // 实例化地图
        currentMap = Instantiate(prefab, mapSpawnPoint);
        currentMap.transform.localPosition = Vector3.zero;
        currentMapId = mapId;

        // 计算地图边界（优先使用 Collider2D，其次 SpriteRenderer，最后 Renderer）
        Bounds? bounds = CalculateMapBounds(currentMap);
        if (bounds.HasValue)
        {
            currentMapBounds = bounds.Value;
            Debug.Log($"[MapLoader] 地图 {mapId} 加载完成，边界: {currentMapBounds}");
        }
        else
        {
            // 如果没有边界，生成一个默认边界（以地图中心为中心，半径 10 单位）
            currentMapBounds = new Bounds(mapSpawnPoint.position, new Vector3(20f, 20f, 0f));
            Debug.LogWarning($"[MapLoader] 地图 {mapId} 无边界组件，使用默认边界");
        }
    }

    /// <summary>
    /// 卸载当前地图
    /// </summary>
    public void UnloadCurrentMap()
    {
        if (currentMap != null)
        {
            Destroy(currentMap);
            currentMap = null;
            currentMapId = string.Empty;
            currentMapBounds = new Bounds();
            Debug.Log("[MapLoader] 地图已卸载");
        }
    }

    /// <summary>
    /// 计算地图边界
    /// </summary>
    private Bounds? CalculateMapBounds(GameObject mapObject)
    {
        // 优先使用 Collider2D 的边界
        Collider2D collider = mapObject.GetComponent<Collider2D>();
        if (collider != null)
        {
            return collider.bounds;
        }

        // 其次使用 SpriteRenderer 的边界
        SpriteRenderer spriteRenderer = mapObject.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            return spriteRenderer.bounds;
        }

        // 再其次使用 Renderer 的边界
        Renderer renderer = mapObject.GetComponent<Renderer>();
        if (renderer != null)
        {
            return renderer.bounds;
        }
        return null;
    }

    /// <summary>
    /// 获取随机生成位置（在地图边缘，且远离玩家）
    /// </summary>
    public Vector3 GetRandomSpawnPosition()
    {
        if (currentMap == null)
        {
            Debug.LogWarning("[MapLoader] 当前没有加载的地图");
            return Vector3.zero;
        }

        Bounds bounds = currentMapBounds;

        // 安全距离：生成的敌人距离玩家至少 5 个单位
        safeDistance = 5f;
        Vector3 playerPos = playerTransform != null ? playerTransform.position : Vector3.zero;

        // 最多尝试 50 次，防止死循环
        for (int attempt = 0; attempt < 50; attempt++)
        {
            // 在地图范围内随机生成点（边缘优先）
            Vector3 randomPoint = GetRandomPointOnBoundsEdge(bounds);

            // 检查是否远离玩家
            if (Vector3.Distance(randomPoint, playerPos) >= safeDistance)
            {
                return randomPoint;
            }
        }

        // 如果 50 次都找不到安全位置，返回地图中心（保底）
        Debug.LogWarning("[MapLoader] 未能找到远离玩家的生成点，使用地图中心");
        return bounds.center;
    }

    /// <summary>
    /// 在地图边界上随机取点（边缘优先）
    /// </summary>
    private Vector3 GetRandomPointOnBoundsEdge(Bounds bounds)
    {
        // 随机选择四条边之一
        int edge = Random.Range(0, 4);
        float x = 0f, y = 0f;

        switch (edge)
        {
            case 0: // 上边
                x = Random.Range(bounds.min.x, bounds.max.x);
                y = bounds.max.y;
                break;
            case 1: // 下边
                x = Random.Range(bounds.min.x, bounds.max.x);
                y = bounds.min.y;
                break;
            case 2: // 左边
                x = bounds.min.x;
                y = Random.Range(bounds.min.y, bounds.max.y);
                break;
            case 3: // 右边
                x = bounds.max.x;
                y = Random.Range(bounds.min.y, bounds.max.y);
                break;
        }

        return new Vector3(x, y, 0f);
    }

    /// <summary>
    /// 获取当前地图边界
    /// </summary>
    public Bounds GetCurrentBounds()
    {
        return currentMapBounds;
    }

    /// <summary>
    /// 获取当前地图ID
    /// </summary>
    public string GetCurrentMapId()
    {
        return currentMapId;
    }

    /// <summary>
    /// 设置玩家引用（运行时动态设置）
    /// </summary>
    public void SetPlayerTransform(Transform player)
    {
        playerTransform = player;
    }
}