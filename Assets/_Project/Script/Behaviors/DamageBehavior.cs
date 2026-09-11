using UnityEngine;
using GameData;

public class DamageBehavior : IBulletBehavior
{
    private int damageAmount = 10;

    public void OnInitialize(GameObject owner)
    {
        // 从 BehaviorEntry 读取参数在工厂中已完成，这里直接使用已设置的值
        // 如果需要在初始化时额外处理，可以在这里做
    }

    /// <summary>
    /// 设置伤害值（由工厂调用）
    /// </summary>
    public void SetDamage(int damage)
    {
        damageAmount = damage;
    }

    public void OnUpdate()
    {
        // 伤害策略不需要每帧更新
    }

    public void OnHit(GameObject target)
    {
        if (target == null) return;

        // 查找目标上的 EnemyBase 组件
        EnemyBase enemy = target.GetComponent<EnemyBase>();
        if (enemy != null)
        {
            enemy.TakeDamage(damageAmount);
            Debug.Log($"[DamageBehavior] 对 {target.name} 造成 {damageAmount} 点伤害");
        }
    }

    public void OnDispose()
    {
        // 清理资源
    }
}