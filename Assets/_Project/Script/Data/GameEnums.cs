namespace GameData
{
    public enum GameState
    {
        Start,
        MainMenu,
        Fighting,
        Paused,
        GameOver
    }

    public enum EnemyType
    {
        Normal,
        Ranged,
        Boss
    }

    public enum GemType
    {
        Atk,
        Def,
        Eco,
        Spec
    }

    public enum DeviceType
    {
        Comb,    // 化合
        Decomp,  // 分解
        Meta,    // 复分解
        Trans    // 置换
    }

    public enum ProjectileType
    {
        Straight,
        Homing,
        Orbital,
        Strike,
        Chain
    }

    public enum BehaviorType
    {
        // 宝石策略（作用于玩家自身或发射行为）
        Heal,
        Shield,
        Dash,
        StunArea,
        Scatter,
        Empower,
        SpeedBoost,
        
        // 子弹策略（作用于子弹飞行和命中）
        Damage,
        Burn,
        Freeze,
        Homing,
        Pierce,
        Split,
        Chain,
        Explode,
        
        // 敌人策略（作用于敌人的额外能力）
        SelfDestruct,
        AuraDamage,
        SlowAura,
        Summon
    }
}