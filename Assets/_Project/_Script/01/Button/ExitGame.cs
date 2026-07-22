using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGame : MonoBehaviour
{
    // Start is called before the first frame update
    public void Exit()
    {
#if UNITY_EDITOR
        // 如果我们在Unity编辑器中运行，使用这一行代码退出播放模式
        UnityEditor.EditorApplication.isPlaying = false;
#else
            // 如果我们在构建的游戏中运行，使用这一行代码退出游戏
            Application.Quit();
#endif
    }
}
