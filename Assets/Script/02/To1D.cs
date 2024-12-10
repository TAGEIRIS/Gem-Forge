using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class To1D : MonoBehaviour
{
    public Button to1DButton;
    void Start()
    {
        to1DButton.onClick.AddListener(call: () =>
        {
            SceneManager.LoadScene("03-GamePlay");
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
