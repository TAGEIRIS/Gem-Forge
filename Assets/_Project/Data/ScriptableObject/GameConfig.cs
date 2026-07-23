using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//总配置文件，包含所有宝石、敌人，弹药和装置的配置
[CreateAssetMenu(fileName = "GameConfig", menuName = "Config/GameConfig")]
public class GameConfig : ScriptableObject
{

    public List<GemConfig> AllGems = new List<GemConfig>();
    public List<ProjectileConfig> AllProjectiles = new List<ProjectileConfig>();
    public List<DeviceConfig> AllDevices = new List<DeviceConfig>();
    public List<EnemyConfig> AllEnemies = new List<EnemyConfig>();

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

//宝石配置
[CreateAssetMenu(fileName = "GemConfig", menuName = "Config/GemConfig")]
public class GemConfig : ScriptableObject
{
    public string Id;
    public string displayName;
    public Sprite icon;
    //宝石类型
    public GemType gemType;
    public bool isActive;          // true=主动按键触发，false=被动自动触发
    public GameObject GemPrefab;
    //出售价格
    public int SellingPrice;
    //购入价格
    public int BuyingPrice;
    // 子弹配置ID
    public string GemProjectileId;
    //宝石简介
    [TextArea] public string itemInfo;
}


//弹药配置
[CreateAssetMenu(fileName = "ProjectileConfig", menuName = "Config/ProjectileConfig")]
public class ProjectileConfig : ScriptableObject
{
    public string Id;
    public string displayName;
    public Sprite icon;
    public ProjectileType projectileType;
    public GameObject ProjectilePrefab;
    //伤害值
    public int Damage;
    //射程
    public float Range;
}

//装置配置
[CreateAssetMenu(fileName = "DeviceConfig", menuName = "Config/DeviceConfig")]
public class DeviceConfig : ScriptableObject
{
    //装置编号
    public string Id;
    public string displayName;
    public Sprite icon;
    public DeviceType deviceType;


    //装置本体
    public GameObject DevicePrefab;

    //装置的运行时间
    public int Operationtime;

    //装置的输入端
    public List<string> InputGemIds = new List<string>();
    //装置的输出端
    public List<string> OutputGemIds = new List<string>();
}

[CreateAssetMenu(fileName = "EnemyConfig", menuName = "Config/EnemyConfig")]
public class EnemyConfig : ScriptableObject
{
    // 基础信息
    public string Id;
    public string displayName;
    public Sprite icon;
    public EnemyType enemyType;
    public string ProjectileId;
    public GameObject prefab;

    // 数值属性
    public int maxHp;
    public int attack;
    public float moveSpeed;
}
