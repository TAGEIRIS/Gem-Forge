using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyShow : MonoBehaviour
{
    private Text text;
    private void Awake()
    {
        text = GetComponent<Text>();
        KuManager.Instance.OnMoneyChanged += Updatetext;
    }

    private void Updatetext()
    {
        text.text = "ÓµÓÐ£º\n"+KuManager.Instance.GetItemNumber("money")+"½ð";
    }

}
