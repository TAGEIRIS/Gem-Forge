using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public abstract class Map : MonoBehaviour
{
    protected LevelController levelController;
    public Transform _map;
    //宝石掉落物
    public Dictionary<string, int> DroppedGems;

    private void Awake()
    {
        levelController = LevelController.Instance;
    }

    private void Start()
    {
        Startgame();
    }
    //产生敌人的协程
    protected IEnumerator SwawnEnemy(GameObject En, float CD)
    {
        while (levelController.waveTimer > 0 && Player.Instance.hp > 0)
        {
            yield return new WaitForSeconds(CD);

            var spawnPoint = levelController.GetRandomPosition(_map.GetComponent<SpriteRenderer>().bounds);

            EnemyBase go = Instantiate(En, spawnPoint, Quaternion.identity)
                .GetComponent<EnemyBase>();
            levelController.enemy_List.Add(go);
        }
    }

    public void Startgame()
    {
        //初始化地图位置
        _map = transform;
        //生成敌人
        GenerateEnemy();
    }

    public void GenerateEnemy()
    {
        if (levelController.isPlay == false) return;

        if (levelController.currenWave % 5 == 1) D1();
        else if (levelController.currenWave % 5 == 2) D2();
        else if (levelController.currenWave % 5 == 3) D3();
        else if (levelController.currenWave % 5 == 4) D4();
        else if (levelController.currenWave % 5 == 0) D5();
    }
    public virtual void D1() { }
    public virtual void D2() { }
    public virtual void D3() { }
    public virtual void D4() { }
    public virtual void D5() { }


}
