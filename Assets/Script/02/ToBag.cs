using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ToBag : MonoBehaviour
{
    public Button toBagButton;
    public UIManager manager;
    void Start()
    {
        toBagButton.onClick.AddListener(call: () =>
        {
            manager.ToBag();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
