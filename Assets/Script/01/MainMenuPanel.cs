using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuPanel : MonoBehaviour
{
    private Button StartButton;
    private Button SetButton;
    private Button ExitButton;

    private void Awake()
    {
        StartButton =GameObject.Find("StartButton").GetComponent<Button>();
        SetButton=GameObject.Find("SetButton").GetComponent <Button>();
        ExitButton = GameObject.Find("ExitButton").GetComponent<Button>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartButton.onClick.AddListener(call: () =>
        {
            SceneManager.LoadScene("02-SelectPlace");
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
