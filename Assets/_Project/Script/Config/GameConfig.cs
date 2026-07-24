using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//总配置文件，包含所有宝石、敌人，弹药和装置的配置
[CreateAssetMenu(fileName = "GameConfig", menuName = "Config/GameConfig")]
public class GameConfig : ScriptableObject
{

    public  List<GemConfig> AllGems = new List<GemConfig>();
    public  List<ProjectileConfig> AllProjectiles = new List<ProjectileConfig>();
    public  List<DeviceConfig> AllDevices = new List<DeviceConfig>();
    public  List<EnemyConfig> AllEnemies = new List<EnemyConfig>();

    //单例：懒加载
    private static GameConfig _instance;
    public static GameConfig Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Resources.Load<GameConfig>("GameConfig");
                #if UNITY_EDITOR
                if (_instance == null)
                {
                    Debug.LogError("GameConfig.asset 未找到！请放置在 Resources 文件夹下");
                }
                #endif
            }
            return _instance;
        }
    }


    public GemConfig GetGemConfigById(string id)
    {
        return AllGems.Find(gem => gem.Id == id);
    }

    public ProjectileConfig GetProjectileConfigById(string id)
    {
        return AllProjectiles.Find(projectile => projectile.Id == id);
    }

    public DeviceConfig GetDeviceConfigById(string id)
    {
        return AllDevices.Find(device => device.Id == id);
    }

    public EnemyConfig GetEnemyConfigById(string id)
    {
        return AllEnemies.Find(enemy => enemy.Id == id);
    }
}