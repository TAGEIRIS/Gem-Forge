using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New EnemyList", menuName = "Inventory/New EnemyList")]
public class EnemyList : ScriptableObject
{
    public List<GameObject> GruntList;
    public List<GameObject> BossList;
}
