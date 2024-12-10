using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : MonoBehaviour
{
    public static GamePanel instance;

    public Slider _hpSlider;
    public TMP_Text _hpCount;
    public TMP_Text _countDown;
    public TMP_Text _waveCount;
    public TMP_Text _killCount;
    public GameObject Enemy;
    public int killnumber;

    private void Awake()
    {
        instance = this;
        _hpSlider = GameObject.Find("HpSlider").GetComponent<Slider>();
        _hpCount=GameObject.Find("HpCount").GetComponent<TMP_Text>();
        _countDown = GameObject.Find("CountDown").GetComponent<TMP_Text>();
        _waveCount=GameObject.Find("WaveCount").GetComponent <TMP_Text>();
    }
    void Start()
    {
        //更新生命
        RenewHp();
        //更新波次
        RenewWaveCount();
    }

    public void RenewHp()
    {
        _hpCount.text=Player.Instance.hp+" / "+Player.Instance.maxHp;
        _hpSlider.value= Player.Instance.hp / Player.Instance.maxHp;
    }
    public void RenewKillCount()
    {
        killnumber++;
        _killCount.text = "point : " + killnumber;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RenewCountDown(float time)
    {
        _countDown.text = time.ToString(format:"F0");
    }

    public void RenewWaveCount()
    {
        _waveCount.text ="第"+ LevelController.Instance.currenWave.ToString()+"天";
    }

}
