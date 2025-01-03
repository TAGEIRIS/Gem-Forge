using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waigua : MonoBehaviour
{
    public LevelController levelController;

    private void Awake()
    {
        levelController = GameObject.Find("LevelController").GetComponent<LevelController>();
    }
    public void a()
    {
        levelController.waveTimer = 0.2f;
    }
}
