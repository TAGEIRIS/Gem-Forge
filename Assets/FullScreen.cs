using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FullScreen : MonoBehaviour
{
    public Button FullScreenButton;
    public GameObject ShowStatus;
    public GameObject Resolution;

    private void Start()
    {
        FullScreenButton.onClick.AddListener(call: () =>
        {
            if (Screen.fullScreenMode == FullScreenMode.FullScreenWindow)
            {
                FullScreenChange(false);
            }
            else
            { FullScreenChange(true);}
        });
    }
    public void FullScreenChange(bool type)
    {
        if(type)
        {
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
            Debug.Log("È«ÆÁ");
        }
        else
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
            Debug.Log("´°¿Ú");
        }
    }
    private void Update()
    {
        RenewShow();
    }

    public void RenewShow()
    {
        if (Screen.fullScreenMode == FullScreenMode.FullScreenWindow)
        {
            ShowStatus.SetActive(true);
            Resolution.SetActive(false);
        }
        else
        {
            ShowStatus.SetActive(false);
            Resolution.SetActive(true);
        }
    }
}
