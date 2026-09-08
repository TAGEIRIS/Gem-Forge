using System.Collections.Generic;
using UnityEngine;

namespace GameData
{
    [CreateAssetMenu(fileName = "WaveConfig", menuName = "Config/WaveConfig")]
    public class WaveConfig : ScriptableObject
    {
        public string levelId;                          // 关卡ID，如 "Level_01"
        public float totalSurvivalTime = 180f;          // 该关卡总生存时间（秒），默认3分钟
        public List<Wave> waves = new List<Wave>();     // 波次列表
    }

    [System.Serializable]
    public class Wave
    {
        public int waveIndex;                           // 波次序号（从0开始）
        public float duration;                          // 该波次持续时长（秒）
        public List<SpawnEntry> enemySpawnEntries = new List<SpawnEntry>();
    }

    [System.Serializable]
    public class SpawnEntry
    {
        public string enemyId;                          // 引用 EnemyConfig 的ID
        public float spawnInterval;                     // 每个敌人的生成间隔（秒）
        public int totalCount;                          // 该类型敌人总共生成的数量
    }
}