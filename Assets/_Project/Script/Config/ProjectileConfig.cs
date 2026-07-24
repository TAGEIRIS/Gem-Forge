using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//弹药配置
[CreateAssetMenu(fileName = "ProjectileConfig", menuName = "Config/ProjectileConfig")]
public class ProjectileConfig : ScriptableObject
{
    public string Id;
    public string displayName;
    public Sprite icon;
    public ProjectileType projectileType;
    public GameObject ProjectilePrefab;
    //伤害值
    public int Damage;
    //射程
    public float Range;
}