// ========================================
// 文件位置：Assets/_Project/Script/Enemies/EnemyBase.cs
// 职责：敌人基础组件（供 ResetToConfig 使用）
// 注意：这只是简化版，后续状态机实现时会扩展
// ========================================

using UnityEngine;
using GameData;

public class EnemyBase : MonoBehaviour
{
    [Header("运行时状态")]
    public int currentHp;
    public float currentSpeed;

    [Header("配置引用（只读）")]
    public string enemyId;

    private EnemyConfig cachedConfig;

    /// <summary>
    /// 根据配置重置敌人状态
    /// </summary>
    public void ResetToConfig(EnemyConfig config)
    {
        if (config == null) return;

        cachedConfig = config;
        enemyId = config.Id;
        currentHp = config.maxHp;
        currentSpeed = config.moveSpeed;
    }

    /// <summary>
    /// 受到伤害
    /// </summary>
    public void TakeDamage(int damage)
    {
        currentHp -= damage;
        if (currentHp <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// 死亡处理
    /// </summary>
    private void Die()
    {
        // 通知对象池回收
        EnemyPool.Instance.ReturnEnemy(gameObject);
    }

    // 简化版移动方法（后续状态机会接管）
    public void MoveTowards(Vector3 target, float deltaTime)
    {
        Vector3 direction = (target - transform.position).normalized;
        transform.position += direction * currentSpeed * deltaTime;
    }
}