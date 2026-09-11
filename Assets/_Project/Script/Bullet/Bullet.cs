using System.Collections.Generic;
using UnityEngine;
using GameData;

public class Bullet : MonoBehaviour
{
    [Header("配置")]
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifeTime = 3f;
    [SerializeField] private List<BehaviorEntry> defaultBehaviors = new List<BehaviorEntry>(); // 子弹自带的默认策略

    [Header("运行时状态")]
    [SerializeField] private Vector2 direction;
    [SerializeField] private float currentLifeTime = 0f;

    private BehaviorContainer container;
    private Rigidbody2D rb;

    private void Awake()
    {
        Debug.Log("Bullet Awake");
        container = GetComponent<BehaviorContainer>();
        if (container == null)
        {
            container = gameObject.AddComponent<BehaviorContainer>();
        }

        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
            rb.gravityScale = 0f;
        }

        // 加载子弹自带的默认策略（从预制体配置）
        LoadDefaultBehaviors();
    }

    /// <summary>
    /// 加载子弹自带的默认策略（从预制体配置的 BehaviorEntry 列表）
    /// </summary>
    private void LoadDefaultBehaviors()
    {
        if (defaultBehaviors == null || defaultBehaviors.Count == 0) return;

        foreach (var entry in defaultBehaviors)
        {
            // 通过 BehaviorFactory 实例化策略
            IBehavior behavior = BehaviorFactory.InstantiateBehavior(entry);
            if (behavior != null)
            {
                container.AddBehavior(behavior);
            }
        }
    }

    /// <summary>
    /// 初始化子弹（由 GemWeapon 调用）
    /// </summary>
    /// <param name="fireDirection">发射方向</param>
    /// <param name="gemBehaviors">宝石传入的策略列表（已经过滤过，只含 IBulletBehavior）</param>
    public void Initialize(Vector2 fireDirection, List<IBehavior> gemBehaviors)
    {

        Debug.Log($"Bullet Initialize: {direction}, speed={speed}");
        direction = fireDirection.normalized;

        // 合并策略：宝石传入的覆盖子弹自带的
        MergeBehaviors(gemBehaviors);

        // 设置速度
        rb.velocity = direction * speed;

        // 重置生命周期
        currentLifeTime = 0f;
    }

    /// <summary>
    /// 合并策略：宝石传入的覆盖子弹自带的（按类型去重）
    /// </summary>
    private void MergeBehaviors(List<IBehavior> gemBehaviors)
    {
        if (gemBehaviors == null || gemBehaviors.Count == 0) return;

        // 获取子弹现有的策略列表
        var existingBehaviors = container.GetBehaviors();

        // 收集宝石策略的类型列表
        List<System.Type> gemBehaviorTypes = new List<System.Type>();
        foreach (var gemBehavior in gemBehaviors)
        {
            gemBehaviorTypes.Add(gemBehavior.GetType());
        }

        // 移除子弹自带的、与宝石策略同类型的策略
        foreach (var existing in existingBehaviors)
        {
            if (gemBehaviorTypes.Contains(existing.GetType()))
            {
                container.RemoveBehavior(existing);
            }
        }

        // 添加宝石策略
        container.AddBehaviors(gemBehaviors);
    }

    private void Update()
    {
        // 生命周期计时
        currentLifeTime += Time.deltaTime;
        if (currentLifeTime >= lifeTime)
        {
            DestroyBullet();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
{
    // ===== 保护1：忽略自己 =====
    if (other.gameObject == gameObject) return;

    // ===== 保护2：忽略其他子弹 =====
    if (other.CompareTag("Bullet")) return;
    if (other.GetComponent<Bullet>() != null) return;

    // ===== 保护3：忽略发射者（玩家） =====
    if (other.CompareTag("Player")) return;

    // ===== 保护4：只对敌人触发 =====
    if (!other.CompareTag("Enemy")) return;

    // 触发容器碰撞事件，所有策略的 OnHit 会被调用
    container.TriggerHit(other.gameObject);

    // 检查是否有穿透策略
    // bool hasPierce = container.HasBehavior<PierceBehavior>();
    // if (!hasPierce)
    // {
    //     DestroyBullet();
    // }

    DestroyBullet();
}

    /// <summary>
    /// 销毁子弹（由对象池或直接销毁）
    /// </summary>
    private void DestroyBullet()
    {
        // 这里可以先不用对象池，直接销毁，后续优化
        Destroy(gameObject);
    }

    /// <summary>
    /// 设置子弹速度（供策略修改，如追踪策略需要加速）
    /// </summary>
    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
        rb.velocity = direction * speed;
    }

    /// <summary>
    /// 获取当前速度
    /// </summary>
    public float GetSpeed() => speed;

    /// <summary>
    /// 获取飞行方向
    /// </summary>
    public Vector2 GetDirection() => direction;
}