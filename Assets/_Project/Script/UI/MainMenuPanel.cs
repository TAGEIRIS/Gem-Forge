// ========================================
// MainMenuPanel - 适配新设置系统
// ========================================

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuPanel : MonoBehaviour
{
    [Header("按钮")]
    public Button startButton;
    public Button continueButton;
    public Button settingsButton;
    public Button exitButton;

    [Header("设置面板")]
    public SettingUI settingUI;

    private void Start()
    {
        if (continueButton != null)
            continueButton.onClick.AddListener(OnContinueClick);

        if (startButton != null)
            startButton.onClick.AddListener(OnStartClick);

        if (settingsButton != null)
            settingsButton.onClick.AddListener(OnSettingsClick);

        if (exitButton != null)
            exitButton.onClick.AddListener(OnExitClick);
    }

    private void OnStartClick()
    {
        // 开始新游戏
        // SaveManager 已经存了默认数据，新游戏重置单局数据即可
        SaveManager.Instance.ResetGame(true); // 保留跨存档（包含设置偏好）
        SceneManager.LoadScene("02-SelectPlace");
    }

    private void OnContinueClick()
    {
        if (SaveManager.Instance.HasSaveFile())
        {
            SceneManager.LoadScene("02-SelectPlace");
        }
    }

    private void OnSettingsClick()
    {
        if (settingUI != null)
            settingUI.OpenSettings();
    }

    private void OnExitClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}