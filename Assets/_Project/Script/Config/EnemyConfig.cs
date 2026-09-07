using System.Collections.Generic;
using UnityEngine;

namespace GameData
{
    [CreateAssetMenu(fileName = "EnemyConfig", menuName = "Config/EnemyConfig")]
    public class EnemyConfig : ScriptableObject
    {
        public string Id;
        public string displayName;
        public Sprite icon;
        public EnemyType enemyType;
        public string ProjectileId;
        public GameObject prefab;

        // 数值属性（状态机使用）
        public int maxHp;
        public int attack;
        public float moveSpeed;
        public float attackRange;
        public float attackCooldown;

        // 敌人的策略列表（自爆、光环、召唤等）
        public List<BehaviorEntry> enemyBehaviors = new List<BehaviorEntry>();
    }
}