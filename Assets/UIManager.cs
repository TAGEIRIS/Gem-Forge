using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject Bag;
    public GameObject Sllect;
    public GameObject UnWeaponButtons;
    private void OnEnable()
    {
        Bag.SetActive(false);
        Sllect.SetActive(true);
        UnWeaponButtons.SetActive(false);
    }
    public void ToBag()
    {
        Sllect.SetActive(false);
        Bag.SetActive(true);
        UnWeaponButtons.SetActive(true);
    }

    public void ToSllect()
    {
        Bag.SetActive(false);
        UnWeaponButtons.SetActive(false);
        Sllect.SetActive(true);
    }
}
