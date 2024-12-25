using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotPlus : Slot
{
    public Slot slot1;
    public Slot slot2;
    LevelController levelController;
    private void Start()
    {
        levelController = FindObjectOfType<LevelController>();
        Button Synthesisbutton = SynthesisButton.GetComponent<Button>();
        Button Decompositionbutton = DecompositionButton.GetComponent<Button>();
        Synthesisbutton.onClick.AddListener(call: () =>
        {
            Synthesis();
        });
        Decompositionbutton.onClick.AddListener(call: () =>
        {
            Decomposition();
        });
    }
    private void UpdateSummaryText()
    {
        if (slotItem != null) summaryText.text = slotItem.itemInfo;
        if (levelController.currenWave <= 3) return;
        if (slot1.slotItem.itemNumber > 0 && slot2.slotItem.itemNumber > 0)
        {
            SynthesisButton.SetActive(true);
        }else SynthesisButton.SetActive(false);
        if(slotItem.itemNumber>0)
        {
            DecompositionButton.SetActive(true);
        }else DecompositionButton.SetActive(false);
    }
    private void OnMouseEnter()
    {
        if (slotItem != null)
        {
            // 更新物品简介
            UpdateSummaryText();
        }
    }


    public void Synthesis()
    {
        AddSlot();
        slot1.SubSlot();
        slot2.SubSlot();
        UpdateSlot();
        OnMouseEnter();
        slot1.UpdateSlot();
        slot2.UpdateSlot();
    }

    public void Decomposition()
    {
        SubSlot();
        slot1.AddSlot();
        slot2.AddSlot();
        UpdateSlot();
        OnMouseEnter();
        slot1.UpdateSlot();
        slot2.UpdateSlot();
    }
}
