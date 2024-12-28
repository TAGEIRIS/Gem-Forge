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
    public bool isCooling = false;//攻击是否冷却中
    public bool isDead=false;//是否死亡

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