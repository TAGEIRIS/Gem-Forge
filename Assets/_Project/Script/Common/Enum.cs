using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
    Comb,   // Combination   化合：多→一，小→大
    Decomp, // Decomposition 分解：一→多，大→小
    Meta,    // Metathesis    复分解：N→M，任意重组
    Trans  // Transmutation 置换（嬗变）：消耗升级，或属性替换
}

public enum ProjectileType
{
    Straight,   // 基本款，配合 count 和 spread 参数可变成扇形/爆发/霰弹
    Homing,     // 追踪（每帧转向目标）
    Orbital,    // 环绕（围绕自身旋转，不飞出去）
    Strike,     // 定点落下（直接出现在目标位置）
    Chain       // 弹射链（击中后转向下一个目标）
}


