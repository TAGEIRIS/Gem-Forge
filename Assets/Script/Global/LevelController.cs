using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    public static LevelController Instance;

    public float waveTimer;
    public GameObject _failPanel;
    public GameObject _successPanel;
    public GameObject GoodEndingPanel;
    public KuManager kuManager;
    public Transform playerTransform;
    //当前天数
    public int currenWave = 1;
    //敌人预制体
    public GameObject enemyG;
    public GameObject enemyR;
    public GameObject enemyB;
    public GameObject enemyWCG;
    public GameObject enemyBoss;
    public List<EnemyBase> enemy_List;//敌人列表
    public Transform _map;
    public EquipmentManagerInBag equipmentManagerInBag;
    public bool isPlay;

    private void Awake()
    {
        Instance=this;
        GameObject gameObject = GameObject.Find("EquipmentManagerInBag");
        equipmentManagerInBag = gameObject.GetComponent<EquipmentManagerInBag>();
    }
    public void GameStart()
    {
        waveTimer = 30;
        isPlay = true;
        if (_map == null) _map = GameObject.Find("map").transform;
        if (_failPanel == null) _failPanel = GameObject.Find("FailPanel");
        if (_successPanel == null) _successPanel = GameObject.Find("SuccessPanel");
        if (GoodEndingPanel == null) GoodEndingPanel = GameObject.Find("GoodEndingPanel");
        GameObject gameObject = GameObject.Find("player");
        playerTransform = gameObject.transform;
        //生成敌人
        GenerateEnemy();
    }
    public void GenerateEnemy()
    {
        if (isPlay == false) return;

        if (currenWave == 1) D1();
        else if (currenWave == 2) D2();
        else if (currenWave == 3) D3();
        else if (currenWave == 4) D4();
        else if (currenWave == 5) D5();
    }

    //关卡敌人生成
    public void D1()
    {
        StartCoroutine(SwawnEnemy(enemyG, 0.2f));
    }
    public void D2()
    {
        StartCoroutine(SwawnEnemy(enemyG, 0.5f));
        StartCoroutine(SwawnEnemy(enemyB, 0.6f));
    }
    public void D3()
    {
        StartCoroutine(SwawnEnemy(enemyG, 0.5f));
        StartCoroutine(SwawnEnemy(enemyR, 1f));
    }
    public void D4()
    {
        StartCoroutine(SwawnEnemy(enemyR, 0.5f));
        StartCoroutine(SwawnEnemy(enemyWCG, 0.8f));
    }
    public void D5()
    {
        StartCoroutine(SwawnEnemy(enemyR, 0.3f));
        StartCoroutine(SwawnEnemy(enemyB, 0.8f));
        StartCoroutine(SwawnEnemy(enemyBoss, 16f));
    }

    //随机位置
    private Vector3 GetRandomPosition(Bounds bounds)
    {
    restart:
        float safeDistance = 5f;
        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomY = Random.Range(bounds.min.y, bounds.max.y);
        float randomZ = 0f;
        if (Mathf.Abs(randomX - playerTransform.position.x) < safeDistance) goto restart;
        if(Mathf.Abs(randomY - playerTransform.position.y) < safeDistance) goto restart;
        return new Vector3(randomX,randomY,randomZ);
    }
    //产生敌人的协程
    IEnumerator SwawnEnemy(GameObject En,float CD)
    {
        while (waveTimer > 0 && Player.Instance.hp > 0)
        {
            yield return new WaitForSeconds(CD);

            var spawnPoint = GetRandomPosition(_map.GetComponent<SpriteRenderer>().bounds);

            EnemyBase go = Instantiate(En, spawnPoint, Quaternion.identity)
                .GetComponent<EnemyBase>();
            enemy_List.Add(go);
        }
    }

    void Update()
    {
        if (isPlay == false) return;
        if (waveTimer > 0)
        {
            waveTimer -= Time.deltaTime;
            if(waveTimer <= 0)
            {
                waveTimer = 0;

                if (isPlay == true)
                { 
                    isPlay = false;
                    LevelOver(3f,true); 
                }
            }

        }

        GamePanel.instance.RenewCountDown(waveTimer);

    }
    //清空怪物
    public void ClearMonster()
    {
        for (int i = 0; i < enemy_List.Count; i++)
        {
            if (enemy_List[i]!=null)enemy_List[i].Dead();
        }
    }
    //游戏胜利
    public void GoodEnding()
    {
        GoodEndingPanel.GetComponent<CanvasGroup>().alpha = 1;
        currenWave = 1;
        StopAllCoroutines();
        kuManager.ReSetKu();
        StartCoroutine(routine: Gomenu(5f,0));
        ClearMonster();
    }
    //天完成(next代表是否变天)
    public void LevelOver(float time, bool next)
    {
        if(next==true)currenWave++;
        if(currenWave>=6)GoodEnding();
        else
        {
            _successPanel.GetComponent<CanvasGroup>().alpha = 1;
            StopAllCoroutines();
            StartCoroutine(routine: Gomenu(time,1));
            ClearMonster();
        }
    }

    //游戏失败(毁档)
    public void BadGame()
    {

    }

    IEnumerator Gomenu(float time,int num)
    {
        equipmentManagerInBag.UnReadyForBattle();
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(num);
    }
    private void OnDestroy()
    {
        equipmentManagerInBag.UnReadyForBattle();
    }

    //前往下一关
    public void NextLevel()
    {
        Debug.Log("ToPlay");
        equipmentManagerInBag.ReadyForBattle();
        equipmentManagerInBag.UnEquipAll();
        SceneManager.LoadScene("03-GamePlay");
    }
}
