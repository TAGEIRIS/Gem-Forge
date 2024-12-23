using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    //按下ESC后显示的界面
    public GameObject EscUI;
    //是否已经暂停
    public bool isPaused = false;
    //关卡控制器
    public LevelController levelController;

    private void Awake()
    {
        GameObject gameObject = GameObject.Find("LevelController");
        levelController = gameObject.GetComponent<LevelController>();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)&&isPaused==false)
        {
            PauseGame();
        }
        else if(Input.GetKeyDown(KeyCode.Escape)&&isPaused==true)
        {
            UnPauseGame();
        }
    }

    private void OnEnable()
    {
        EscUI.SetActive(false);
    }
    //暂停游戏
    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
        EscUI.SetActive(true);
    }
    //继续游戏
    public void UnPauseGame()
    {
        isPaused=false;
        Time.timeScale = 1f;
        EscUI.SetActive(false);
    }

    //返回关卡选择
    public void Back()
    {
        UnPauseGame();
        if (isPaused == false)
        {
            levelController.isPlay = false;
            levelController.BadGame(1f);
        }
    }

    //退出游戏
    public void ExitGame()
    {
        levelController.equipmentManagerInBag.UnReadyForBattle();
#if UNITY_EDITOR
        // 如果我们在Unity编辑器中运行，使用这一行代码退出播放模式
        UnityEditor.EditorApplication.isPlaying = false;
#else
            // 如果我们在构建的游戏中运行，使用这一行代码退出游戏
            Application.Quit();
#endif
    }
}
