using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameData;

public class WaveSpawner : MonoBehaviour
{
    [Header("配置")]
    [SerializeField] private WaveConfig currentWaveConfig;

    [Header("状态（只读）")]
    [SerializeField] private int currentWaveIndex = 0;
    [SerializeField] private float waveTimer = 0f;
    [SerializeField] private bool isSpawning = false;

    // 每个波次中，每个 SpawnEntry 的独立计时器
    private List<float> entryTimers = new List<float>();
    private List<int> entrySpawnedCounts = new List<int>();
    private List<bool> entryCompleted = new List<bool>();

    // 事件：当所有波次完成时触发
    public System.Action OnAllWavesComplete;

    private void Update()
    {
        if (!isSpawning || currentWaveConfig == null) return;

        // 检查是否所有波次已完成
        if (currentWaveIndex >= currentWaveConfig.waves.Count)
        {
            isSpawning = false;
            OnAllWavesComplete?.Invoke();
            return;
        }

        Wave currentWave = currentWaveConfig.waves[currentWaveIndex];

        // 更新波次计时
        waveTimer += Time.deltaTime;

        // 检查是否该进入下一波
        if (waveTimer >= currentWave.duration)
        {
            AdvanceToNextWave();
            return;
        }

        // 处理当前波次的所有生成条目
        for (int i = 0; i < currentWave.enemySpawnEntries.Count; i++)
        {
            if (entryCompleted[i]) continue;

            SpawnEntry entry = currentWave.enemySpawnEntries[i];

            // 检查是否已达到生成总数
            if (entrySpawnedCounts[i] >= entry.totalCount)
            {
                entryCompleted[i] = true;
                continue;
            }

            // 更新该条目的计时器
            entryTimers[i] += Time.deltaTime;

            // 检查是否该生成
            if (entryTimers[i] >= entry.spawnInterval)
            {
                entryTimers[i] = 0f;
                SpawnEnemy(entry.enemyId);
                entrySpawnedCounts[i]++;
            }
        }
    }

    /// <summary>
    /// 开始生成（由 LevelManager 调用）
    /// </summary>
    public void StartSpawning(WaveConfig config)
    {
        if (config == null)
        {
            Debug.LogError("[WaveSpawner] WaveConfig 为空");
            return;
        }

        currentWaveConfig = config;
        currentWaveIndex = 0;
        waveTimer = 0f;
        isSpawning = true;

        // 重置条目追踪数据
        ResetEntryTracking();

        Debug.Log($"[WaveSpawner] 开始生成，共 {config.waves.Count} 波");
    }

    /// <summary>
    /// 停止生成（由 LevelManager 调用，玩家死亡或胜利时）
    /// </summary>
    public void StopSpawning()
    {
        isSpawning = false;
        Debug.Log("[WaveSpawner] 停止生成");
    }

    /// <summary>
    /// 进入下一波
    /// </summary>
    private void AdvanceToNextWave()
    {
        currentWaveIndex++;
        waveTimer = 0f;

        if (currentWaveIndex >= currentWaveConfig.waves.Count)
        {
            isSpawning = false;
            OnAllWavesComplete?.Invoke();
            Debug.Log("[WaveSpawner] 所有波次已完成");
            return;
        }

        ResetEntryTracking();
        Debug.Log($"[WaveSpawner] 进入第 {currentWaveIndex + 1} 波");
    }

    /// <summary>
    /// 重置波次条目追踪数据
    /// </summary>
    private void ResetEntryTracking()
    {
        if (currentWaveIndex >= currentWaveConfig.waves.Count) return;

        Wave currentWave = currentWaveConfig.waves[currentWaveIndex];
        int entryCount = currentWave.enemySpawnEntries.Count;

        entryTimers.Clear();
        entrySpawnedCounts.Clear();
        entryCompleted.Clear();

        for (int i = 0; i < entryCount; i++)
        {
            entryTimers.Add(0f);
            entrySpawnedCounts.Add(0);
            entryCompleted.Add(false);
        }
    }

    /// <summary>
    /// 生成一个敌人
    /// </summary>
    private void SpawnEnemy(string enemyId)
    {
        Vector3 spawnPos = MapLoader.Instance.GetRandomSpawnPosition();

        if (spawnPos == Vector3.zero)
        {
            Debug.LogWarning("[WaveSpawner] 无法获取生成位置");
            return;
        }

        GameObject enemy = EnemyPool.Instance.RequestEnemy(enemyId, spawnPos);

        if (enemy != null)
        {
            Debug.Log($"[WaveSpawner] 生成敌人: {enemyId} 于 {spawnPos}");
        }
    }

    /// <summary>
    /// 获取当前进度（供 UI 显示）
    /// </summary>
    public float GetProgress()
    {
        if (currentWaveConfig == null || currentWaveConfig.waves.Count == 0) return 0f;
        return (float)(currentWaveIndex + 1) / currentWaveConfig.waves.Count;
    }

    /// <summary>
    /// 获取当前波次信息（供 UI 显示）
    /// </summary>
    public string GetWaveInfo()
    {
        if (currentWaveConfig == null) return "无波次";
        return $"波次 {currentWaveIndex + 1}/{currentWaveConfig.waves.Count}";
    }
}