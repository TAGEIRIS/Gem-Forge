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
    //结束时弹出面板
    public GameObject _failPanel;
    public GameObject _successPanel;
    public GameObject GoodEndingPanel;
    public KuManager kuManager;
    public Transform playerTransform;
    //当前天数
    public int currenWave;
    public List<EnemyBase> enemy_List;//敌人列表
    public EquipmentManagerInBag equipmentManagerInBag;
    public bool isPlay;

    //选中地图
    public string NowMap;
    //地图加载器
    public MapLoader mapLoader;


    private void Awake()
    {
        if(Instance==null)Instance=this;
        GameObject gameObject = GameObject.Find("EquipmentManagerInBag");
        equipmentManagerInBag = gameObject.GetComponent<EquipmentManagerInBag>();
        SceneManager.sceneLoaded += (_,_) => { currenWave = PlayerPrefs.GetInt("当前游戏天数", 1); };
    }

    public void GameStart()
    {
       
        waveTimer = 30;
        isPlay = true;
        if (_failPanel == null) _failPanel = GameObject.Find("FailPanel");
        if (_successPanel == null) _successPanel = GameObject.Find("SuccessPanel");
        if (GoodEndingPanel == null) GoodEndingPanel = GameObject.Find("GoodEndingPanel");
        GameObject gameObject = GameObject.Find("player");
        playerTransform = gameObject.transform;

        NowMap = PlayerPrefs.GetString("当前地图", "Meadow");
        mapLoader = GameObject.Find("maps").GetComponent<MapLoader>();
        mapLoader.GameStart();
    }

    //分配随机位置
    public Vector3 GetRandomPosition(Bounds bounds)
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
                    LevelOver(3f, true);
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
        PlayerPrefs.DeleteAll();
        StopAllCoroutines();
        kuManager.ReSetKu();
        StartCoroutine(routine: Gomenu(5f,0));
        ClearMonster();
    }
    //天完成(next代表是否变天)
    public void LevelOver(float time, bool next)
    {
        if(next==true)currenWave++;
        if(currenWave>=16)GoodEnding();
        else
        {
            if (currenWave <= 5) NowMap = "Meadow";
            else if (currenWave <= 10) NowMap = "Temple";
            else if (currenWave <= 15) NowMap = "City";
            PlayerPrefs.SetString("当前地图",NowMap);

            PlayerPrefs.SetInt("当前游戏天数", currenWave);
            _successPanel.GetComponent<CanvasGroup>().alpha = 1;
            StopAllCoroutines();
            StartCoroutine(routine: Gomenu(time,1));
            ClearMonster();
        }
    }

    //游戏失败(毁档)
    public void BadGame()
    {
        _failPanel.GetComponent<CanvasGroup>().alpha = 1;
        currenWave = 1;
        PlayerPrefs.DeleteAll();
        StopAllCoroutines();
        kuManager.ReSetKu();
        StartCoroutine(routine: Gomenu(5f, 0));
        ClearMonster();
    }

    //返回村庄
    IEnumerator Gomenu(float time,int num)
    {
        equipmentManagerInBag.UnReadyForBattle();
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(num);
    }

    //前往异界
    public void NextLevel()
    {
        Debug.Log("ToPlay");
        equipmentManagerInBag.ReadyForBattle();
        equipmentManagerInBag.UnEquipAll();
        SceneManager.LoadScene("03-GamePlay");
    }
    private void OnDestroy()
    {
        equipmentManagerInBag.UnReadyForBattle();
        SceneManager.sceneLoaded -= (_, _) => { currenWave = PlayerPrefs.GetInt("当前游戏天数", 1); };
    }

}
