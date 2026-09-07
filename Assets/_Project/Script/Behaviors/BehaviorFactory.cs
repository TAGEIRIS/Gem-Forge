using System.Collections.Generic;
using UnityEngine;

namespace GameData
{
    /// <summary>
    /// 行为策略工厂
    /// Day 1 版本：只返回空列表，确保调用入口存在
    /// Day 3 开始逐步填入具体策略实现
    /// </summary>
    public static class BehaviorFactory
    {
        /// <summary>
        /// 根据宝石ID获取宝石策略列表
        /// </summary>
        public static List<IBehavior> GetGemBehaviors(string gemId)
        {
            if (string.IsNullOrEmpty(gemId))
            {
                return new List<IBehavior>();
            }

            var gemConfig = GameConfig.Instance.GetGemConfigById(gemId);
            if (gemConfig == null)
            {
                Debug.LogWarning($"[BehaviorFactory] 未找到宝石配置: {gemId}");
                return new List<IBehavior>();
            }

            // Day 1：暂时返回空列表
            // 后续实现：遍历 gemConfig.gemBehaviors，根据 behaviorId 实例化策略

            return new List<IBehavior>();
        }

        /// <summary>
        /// 根据子弹ID获取子弹策略列表
        /// </summary>
        public static List<IBehavior> GetProjectileBehaviors(string projectileId)
        {
            if (string.IsNullOrEmpty(projectileId))
            {
                return new List<IBehavior>();
            }

            var projectileConfig = GameConfig.Instance.GetProjectileConfigById(projectileId);
            if (projectileConfig == null)
            {
                Debug.LogWarning($"[BehaviorFactory] 未找到子弹配置: {projectileId}");
                return new List<IBehavior>();
            }

            // Day 1：暂时返回空列表
            return new List<IBehavior>();
        }

        /// <summary>
        /// 根据敌人ID获取敌人策略列表
        /// </summary>
        public static List<IBehavior> GetEnemyBehaviors(string enemyId)
        {
            if (string.IsNullOrEmpty(enemyId))
            {
                return new List<IBehavior>();
            }

            var enemyConfig = GameConfig.Instance.GetEnemyConfigById(enemyId);
            if (enemyConfig == null)
            {
                Debug.LogWarning($"[BehaviorFactory] 未找到敌人配置: {enemyId}");
                return new List<IBehavior>();
            }

            // Day 1：暂时返回空列表
            return new List<IBehavior>();
        }

        // ========== Day 3 之后会实现的私有方法 ==========
        // private static IBehavior InstantiateBehavior(BehaviorEntry entry)
        // {
        //     var config = GameConfig.Instance.GetBehaviorConfigById(entry.behaviorId);
        //     if (config == null) return null;
        //
        //     // 根据 config.behaviorType 创建对应的策略实例
        //     switch (config.behaviorType)
        //     {
        //         case BehaviorType.Damage: return new DamageBehavior(entry, config);
        //         case BehaviorType.Heal: return new HealBehavior(entry, config);
        //         // ... 其他策略
        //         default: return null;
        //     }
        // }
    }
}