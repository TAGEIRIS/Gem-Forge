using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[InitializeOnLoad]
public class EditorStartupScene
{
    private const string TargetScenePath = "Assets/_Project/Scenes/01-StartScene.unity";
    private const string PrevSceneKey = "EditorStartupScene.PreviousScenePath";
    private static bool _isSwitching = false;

    static EditorStartupScene()
    {
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }

    private static void OnPlayModeStateChanged(PlayModeStateChange state)
    {
        // 进入 Play 前：记录并切换到目标场景
        if (state == PlayModeStateChange.ExitingEditMode && !_isSwitching)
        {
            var active = EditorSceneManager.GetActiveScene();
            if (active.path == TargetScenePath)
            {
                SessionState.EraseString(PrevSceneKey); // 本来就在目标场景
                return;
            }

            if (!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            {
                // 用户取消保存，放弃切换
                return;
            }

            _isSwitching = true;
            SessionState.SetString(PrevSceneKey, active.path);
            EditorSceneManager.OpenScene(TargetScenePath);
            _isSwitching = false;
        }
        // 完全退出 Play 后：恢复
        else if (state == PlayModeStateChange.EnteredEditMode)
        {
            string prev = SessionState.GetString(PrevSceneKey, "");
            if (!string.IsNullOrEmpty(prev) && System.IO.File.Exists(prev))
            {
                // 用 delayCall 避免在状态回调里直接切场景
                EditorApplication.delayCall += () =>
                {
                    if (EditorSceneManager.GetActiveScene().path != prev)
                    {
                        EditorSceneManager.OpenScene(prev);
                        Debug.Log($"已恢复场景：{prev}");
                    }
                    SessionState.EraseString(PrevSceneKey);
                };
            }
            else
            {
                SessionState.EraseString(PrevSceneKey);
            }
        }
    }
}