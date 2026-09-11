using System.Collections.Generic;
using UnityEngine;

namespace GameData
{
    [CreateAssetMenu(fileName = "BulletConfig", menuName = "Config/BulletConfig")]
    public class BulletConfig : ScriptableObject
    {
        public string Id;
        public string displayName;
        public Sprite icon;
        public BulletType bulletType;
        public GameObject bulletPrefab;

        // 子弹固有属性
        public float speed;
        public float Range;
        public int baseDamage;

        // 子弹自身的策略列表（追踪、穿透、分裂、弹射等）
        public List<BehaviorEntry> bulletBehaviors = new List<BehaviorEntry>();
    }
}