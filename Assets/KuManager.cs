using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KuManager : MonoBehaviour
{
    public Inventory GemsKu;
    public Item AB1;
    //Blue
    public Item AB2;
    //Green
    public Item AW1;
    //Red
    public Item AW2;
    //Yellow
    public Item BB1;
    //Crystal

    private void Awake()
    {
        AB1 = FindGem(AB1,"AB1");
        AB2 = FindGem(AB2, "AB2");
        AW1 = FindGem(AW1, "AW1");
        AW2 = FindGem(AW2, "AW2");
        BB1 = FindGem(BB1, "BB1");
    }


    public void ReSetKu()
    {
        AB1.itemNumber = 3;
        AB2.itemNumber = 3;
        AW1 .itemNumber = 3;
        AW2.itemNumber = 3;
        BB1.itemNumber = 0;
    }




    private Item FindGem(Item item,string s)
    {
        foreach (var itema in GemsKu.itemList)
        {
            if (itema.name == s)
            {
                return itema;
            }
        }
        Debug.Log("Fuck");
        return null;
    }
}
