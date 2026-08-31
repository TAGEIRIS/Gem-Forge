using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemSlot : MonoBehaviour
{
    public Sprite gemIcon;
    public string gemName;
    public int gemNum;

//初始化宝石显示信息
    public void SetGemInfo(string GemId)
    {
        GemConfig gemConfig = GameConfig.Instance.GetGemConfigById(GemId);
        gemIcon = gemConfig.icon;
        gemName = gemConfig.displayName;
        
    }
}
