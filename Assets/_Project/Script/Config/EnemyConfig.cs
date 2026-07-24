using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConfig", menuName = "Config/EnemyConfig")]
public class EnemyConfig : ScriptableObject
{
    // 基础信息
    public string Id;
    public string displayName;
    public Sprite icon;
    public EnemyType enemyType;
    public string ProjectileId;
    public GameObject prefab;

    // 数值属性
    public int maxHp;
    public int attack;
    public float moveSpeed;
}