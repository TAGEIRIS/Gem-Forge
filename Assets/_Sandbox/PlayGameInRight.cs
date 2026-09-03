using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[InitializeOnLoad]
public class EditorStartupScene
{
    private static bool _isStarting = false;
    private static string _previousScenePath;
    private static bool _isWaitingToRestore = false;

    static EditorStartupScene()
    {
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        EditorApplication.update += OnEditorUpdate;
    }

    private static void OnPlayModeStateChanged(PlayModeStateChange state)
    {
        // ===== 进入 Play 模式前：切换到目标场景 =====
        if (state == PlayModeStateChange.ExitingEditMode && !_isStarting)
        {
            _isStarting = true;
            
            string currentScene = EditorSceneManager.GetActiveScene().name;
            string targetScene = "01-StartScene";

            if (currentScene != targetScene)
            {
                _previousScenePath = EditorSceneManager.GetActiveScene().path;
                EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();

                string scenePath = "Assets/_Project/Scenes/01-StartScene.unity";
                EditorSceneManager.OpenScene(scenePath);
            }
            else
            {
                _previousScenePath = null;
            }
        }

        // ===== 退出 Play 模式后：标记需要恢复 =====
        else if (state == PlayModeStateChange.ExitingPlayMode)
        {
            if (!string.IsNullOrEmpty(_previousScenePath))
            {
                _isWaitingToRestore = true;  // 标记等待恢复
            }
            _isStarting = false;
        }

        // ===== 进入 Play 模式后：重置标志 =====
        else if (state == PlayModeStateChange.EnteredPlayMode)
        {
            _isStarting = false;
        }
    }

    // ===== 每帧检查：在 Play 模式完全退出后恢复场景 =====
    private static void OnEditorUpdate()
    {
        // 如果标记了需要恢复，并且当前不在 Play 模式（已经退出）
        if (_isWaitingToRestore && !EditorApplication.isPlaying)
        {
            _isWaitingToRestore = false;

            // 再次检查：确保恢复路径存在，且当前场景不是目标场景
            if (!string.IsNullOrEmpty(_previousScenePath) && 
                System.IO.File.Exists(_previousScenePath))
            {
                // 延迟一帧执行，确保 Unity 完全退出 Play 模式
                EditorApplication.delayCall += () =>
                {
                    // 再次检查当前场景是否已经是目标场景，避免重复加载
                    string currentPath = EditorSceneManager.GetActiveScene().path;
                    if (currentPath != _previousScenePath)
                    {
                        EditorSceneManager.OpenScene(_previousScenePath);
                        Debug.Log($"已恢复场景：{_previousScenePath}");
                    }
                    _previousScenePath = null;
                };
            }
            else
            {
                _previousScenePath = null;
            }
        }
    }
}