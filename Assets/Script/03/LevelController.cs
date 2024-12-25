using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public static LevelController Instance;

    public float waveTimer;
    public GameObject _failPanel;
    public GameObject _successPanel;
    public KuManager kuManager;
    //当前天数
    public int currenWave = 1;
    //敌人预制体
    public GameObject enemyG;
    public GameObject enemyR;
    public GameObject enemyB;
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
        //生成敌人
        GenerateEnemy();
    }
    public void GenerateEnemy()
    {
        if (isPlay == false) return;
        StartCoroutine(SwawnEnemy(enemyG,0.5f));
        StartCoroutine(SwawnEnemy(enemyB,1f));
        StartCoroutine(SwawnEnemy(enemyR,3f));
        StartCoroutine(SwawnEnemy(enemyB,20f));
    }

    //随机位置
    private Vector3 GetRandomPosition(Bounds bounds)
    {
        float safeDistance = 0f;
        float randomX = Random.Range(bounds.min.x + safeDistance, bounds.max.x + safeDistance);
        float randomY = Random.Range(bounds.min.y + safeDistance, bounds.max.y + safeDistance);
        float randomZ = 0f;
        return new Vector3(randomX,randomY,randomZ);
    }
    //产生敌人的协程
    IEnumerator SwawnEnemy(GameObject En,float CD)
    {
        while (waveTimer > 0 && Player.Instance.hp > 0)
        {
            yield return new WaitForSeconds(CD);

            var spawnPoint = GetRandomPosition(_map.GetComponent<SpriteRenderer>().bounds);

            EnemyBase go = Instantiate(En, spawnPoint, Quaternion.identity).GetComponent<EnemyBase>();
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
                    GoodGame(3f); 
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
    public void GoodGame(float time)
    {
        _successPanel.GetComponent<CanvasGroup>().alpha = 1;
        StopAllCoroutines();
        StartCoroutine(routine: Gomenu(time));
        ClearMonster();
    }
    //天完成

    //游戏失败
    public void BadGame(float time)
    {
        _failPanel.GetComponent<CanvasGroup>().alpha = 1;
        StopAllCoroutines();
        StartCoroutine(routine:Gomenu(time));
        ClearMonster();
    }

    IEnumerator Gomenu(float time)
    {
        equipmentManagerInBag.UnReadyForBattle();
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(1);
    }
    private void OnDestroy()
    {
        equipmentManagerInBag.UnReadyForBattle();
    }

}
