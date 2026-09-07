using System;
using UnityEngine;

namespace GameData
{
    [Serializable]
    public class BehaviorEntry
    {
        public string behaviorId;

        // 覆盖参数（如果留空则使用 BehaviorConfig 的默认值）
        public float damageOverride;
        public float durationOverride;
        public float rangeOverride;
        public int countOverride;
        public float chanceOverride;

        public bool overrideDamage;
        public bool overrideDuration;
        public bool overrideRange;
        public bool overrideCount;
        public bool overrideChance;


    }
}