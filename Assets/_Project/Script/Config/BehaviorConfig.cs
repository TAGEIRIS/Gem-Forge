using UnityEngine;

namespace GameData
{
    [CreateAssetMenu(fileName = "BehaviorConfig", menuName = "Config/BehaviorConfig")]
    public class BehaviorConfig : ScriptableObject
    {
        public string Id;
        public string displayName;
        public BehaviorType behaviorType;

        [TextArea]
        public string description;

        // 策略的默认参数
        public float defaultDamage;
        public float defaultDuration;
        public float defaultRange;
        public int defaultCount;
        public float defaultChance;
    }
}