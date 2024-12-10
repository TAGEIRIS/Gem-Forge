using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ToBag : MonoBehaviour
{
    public Button toBagButton;
    void Start()
    {
        toBagButton.onClick.AddListener(call: () =>
        {
            SceneManager.LoadScene("Bag");
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
