using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public float hp;//血量
    public float damage;//攻击力
    public float speed;//移动速度
    public float attackTime;//攻击间隔
    public float attackTimer = 0;//攻击定时器
    public bool isContact = false;//是否接触到玩家
    public bool isCooling = false;//攻击是否冷却中
    public bool isDead=false;//是否死亡

    public void Awake()
    {

    }

    public void Start()
    {

    }


    public void Update()
    {
        Move();//移动

        //攻击
        if (isContact && !isCooling)
        {
            Attack();
        }

        //更新计时器
        if(isCooling)
        {
            attackTimer -= Time.deltaTime;
            if(attackTimer <= 0)
            {
                attackTimer = 0;
                isCooling = false;
            }
        }


    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isContact = true;
        }
        else if (other.CompareTag("Dan8"))
        {
            Injured(8f);
        }
        else if (other.CompareTag("Dan4"))
        {
            Injured(4f);
        }
        else if (other.CompareTag("Dan2"))
        {
            Injured(2f);
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isContact = false;
        }
    }

    //自动移动

    public void Move()
    {
        Vector2 direction = (Player.Instance.transform.position - transform.position).normalized;
        transform.Translate(direction * speed * Time.deltaTime);
    }

    //攻击

    public void Attack()
    {
        if (isCooling)
        {
            return;
        }

        Player.Instance.Injured(damage);

        //攻击进入冷却
        isCooling = true;
        attackTimer = attackTime;

    }

    //受伤

    public void Injured(float attack)
    {

        //判断本次攻击是否会死亡
        if (hp - attack <= 0)
        {
            hp = 0;
            GamePanel.instance.RenewKillCount();
            Dead();
        }
        else
        {
            hp -= attack;
        }
    }

    //死亡

    public void Dead()
    {
        if(this!=null)Destroy(gameObject);
    }

}