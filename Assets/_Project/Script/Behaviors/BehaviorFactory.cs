using System.Collections.Generic;
using UnityEngine;
using GameData;

public static class BehaviorFactory
{
    // ==================== 公开方法 ====================

    /// <summary>
    /// 根据宝石ID获取宝石策略列表
    /// </summary>
    public static List<IBehavior> GetGemBehaviors(string gemId)
    {
        if (string.IsNullOrEmpty(gemId))
        {
            return new List<IBehavior>();
        }

        GemConfig gemConfig = GameConfig.Instance.GetGemConfigById(gemId);
        if (gemConfig == null)
        {
            Debug.LogWarning($"[BehaviorFactory] 未找到宝石配置: {gemId}");
            return new List<IBehavior>();
        }

        List<IBehavior> behaviors = new List<IBehavior>();
        foreach (var entry in gemConfig.gemBehaviors)
        {
            IBehavior behavior = InstantiateBehavior(entry);
            if (behavior != null)
            {
                behaviors.Add(behavior);
            }
        }

        return behaviors;
    }

    /// <summary>
    /// 根据子弹ID获取子弹策略列表
    /// </summary>
    public static List<IBehavior> GetBulletBehaviors(string BulletId)
    {
        if (string.IsNullOrEmpty(BulletId))
        {
            return new List<IBehavior>();
        }

        BulletConfig config = GameConfig.Instance.GetBulletConfigById(BulletId);
        if (config == null)
        {
            Debug.LogWarning($"[BehaviorFactory] 未找到子弹配置: {BulletId}");
            return new List<IBehavior>();
        }

        List<IBehavior> behaviors = new List<IBehavior>();
        foreach (var entry in config.bulletBehaviors)
        {
            IBehavior behavior = InstantiateBehavior(entry);
            if (behavior != null)
            {
                behaviors.Add(behavior);
            }
        }

        return behaviors;
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

        EnemyConfig config = GameConfig.Instance.GetEnemyConfigById(enemyId);
        if (config == null)
        {
            Debug.LogWarning($"[BehaviorFactory] 未找到敌人配置: {enemyId}");
            return new List<IBehavior>();
        }

        List<IBehavior> behaviors = new List<IBehavior>();
        foreach (var entry in config.enemyBehaviors)
        {
            IBehavior behavior = InstantiateBehavior(entry);
            if (behavior != null)
            {
                behaviors.Add(behavior);
            }
        }

        return behaviors;
    }

    /// <summary>
    /// 根据 BehaviorEntry 实例化策略（公开方法，供 Bullet 使用）
    /// </summary>
    public static IBehavior InstantiateBehavior(BehaviorEntry entry)
    {
        if (entry == null || string.IsNullOrEmpty(entry.behaviorId))
        {
            return null;
        }

        BehaviorConfig config = GameConfig.Instance.GetBehaviorConfigById(entry.behaviorId);
        if (config == null)
        {
            Debug.LogWarning($"[BehaviorFactory] 未找到策略配置: {entry.behaviorId}");
            return null;
        }

        // 根据策略类型创建实例
        IBehavior behavior = CreateBehaviorInstance(config.behaviorType);
        if (behavior == null)
        {
            Debug.LogWarning($"[BehaviorFactory] 无法实例化策略类型: {config.behaviorType}");
            return null;
        }

        // 注入参数（通过反射或直接赋值）
        InjectParameters(behavior, entry, config);

        return behavior;
    }

    // ==================== 私有方法 ====================

    /// <summary>
    /// 根据类型创建策略实例
    /// </summary>
    private static IBehavior CreateBehaviorInstance(BehaviorType type)
    {
        switch (type)
        {
            // 武器行为策略
            case BehaviorType.AimAuto:
                return new AimBehavior_Auto();
            case BehaviorType.AimManual:
                return new AimBehavior_Manual();
            case BehaviorType.FireAuto:
                return new FireBehavior_Auto();
            case BehaviorType.FireManual:
                return new FireBehavior_Manual();

            // 子弹策略
            case BehaviorType.Damage:
                return new DamageBehavior();
            case BehaviorType.Heal:
                return new HealBehavior();

            // 其他策略占位（后续扩展）
            case BehaviorType.Burn:
                Debug.LogWarning($"[BehaviorFactory] {type} 策略尚未实现");
                return null;
            case BehaviorType.Pierce:
                Debug.LogWarning($"[BehaviorFactory] {type} 策略尚未实现");
                return null;
            case BehaviorType.Scatter:
                Debug.LogWarning($"[BehaviorFactory] {type} 策略尚未实现");
                return null;

            default:
                Debug.LogWarning($"[BehaviorFactory] 未处理的策略类型: {type}");
                return null;
        }
    }

    /// <summary>
    /// 注入参数到策略实例
    /// </summary>
    private static void InjectParameters(IBehavior behavior, BehaviorEntry entry, BehaviorConfig config)
    {
        // 获取最终参数值
        float damage = entry.overrideDamage ? entry.damageOverride : config.defaultDamage;
        float healAmount = entry.overrideDamage ? entry.damageOverride : config.defaultDamage;
        int count = entry.overrideCount ? entry.countOverride : config.defaultCount;

        // 根据策略类型注入参数
        if (behavior is DamageBehavior damageBehavior)
        {
            damageBehavior.SetDamage((int)damage);
        }
        else if (behavior is HealBehavior healBehavior)
        {
            healBehavior.SetHealAmount((int)healAmount);
        }
        // 其他策略的参数注入后续扩展
    }
}