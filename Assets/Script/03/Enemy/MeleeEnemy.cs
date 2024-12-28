
using UnityEngine;

public class MeleeEnemy:EnemyBase
{
    public bool isContact = false;//是否接触到玩家

    public void Update()
    {
        Move();//移动

        //攻击
        if (isContact && !isCooling)
        {
            Attack();
        }

        //更新计时器
        if (isCooling)
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0)
            {
                attackTimer = 0;
                isCooling = false;
            }
        }


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

    //自动移动
    public void Move()
    {
        Vector2 direction = (Player.Instance.transform.position - transform.position).normalized;
        transform.Translate(direction * speed * Time.deltaTime);
    }

    //接触玩家
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

}