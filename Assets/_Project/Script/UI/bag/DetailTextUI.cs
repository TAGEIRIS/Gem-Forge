using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetailTextUI : MonoBehaviour
{
    public Text text;
    public Image image;

    public void UpdateDisplay(Sprite sprite, String s)
    {
        this.image.sprite = sprite;
        text.text = s;
    }
}
