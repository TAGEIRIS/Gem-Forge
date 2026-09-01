using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    // ===== 单例 =====
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
                if (_instance == null)
                {
                    GameObject go = new GameObject("GameManager");
                    _instance = go.AddComponent<GameManager>();
                    DontDestroyOnLoad(go);
                }
            }
            return _instance;
        }
    }
    // 场景名称常量
    private const string StartScene = "01-StartScene";
    private const string TownScene = "02-TownScene";
    private const string BattleScene = "03-BattleScene";

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        // 避免内存泄漏
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // ========== 外部接口 ==========

    // 开始新游戏
    public void StartNewGame()
    {
        SaveManager.Instance.ResetGame(true);// 保留跨存档（包含设置偏好）
        SceneManager.LoadScene(TownScene);
    }

    // 继续游戏
    public void ContinueGame()
    {
        if (SaveManager.Instance.HasSaveFile())
        {
            SceneManager.LoadScene(TownScene);
        }
        else
        {
            Debug.LogWarning("没有存档，无法继续");
            // 可以选择跳转到主菜单或新游戏
            SceneManager.LoadScene(StartScene);
        }
    }

    //从城镇进入战斗（夜晚）
    public void StartBattle()
    {
        SaveManager.Instance.SaveGame();
        SceneManager.LoadScene(BattleScene);
    }

    // 战斗结束，返回城镇（白天）
    public void EndBattle(bool victory)
    {
        var data = SaveManager.Instance.SaveData;

        // 推进天数
        data.runData.currentDay++;
        if (victory)
        {
            // 检查是否通关（15天结束）
            if (data.runData.currentDay > 15)
            {
                TriggerEnding();
                return;
            }

            SaveManager.Instance.SaveGame();
            SceneManager.LoadScene(TownScene);
        }
        else
        {
            data.runData.loseNum++;
            // 战斗失败处理
            if (data.runData.loseNum >= 3 || data.runData.currentDay > 15)
            {
                data.runData.loseNum = 3;
                TriggerEnding();
                return;
            }
            SaveManager.Instance.SaveGame();
            SceneManager.LoadScene(TownScene);
        }
    }

    // 推进到新的一天（白天切换）
    public void NextDay()
    {
        var data = SaveManager.Instance.SaveData;
        data.runData.currentDay++;
        data.runData.isNight = false;
        SaveManager.Instance.SaveGame();
        SceneManager.LoadScene(TownScene);
    }

    // 触发结局
    private void TriggerEnding()
    {
        // 判断结局类型，保存跨存档数据
        var data = SaveManager.Instance.SaveData;
        // 这里填入结局判定逻辑
        SaveManager.Instance.SaveGame();
        SceneManager.LoadScene(StartScene); // 回到主菜单
    }

    // 返回主菜单
    public void EndGame()
    {
        SaveManager.Instance.SaveGame();
        SceneManager.LoadScene(StartScene);
    }
    //退出游戏
    private void ExitGame()
    {
        SaveManager.Instance.SaveGame();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    // ========== 场景加载回调 ==========

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"场景加载完成：{scene.name}");

        switch (scene.name)
        {
            case TownScene:
                InitializeTownSystems();
                break;
            case BattleScene:
                InitializeBattleSystems();
                break;
            case StartScene:
                break;
        }
    }

    // ========== 初始化方法 ==========

    private void InitializeTownSystems()
    {
        var data = SaveManager.Instance.SaveData;

        PageRouter.Instance.Initialize(data);
        Debug.Log("城镇初始化完成");

    }

    private void InitializeBattleSystems()
    {
        var data = SaveManager.Instance.SaveData;
        Debug.Log("战斗初始化完成");

    }

}