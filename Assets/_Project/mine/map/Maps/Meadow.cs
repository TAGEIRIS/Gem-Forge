using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meadow : Map
{
    //敌人预制体
    public GameObject enemyG;
    public GameObject enemyR;
    public GameObject enemyB;
    public GameObject enemyWCG;
    public GameObject enemyBoss;

    //关卡敌人生成
    public override void D1()
    {
        StartCoroutine(SwawnEnemy(enemyG, 0.2f));
    }
    public override void D2()
    {
        StartCoroutine(SwawnEnemy(enemyG, 0.5f));
        StartCoroutine(SwawnEnemy(enemyB, 0.6f));
    }
    public override void D3()
    {
        StartCoroutine(SwawnEnemy(enemyG, 0.5f));
        StartCoroutine(SwawnEnemy(enemyR, 1f));
    }
    public override void D4()
    {
        StartCoroutine(SwawnEnemy(enemyR, 0.5f));
        StartCoroutine(SwawnEnemy(enemyWCG, 0.8f));
    }
    public override void D5()
    {
        StartCoroutine(SwawnEnemy(enemyR, 0.3f));
        StartCoroutine(SwawnEnemy(enemyB, 0.8f));
        StartCoroutine(SwawnEnemy(enemyBoss, 16f));
    }
}
