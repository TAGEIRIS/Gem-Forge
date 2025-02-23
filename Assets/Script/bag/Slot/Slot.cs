using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    //物品信息
    public Item slotItem;
    //UI上的显示
    public Image slotImage;
    //物品个数
    public Text slotNum;
    //装备管理器
    public EquipmentManagerInBag equipmentManagerInBag;
    //下一件装备应该放的位置
    public int slotIndex;
    //物品简介显示文本
    public Text summaryText;
    //价格显示文本
    public Text sellingNumber;
    //合成与分解的按钮物体
    public GameObject sellingButton;


    private void Awake()
    {
        equipmentManagerInBag = GameObject.FindObjectOfType<EquipmentManagerInBag>();
        
        GameObject gameObject = GameObject.Find("item Description");
        summaryText = gameObject.GetComponent<Text>();

        sellingButton = GameObject.Find("SellingButton");
        GameObject gameObject1 = GameObject.Find("SellingNumber");
        sellingNumber = gameObject1.GetComponent<Text>();
    }

    private void OnEnable()
    {
        UpdateSlot();
    }


    //更新slot显示
    public void UpdateSlot()
    {
        //为零则隐藏Slot
        if (slotItem != null && slotItem.itemNumber > 0)
        {
            slotNum.text = slotItem.itemNumber.ToString();
            slotNum.enabled = true;
            slotImage.color = new Color(slotImage.color.r,slotImage.color.g,slotImage.color.b,1f);
        }
        else
        {
            slotNum.enabled = false;
            slotImage.color = new Color(slotImage.color.r, slotImage.color.g, slotImage.color.b, 0.5f);
        }

    }


    //增加宝石数量
    public void AddSlot()
    {
        if (slotItem.itemNumber < slotItem.itemNumberMax)
        {
            slotItem.itemNumber++;
            UpdateSlot();
        }
    }

    //减少宝石数量
    public void SubSlot()
    {
        if (slotItem.itemNumber > 0)
        {
            slotItem.itemNumber--;
            UpdateSlot();
        }
    }

    //出售

    //装备宝石
    public void Weapon()
    {
        if (slotItem != null && slotItem.itemNumber > 0)
        {
            for (int i = 0; i < equipmentManagerInBag.equipmentPositions.Length; i++)
            {
                if(!equipmentManagerInBag.equippedItems.ContainsKey(i))
                {
                    equipmentManagerInBag.EquipItem(slotItem,i,this);
                    break;
                }
            }
        }
    }

    //更新文本
    private void UpdateText()
    {
        if(slotItem!=null)summaryText.text = slotItem.itemInfo;
        string s = slotItem.SellingPrice.ToString();
        s += "金币";
        sellingNumber.text = s;

    }

    // 当鼠标指针进入游戏对象时调用
    private void OnMouseEnter()
    {
        if (slotItem != null)
        {
            // 更新物品文本信息
            UpdateText();
        }
    }

}
