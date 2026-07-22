using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipLevel : MonoBehaviour
{
    LevelController controller;
    private void Awake()
    {
        controller = GameObject.Find("LevelController").GetComponent<LevelController>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F11))
        {
            controller.LevelOver(0.5f,true);
        }
    }
}
