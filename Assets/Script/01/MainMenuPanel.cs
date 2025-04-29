using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuPanel : MonoBehaviour
{
    private Button StartButton;
    private Button ContinueButton;
    private Button SetButton;
    private Button ExitButton;

    public GameObject SettingUI;

    private void Awake()
    {
        StartButton =GameObject.Find("StartButton").GetComponent<Button>();
        ContinueButton = GameObject.Find("ContinueButton").GetComponent<Button> ();
        SetButton=GameObject.Find("SetButton").GetComponent <Button>();
        ExitButton = GameObject.Find("ExitButton").GetComponent<Button>();
        SettingUI = GameObject.Find("Settings");
        SettingUI.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        ContinueButton.onClick.AddListener(call: () =>
        {
            SceneManager.LoadScene("02-SelectPlace");
        });
        StartButton.onClick.AddListener(call: () =>
        {
            KuManager.Instance.ReSetKu();
            PlayerPrefs.DeleteAll();
            SceneManager.LoadScene("02-SelectPlace");
        });
        SetButton.onClick.AddListener(call: () =>
        {
            SettingUI.SetActive(true);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
