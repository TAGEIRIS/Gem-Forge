using System.Collections;
using System.Collections.Generic;
using Unity.Profiling.LowLevel.Unsafe;
using UnityEngine;

[System.Serializable]
public class GameSaveData
{
    public CrossSaveData crossData = new CrossSaveData();   // 第1层：跨存档
    public RunSaveData runData = new RunSaveData();         // 第2层：单局
    public BattleSaveData battleData = new BattleSaveData(); // 第3层：战斗
}


// ========================================
// 第1层：跨存档数据
// ========================================
[System.Serializable]
public class CrossSaveData
{
    [Header("=== 永久解锁 ===")]
    public List<string> permanentlyUnlockedGems = new List<string>();
    public List<string> permanentlyUnlockedDevices = new List<string>();

    [Header("=== 成就/统计 ===")]
    public List<string> unlockedEndings = new List<string>();
    public int totalPlayCount;
    public int totalDaysSurvived;
    public float totalPlayTime;
}

// ========================================
// 第2层：单局游戏数据（15天)
// ========================================
[System.Serializable]
public class RunSaveData
{
    [Header("=== 进度 ===")]
    public int currentDay = 1;
    public bool isNight;
    public string currentMap;

    [Header("=== 资源（全部以宝石形式存在） ===")]
    public List<GemOwnership> ownedGems = new List<GemOwnership>();      // 所有宝石（数量）
    public List<string> ownedDevices = new List<string>();               // 已拥有的装置

    [Header("=== 装备配置 ===")]
    public List<string> equippedGems = new List<string>();               // 身上装备的宝石
    public List<OperateDevice> equippedDevices = new List<OperateDevice>(); // 运行中的装置

    [Header("=== 解锁进度（本局） ===")]
    public List<string> unlockedGemsThisRun = new List<string>();
    public List<string> unlockedDevicesThisRun = new List<string>();
}


// ========================================
// 第3层：当前战斗数据（夜晚临时）
// ========================================
[System.Serializable]
public class BattleSaveData
{
    [Header("=== 战斗状态 ===")]
    public int currentHp;                         // 当前血量
    public int maxHp;                             // 最大血量
    public int currentDefense;                    // 当前防御力

    [Header("=== 本晚收益 ===")]
    public List<string> tempGems = new List<string>();                  // 今晚临时获得宝石

    [Header("=== 战斗记录 ===")]
    public int enemiesKilled;                     // 本晚击杀数
    public float battleTime;                      // 本晚战斗时间
}


[System.Serializable]
public class GemOwnership
{
    public string gemId;
    public int count;                            // 拥有数量
}

[System.Serializable]

public class OperateDevice
{
    public string deviceId;
    public int Operationtime;                      //剩余运行时间
}