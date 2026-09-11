using System.Collections.Generic;
using UnityEngine;

namespace GameData
{
    [CreateAssetMenu(fileName = "GemConfig", menuName = "Config/GemConfig")]
    public class GemConfig : ScriptableObject
    {
        public string Id;
        public string displayName;
        public Sprite icon;
        public GemType gemType;
        public bool isActive;
        public GameObject GemPrefab;

        // 宝石的子弹ID（如果宝石会发射子弹）
        public string GemBulletId;

        // 宝石自身的策略列表（回血、闪现、散射、定身等）
        public List<BehaviorEntry> gemBehaviors = new List<BehaviorEntry>();

        [TextArea]
        public string itemInfo;
    }
}