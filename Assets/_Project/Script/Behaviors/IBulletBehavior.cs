using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameData;


/// <summary>
/// 实现此接口的策略可以附着到子弹上
/// 用于 GemWeapon.Fire() 时过滤策略列表
/// </summary>
public interface IBulletBehavior : IBehavior
{
    // 空接口，仅作为标记
}
