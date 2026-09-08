using System.Collections;
using UnityEngine;
using GameData;

public class LevelManager : MonoBehaviour
{
    [Header("配置")]
    [SerializeField] private string currentMapId = "Meadow_01";
    [SerializeField] private WaveConfig currentWaveConfig;

    [Header("组件引用")]
    [SerializeField] private WaveSpawner waveSpawner;
    [SerializeField] private MapLoader mapLoader;

    [Header("玩家")]
    [SerializeField] private GameObject player;
    [SerializeField] private PlayerHealth playerHealth;

    [Header("状态")]
    [SerializeField] private float survivalTimer = 0f;
    [SerializeField] private float totalSurvivalTime = 180f;  // 3分钟
    [SerializeField] private bool isLevelActive = false;

    private static LevelManager _instance;
    public static LevelManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<LevelManager>();
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

        // 自动查找组件
        if (waveSpawner == null)
            waveSpawner = FindObjectOfType<WaveSpawner>();

        if (mapLoader == null)
            mapLoader = MapLoader.Instance;

        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");

        if (playerHealth == null && player != null)
            playerHealth = player.GetComponent<PlayerHealth>();

        // 从 WaveConfig 读取生存时间
        if (currentWaveConfig != null)
        {
            totalSurvivalTime = currentWaveConfig.totalSurvivalTime;
        }
    }

    private void Start()
    {
        StartLevel();
    }

    private void Update()
    {
        if (!isLevelActive) return;

        // 生存倒计时
        survivalTimer += Time.deltaTime;

        // 检查是否胜利
        if (survivalTimer >= totalSurvivalTime)
        {
            OnVictory();
        }
    }

    /// <summary>
    /// 开始关卡
    /// </summary>
    private void StartLevel()
    {
        Debug.Log("[LevelManager] 开始关卡");

        // 1. 加载地图
        mapLoader.LoadMap(currentMapId);

        // 2. 设置玩家位置（在地图中心偏下位置）
        if (player != null)
        {
            Bounds bounds = mapLoader.GetCurrentBounds();
            if (bounds.size != Vector3.zero)
            {
                Vector3 playerPos = new Vector3(bounds.center.x, bounds.min.y + 2f, 0f);
                player.transform.position = playerPos;
                mapLoader.SetPlayerTransform(player.transform);
            }
        }

        // 3. 重置计时器
        survivalTimer = 0f;
        isLevelActive = true;

        // 4. 开始波次生成
        if (waveSpawner != null && currentWaveConfig != null)
        {
            waveSpawner.StartSpawning(currentWaveConfig);
            waveSpawner.OnAllWavesComplete += OnAllWavesComplete;
        }
        else
        {
            Debug.LogWarning("[LevelManager] WaveSpawner 或 WaveConfig 未配置");
        }

        // 5. 订阅玩家死亡事件
        if (playerHealth != null)
        {
            playerHealth.OnDeath += OnPlayerDeath;
        }
    }

    /// <summary>
    /// 所有波次完成
    /// </summary>
    private void OnAllWavesComplete()
    {
        Debug.Log("[LevelManager] 所有波次已完成");
        // 如果设计为“清完怪才赢”，可以在这里触发胜利
        // 目前我们的设计是“生存3分钟”，所以继续等倒计时
    }

    /// <summary>
    /// 玩家死亡
    /// </summary>
    private void OnPlayerDeath()
    {
        if (!isLevelActive) return;
        OnDefeat();
    }

    /// <summary>
    /// 胜利
    /// </summary>
    private void OnVictory()
    {
        if (!isLevelActive) return;
        isLevelActive = false;

        waveSpawner?.StopSpawning();
        Debug.Log("[LevelManager] 胜利！生存时间到");

        // 调用 GameManager 上报胜利
        GameManager.Instance?.EndBattle(true);
    }

    /// <summary>
    /// 失败
    /// </summary>
    private void OnDefeat()
    {
        if (!isLevelActive) return;
        isLevelActive = false;

        waveSpawner?.StopSpawning();
        Debug.Log("[LevelManager] 失败！玩家死亡");

        // 调用 GameManager 上报失败
        GameManager.Instance?.EndBattle(false);
    }

    /// <summary>
    /// 获取生存进度（0~1）
    /// </summary>
    public float GetSurvivalProgress()
    {
        return Mathf.Clamp01(survivalTimer / totalSurvivalTime);
    }

    /// <summary>
    /// 获取剩余时间（秒）
    /// </summary>
    public float GetRemainingTime()
    {
        return Mathf.Max(0f, totalSurvivalTime - survivalTimer);
    }

    private void OnDestroy()
    {
        if (waveSpawner != null)
        {
            waveSpawner.OnAllWavesComplete -= OnAllWavesComplete;
        }
        if (playerHealth != null)
        {
            playerHealth.OnDeath -= OnPlayerDeath;
        }
    }
}