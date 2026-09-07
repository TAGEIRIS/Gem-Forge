using System.Collections.Generic;
using UnityEngine;

namespace GameData
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Config/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        [Header("=== 宝石配置 ===")]
        public List<GemConfig> AllGems = new List<GemConfig>();

        [Header("=== 弹药配置 ===")]
        public List<ProjectileConfig> AllProjectiles = new List<ProjectileConfig>();

        [Header("=== 装置配置 ===")]
        public List<DeviceConfig> AllDevices = new List<DeviceConfig>();

        [Header("=== 敌人配置 ===")]
        public List<EnemyConfig> AllEnemies = new List<EnemyConfig>();

        [Header("=== 行为策略配置 ===")]
        public List<BehaviorConfig> AllBehaviors = new List<BehaviorConfig>();

        private static GameConfig _instance;
        public static GameConfig Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Resources.Load<GameConfig>("GameConfig");
                }
                return _instance;
            }
        }

        public GemConfig GetGemConfigById(string id)
        {
            return AllGems.Find(g => g.Id == id);
        }

        public ProjectileConfig GetProjectileConfigById(string id)
        {
            return AllProjectiles.Find(p => p.Id == id);
        }

        public DeviceConfig GetDeviceConfigById(string id)
        {
            return AllDevices.Find(d => d.Id == id);
        }

        public EnemyConfig GetEnemyConfigById(string id)
        {
            return AllEnemies.Find(e => e.Id == id);
        }

        public BehaviorConfig GetBehaviorConfigById(string id)
        {
            return AllBehaviors.Find(b => b.Id == id);
        }
    }
}