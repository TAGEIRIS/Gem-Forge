using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Resolution : MonoBehaviour
{
    public TMP_Dropdown dropdown;

    private void Start()
    {
        if(dropdown != null)
        {
            dropdown.onValueChanged.AddListener(delegate
            {
                OnDropdownValueChanged(dropdown);
            });
        }
    }

    //ÐÞ¸Ä·Ö±æÂÊ
    public void ResolutionChange(int type)
    {
        Debug.Log(type);
        if(type == 0)
        {
            Screen.SetResolution(1920, 1080, false);
        }
        else if(type == 1)
        {
            Screen.SetResolution(2560,1440,false);
        }
        else if(type == 2) 
        {
            Screen.SetResolution(3840,2160,false);
        }
    }
    public void FullScreen(bool fullscreen)
    {
        Screen.fullScreenMode = fullscreen ? FullScreenMode.FullScreenWindow 
            : FullScreenMode.Windowed;
    }

    private void OnDropdownValueChanged(TMP_Dropdown change)
    {
        int selectedType = change.value;
        ResolutionChange(selectedType);
    }
}
