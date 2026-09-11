namespace GameData
{
    /// <summary>
    /// 游戏状态
    /// </summary>
    public enum GameState
    {
        Start,      // 启动
        MainMenu,   // 主菜单
        Fighting,   // 战斗中
        Paused,     // 暂停
        GameOver    // 游戏结束
    }

    /// <summary>
    /// 敌人类型
    /// </summary>
    public enum EnemyType
    {
        Normal,     // 普通（近战）
        Ranged,     // 远程
        Boss        // BOSS
    }

    /// <summary>
    /// 宝石类型（大类）
    /// </summary>
    public enum GemType
    {
        Atk,        // 攻击型
        Def,        // 防御型
        Eco,        // 经济型
        Spec        // 特殊型
    }

    /// <summary>
    /// 装置类型
    /// </summary>
    public enum DeviceType
    {
        Comb,       // 化合：多→一
        Decomp,     // 分解：一→多
        Meta,       // 复分解：N→M，任意重组
        Trans       // 置换（嬗变）：消耗升级，或属性替换
    }

    /// <summary>
    /// 子弹弹道类型
    /// </summary>
    public enum BulletType
    {
        Straight,   // 直线
        Homing,     // 追踪
        Orbital,    // 环绕（不飞出去，围绕自身旋转）
        Strike,     // 定点落下（直接出现在目标位置）
        Chain       // 弹射链（击中后转向下一个目标）
    }

    /// <summary>
    /// 行为策略类型
    /// </summary>
    public enum BehaviorType
{
    // ===== 武器行为策略 =====
    AimAuto,        // 自动瞄准
    AimManual,      // 手动瞄准
    FireAuto,       // 自动开火
    FireManual,     // 手动开火

    // ===== 宝石策略（作用于玩家自身） =====
    Heal,           // 回血
    Shield,         // 护盾
    Dash,           // 闪现
    StunArea,       // 定身周围敌人
    Scatter,        // 散射
    Empower,        // 增伤
    SpeedBoost,     // 加速

    // ===== 子弹策略 =====
    Damage,         // 伤害
    Burn,           // 灼烧
    Freeze,         // 冰冻
    Homing,         // 追踪
    Pierce,         // 穿透
    Split,          // 分裂
    Chain,          // 弹射
    Explode,        // 爆炸

    // ===== 敌人策略 =====
    SelfDestruct,   // 自爆
    AuraDamage,     // 光环伤害
    SlowAura,       // 减速光环
    Summon          // 召唤
}
}