using System.Collections.Generic;
using UnityEngine;

namespace GameData
{
    [CreateAssetMenu(fileName = "ProjectileConfig", menuName = "Config/ProjectileConfig")]
    public class ProjectileConfig : ScriptableObject
    {
        public string Id;
        public string displayName;
        public Sprite icon;
        public ProjectileType projectileType;
        public GameObject ProjectilePrefab;

        // 子弹固有属性
        public float speed;
        public float Range;
        public int baseDamage;

        // 子弹自身的策略列表（追踪、穿透、分裂、弹射等）
        public List<BehaviorEntry> projectileBehaviors = new List<BehaviorEntry>();
    }
}