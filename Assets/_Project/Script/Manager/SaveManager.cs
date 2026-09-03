using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// ========================================
// SaveManager - 存档管理单例
// ========================================

public class SaveManager : MonoBehaviour
{
    // ===== 单例 =====
    private static SaveManager _instance;
    public static SaveManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SaveManager>();
                if (_instance == null)
                {
                    GameObject go = new GameObject("SaveManager");
                    _instance = go.AddComponent<SaveManager>();
                    DontDestroyOnLoad(go);
                }
            }
            return _instance;
        }
    }

    // ===== 数据 =====
    private GameSaveData _saveData;
    public GameSaveData SaveData => _saveData;

    // ===== 事件 =====
    public event Action OnGameLoaded;
    public event Action OnGameSaved;
    public event Action OnSaveReset;

    // ===== 文件路径 =====
    private string SavePath => Path.Combine(Application.persistentDataPath, "gamesave.json");
    private string BackupPath => Path.Combine(Application.persistentDataPath, "gamesave.backup.json");

    // ========================================
    // 生命周期
    // ========================================

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);

        _saveData = new GameSaveData();
        LoadGame();
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus) SaveGame();
    }

    // ========================================
    // 核心方法
    // ========================================

    /// <summary>
    /// 加载存档
    /// </summary>
    public void LoadGame()
    {
        try
        {
            if (File.Exists(SavePath))
            {
                string json = File.ReadAllText(SavePath);
                _saveData = JsonUtility.FromJson<GameSaveData>(json);

                if (_saveData == null)
                {
                    Debug.LogWarning("存档数据损坏，创建新存档");
                    CreateNewSave();
                }
                else
                {
                    Debug.Log("✅ 存档加载成功");
                }
            }
            else
            {
                Debug.Log("📁 未找到存档，创建新存档");
                CreateNewSave();
            }

            OnGameLoaded?.Invoke();
        }
        catch (Exception e)
        {
            Debug.LogError($"❌ 加载存档失败：{e.Message}");

            // 尝试从备份恢复
            if (File.Exists(BackupPath))
            {
                try
                {
                    string json = File.ReadAllText(BackupPath);
                    _saveData = JsonUtility.FromJson<GameSaveData>(json);
                    if (_saveData != null)
                    {
                        Debug.Log("✅ 从备份恢复成功");
                        OnGameLoaded?.Invoke();
                        return;
                    }
                }
                catch { }
            }

            CreateNewSave();
        }
    }

    /// <summary>
    /// 保存存档
    /// </summary>
    public void SaveGame()
    {
        try
        {
            // 校验数据不为空
            if (_saveData == null)
            {
                _saveData = new GameSaveData();
            }

            string json = JsonUtility.ToJson(_saveData, true);

            // 先写临时文件
            string tempPath = SavePath + ".tmp";
            File.WriteAllText(tempPath, json);

            // 备份旧存档
            if (File.Exists(SavePath))
            {
                File.Copy(SavePath, BackupPath, true);
            }
            // 替换存档（Unity 的 File.Move 不支持 bool 重载，需先删除旧文件）
            if (File.Exists(SavePath))
            {
                File.Delete(SavePath);
            }
            File.Move(tempPath, SavePath);

            Debug.Log("存档已保存");
            OnGameSaved?.Invoke();
        }
        catch (Exception e)
        {
            Debug.LogError($"保存存档失败：{e.Message}");
        }
    }

    /// <summary>
    /// 创建新存档
    /// </summary>
    public void CreateNewSave()
    {
        _saveData = new GameSaveData();

        // ===== CrossSaveData：使用类里的默认值，不需要额外设置 =====

        // ===== RunSaveData：初始值 =====
        _saveData.runData.currentDay = 1;
        _saveData.runData.isNight = false;
        _saveData.runData.currentMap = "起点";
        _saveData.runData.loseNum = 0;

        // 初始宝石：根据你的设计，送 3 颗基础宝石
        _saveData.runData.ownedGems = new List<GemOwnership>
    {
        new GemOwnership { gemId = "blue", count = 3 }
    };

        _saveData.runData.ownedDevices = new List<string>();

        // 4 个装备槽位，全部为空
        _saveData.runData.equippedGems = new List<string>();
        _saveData.runData.equippedDevices = new List<OperateDevice>();
        _saveData.runData.unlockedGemsThisRun = new List<string>();
        _saveData.runData.unlockedDevicesThisRun = new List<string>();

        // ===== BattleSaveData：初始值 =====
        _saveData.battleData.currentHp = 10;
        _saveData.battleData.maxHp = 10;
        _saveData.battleData.currentDefense = 0;
        _saveData.battleData.tempGems = new List<string>();
        _saveData.battleData.enemiesKilled = 0;
        _saveData.battleData.battleTime = 0;

        OnSaveReset?.Invoke();  // 通知所有订阅者
        SaveGame();
        Debug.Log("新存档已创建");
    }

    /// <summary>
    /// 重置游戏（保留跨存档数据）
    /// </summary>
    public void ResetGame(bool keepCrossData = true)
    {
        if (keepCrossData)
        {
            var crossData = _saveData.crossData;
            CreateNewSave();  // 创建初始存档
            _saveData.crossData = crossData;  // 恢复跨存档数据
        }
        else
        {
            CreateNewSave();  // 完全重置
        }

        SaveGame();
        Debug.Log("游戏已重置");
    }

    /// <summary>
    /// 删除存档文件
    /// </summary>
    public void DeleteSaveFiles()
    {
        try
        {
            if (File.Exists(SavePath)) File.Delete(SavePath);
            if (File.Exists(BackupPath)) File.Delete(BackupPath);
            if (File.Exists(SavePath + ".tmp")) File.Delete(SavePath + ".tmp");
            Debug.Log("存档文件已删除");
        }
        catch (Exception e)
        {
            Debug.LogError($"删除存档失败：{e.Message}");
        }
    }

    /// <summary>
    /// 检查是否有存档
    /// </summary>
    public bool HasSaveFile()
    {
        return File.Exists(SavePath);
    }

    private void OnDestroy()
    {
        // 取消所有事件订阅，避免悬空引用
        OnGameLoaded = null;
        OnGameSaved = null;
        OnSaveReset = null;
    }
}