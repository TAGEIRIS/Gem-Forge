using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class SettingManager : MonoBehaviour
{
    // ===== 单例 =====
    private static SettingManager _instance;
    public static SettingManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SettingManager>();
                if (_instance == null)
                {
                    GameObject go = new GameObject("SettingManager");
                    _instance = go.AddComponent<SettingManager>();
                    DontDestroyOnLoad(go);
                }
            }
            return _instance;
        }
    }

    // ===== 事件 =====
    public event Action<CrossSaveData> OnSettingsChanged;
    public event Action OnSettingsApplied;

    // ===== 快捷访问设置数据 =====
    private CrossSaveData Settings => SaveManager.Instance.SaveData.crossData;

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
    }

    private void Start()
    {
        // 等待 SaveManager 加载完成后应用设置
        ApplyAllSettings();
    }

    // ========================================
    // 应用设置
    // ========================================

    public void ApplyAllSettings()
    {
        var s = Settings;
        ApplyFullscreen(s.isFullscreen);
        ApplyResolution(s.resolutionIndex);
        ApplyVolume(s.masterVolume, s.musicVolume, s.sfxVolume);
        
        OnSettingsApplied?.Invoke();
        Debug.Log("✅ 所有设置已应用");
    }

    // ========================================
    // 单项设置（修改后自动保存）
    // ========================================

    public void SetFullscreen(bool isFullscreen)
    {
        Settings.isFullscreen = isFullscreen;
        ApplyFullscreen(isFullscreen);
        OnSettingsChanged?.Invoke(Settings);
        SaveManager.Instance.SaveGame();
    }

    public void SetResolution(int index)
    {
        Settings.resolutionIndex = index;
        ApplyResolution(index);
        OnSettingsChanged?.Invoke(Settings);
        SaveManager.Instance.SaveGame();
    }

    public void SetMasterVolume(float value)
    {
        Settings.masterVolume = Mathf.Clamp01(value);
        ApplyVolume(Settings.masterVolume, Settings.musicVolume, Settings.sfxVolume);
        OnSettingsChanged?.Invoke(Settings);
        SaveManager.Instance.SaveGame();
    }

    public void SetMusicVolume(float value)
    {
        Settings.musicVolume = Mathf.Clamp01(value);
        ApplyVolume(Settings.masterVolume, Settings.musicVolume, Settings.sfxVolume);
        OnSettingsChanged?.Invoke(Settings);
        SaveManager.Instance.SaveGame();
    }

    public void SetSfxVolume(float value)
    {
        Settings.sfxVolume = Mathf.Clamp01(value);
        ApplyVolume(Settings.masterVolume, Settings.musicVolume, Settings.sfxVolume);
        OnSettingsChanged?.Invoke(Settings);
        SaveManager.Instance.SaveGame();
    }

    public void SetLanguage(string languageCode)
    {
        Settings.language = languageCode;
        OnSettingsChanged?.Invoke(Settings);
        SaveManager.Instance.SaveGame();
        
        // 如果以后有本地化系统，在这里触发语言切换事件
        // LocalizationManager.Instance.ChangeLanguage(languageCode);
    }

    // ========================================
    // 私有执行方法
    // ========================================

    private void ApplyFullscreen(bool isFullscreen)
    {
        Screen.fullScreenMode = isFullscreen 
            ? FullScreenMode.FullScreenWindow 
            : FullScreenMode.Windowed;
        Debug.Log($"全屏模式：{isFullscreen}");
    }

    private void ApplyResolution(int index)
    {
        var resolution = GetResolutionByIndex(index);
        if (resolution.HasValue)
        {
            Screen.SetResolution(resolution.Value.width, resolution.Value.height, Settings.isFullscreen);
            Debug.Log($"分辨率已切换：{resolution.Value.width}x{resolution.Value.height}");
        }
    }

    private void ApplyVolume(float master, float music, float sfx)
    {
        // TODO: 对接音频系统
        // AudioManager.Instance.SetMasterVolume(master);
        // AudioManager.Instance.SetMusicVolume(music);
        // AudioManager.Instance.SetSFXVolume(sfx);
        Debug.Log($"音量已应用：主音量={master}, 音乐={music}, 音效={sfx}");
    }

    // ========================================
    // 辅助方法
    // ========================================

    private (int width, int height)? GetResolutionByIndex(int index)
    {
        return index switch
        {
            0 => (1920, 1080),
            1 => (2560, 1440),
            2 => (3840, 2160),
            _ => null
        };
    }

    public string GetCurrentResolutionText()
    {
        var res = GetResolutionByIndex(Settings.resolutionIndex);
        return res.HasValue ? $"{res.Value.width}x{res.Value.height}" : "未知";
    }

    public (int width, int height)[] GetAvailableResolutions()
    {
        return new (int, int)[]
        {
            (1920, 1080),
            (2560, 1440),
            (3840, 2160)
        };
    }

    /// <summary>
    /// 获取当前设置快照（供UI读取）
    /// </summary>
    public CrossSaveData GetSettings() => Settings;
}