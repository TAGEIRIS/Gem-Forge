using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    public float speed = 5f;//速度
    public Transform PlayerVisual;
    public float hp= 15f;
    public bool isDead=false;//是否死亡
    public float maxHp = 15f;//最大生命值



    private void Awake()
    {
        Instance = this;
        PlayerVisual = GameObject.Find("PlayerVisual").transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isDead) return;

        Move();
    }


    //wasd 移动
    public void Move()
    {
        float moveHcrizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        Vector2 moveMent=new Vector2(x:moveHcrizontal, y:moveVertical);

        moveMent.Normalize();
        transform.Translate(translation:(Vector3)(moveMent*speed*Time.deltaTime));

        TurnAround(moveHcrizontal);
    }

    //转头
    public void TurnAround(float h)
    {
        if (h == -1)
        {
            PlayerVisual.localScale = new Vector3(x: -1, PlayerVisual.localScale.y, PlayerVisual.localScale.z);
        }
        else if (h == 1)
        {
            PlayerVisual.localScale = new Vector3(x: 1, PlayerVisual.localScale.y, PlayerVisual.localScale.z);
        }
    }

    //受伤
    public void Injured(float attack)
    {
        if (isDead) return;

        //判断本次攻击是否会死亡
        if (hp - attack <= 0)
        {
            hp = 0;
            Dead();
        }
        else
        {
            hp -= attack;
        }
        GamePanel.instance.RenewHp();
    }

    //攻击
    public void Attack()
    {

    }


    //死亡
    public void Dead()
    {
        isDead = true;

        LevelController.Instance.BadGame();

    }


}
