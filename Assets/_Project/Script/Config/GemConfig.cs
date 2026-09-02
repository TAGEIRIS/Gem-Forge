using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//宝石配置
[CreateAssetMenu(fileName = "GemConfig", menuName = "Config/GemConfig")]
public class GemConfig : ScriptableObject
{
    public string Id;
    public string displayName;
    //宝石图标
    public Sprite icon;
    //宝石类型
    public GemType gemType;
    public bool isActive;          // true=主动按键触发，false=被动自动触发
    public GameObject GemPrefab;
    // 子弹配置ID
    public string GemProjectileId;
    //宝石简介
    [TextArea] public string itemInfo;
}

