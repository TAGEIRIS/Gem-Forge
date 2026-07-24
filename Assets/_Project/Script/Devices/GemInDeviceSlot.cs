using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemInDeviceSlot : MonoBehaviour
{
    public Sprite gemIcon;
    public string gemName;
    public int gemNum;

    public void SetGemInfo(string GemId)
    {
        GemConfig gemConfig = GameConfig.Instance.GetGemConfigById(GemId);
        gemIcon = gemConfig.icon;
        gemName = gemConfig.displayName;
        
    }
}
