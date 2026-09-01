using UnityEngine;
using UnityEngine.UI;
using TMPro;

// ========================================
// SettingUI - 挂载在设置面板上
// 只负责显示和交互，数据由 SettingManager 管理
// ========================================

public class SettingUI : MonoBehaviour
{
    [Header("UI 控件")]
    public Toggle fullscreenToggle;
    public TMP_Dropdown resolutionDropdown;
    public Slider masterVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;
    public Button closeButton;

    [Header("面板引用")]
    public GameObject settingPanel;

    private bool isInitializing = false;

    private void Awake()
    {
        // 默认关闭
        if (settingPanel != null)
            settingPanel.SetActive(false);
    }

    private void OnEnable()
    {
        // 每次打开时刷新显示
        RefreshUI();
    }

    private void Start()
    {
        BindUIEvents();
    }

    // ========================================
    // 绑定 UI 事件
    // ========================================

    private void BindUIEvents()
    {
        if (fullscreenToggle != null)
            fullscreenToggle.onValueChanged.AddListener(OnFullscreenToggle);

        if (resolutionDropdown != null)
            resolutionDropdown.onValueChanged.AddListener(OnResolutionChanged);

        if (masterVolumeSlider != null)
            masterVolumeSlider.onValueChanged.AddListener(OnMasterVolumeChanged);

        if (musicVolumeSlider != null)
            musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeChanged);

        if (sfxVolumeSlider != null)
            sfxVolumeSlider.onValueChanged.AddListener(OnSfxVolumeChanged);

        if (closeButton != null)
            closeButton.onClick.AddListener(CloseSettings);
    }

    // ========================================
    // UI 事件回调 → 转发给 SettingManager
    // ========================================

    private void OnFullscreenToggle(bool isOn)
    {
        if (isInitializing) return;
        SettingManager.Instance.SetFullscreen(isOn);
    }

    private void OnResolutionChanged(int index)
    {
        if (isInitializing) return;
        SettingManager.Instance.SetResolution(index);
    }

    private void OnMasterVolumeChanged(float value)
    {
        if (isInitializing) return;
        SettingManager.Instance.SetMasterVolume(value);
    }

    private void OnMusicVolumeChanged(float value)
    {
        if (isInitializing) return;
        SettingManager.Instance.SetMusicVolume(value);
    }

    private void OnSfxVolumeChanged(float value)
    {
        if (isInitializing) return;
        SettingManager.Instance.SetSfxVolume(value);
    }

    // ========================================
    // 刷新 UI 显示（从 SettingManager 读取数据）
    // ========================================

    public void RefreshUI()
    {
        isInitializing = true;

        var settings = SettingManager.Instance.GetSettings();

        if (fullscreenToggle != null)
            fullscreenToggle.isOn = settings.isFullscreen;

        if (resolutionDropdown != null)
            resolutionDropdown.value = settings.resolutionIndex;

        if (masterVolumeSlider != null)
            masterVolumeSlider.value = settings.masterVolume;

        if (musicVolumeSlider != null)
            musicVolumeSlider.value = settings.musicVolume;

        if (sfxVolumeSlider != null)
            sfxVolumeSlider.value = settings.sfxVolume;

        isInitializing = false;
    }

    // ========================================
    // 对外接口（供其他系统调用）
    // ========================================

    public void OpenSettings()
    {
        if (settingPanel != null)
        {
            RefreshUI();
            settingPanel.SetActive(true);
        }
    }

    public void CloseSettings()
    {
        if (settingPanel != null)
        {
            settingPanel.SetActive(false);
            // 确保最后一次修改已保存（其实每次修改都自动存了）
            SaveManager.Instance.SaveGame();
        }
    }

    public void ToggleSettings()
    {
        if (settingPanel != null)
        {
            if (settingPanel.activeSelf)
                CloseSettings();
            else
                OpenSettings();
        }
    }
}