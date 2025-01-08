using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownCanvasManager : MonoBehaviour
{    
    public GameObject TownMainCanvas;
    public GameObject GemStoreCanvas;
    public GameObject BlacksmithyCanvas;
    public GameObject LibraryCanvas;
    public GameObject MuseumCanvas;

    private void Start()
    {
        ToPlace(0);
    }






    public void ToPlace(int num)
    {
        Clear();
        if (num == 0) TownMainCanvas.SetActive(true);
        else if (num == 1) GemStoreCanvas.SetActive(true);
        else if (num == 2) BlacksmithyCanvas.SetActive(true);
        else if (num == 3) LibraryCanvas.SetActive(true);
        else if (num == 4) MuseumCanvas.SetActive(true);
    }
    public void Clear()
    {
        TownMainCanvas.SetActive(false);
        GemStoreCanvas.SetActive(false);
        BlacksmithyCanvas.SetActive(false);
        LibraryCanvas.SetActive(false);
        MuseumCanvas.SetActive(false);
    }
}
