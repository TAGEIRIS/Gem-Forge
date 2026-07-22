using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.UI.Image;

public class EnemyShoot : MonoBehaviour
{
    public bool isCooling;//是否处于冷却

    public float AttackCD;//射击CD
    public float AttackTimer;//计时器
    public float Speed;//子弹速度
    public float Range;//射程
    public GameObject Dan;//子弹预制件
    public Transform Player;//玩家位置
    public float originZ;//原始z轴
    public RangedEnemy RangedEnemy;

    protected void Awake()
    {
        originZ = transform.eulerAngles.z;//获取原始z轴
    }
    protected void Update()
    {
        // 自动瞄准
        Aiming();

        // 判断攻击
        // 攻击冷却  
        if (isCooling)
        {
            AttackTimer += Time.deltaTime;
            if (AttackTimer >= AttackCD)
            {
                AttackTimer = 0;
                isCooling = false;
            }
        }
        else
        {
            if (RangedEnemy.isInRange==true)
            {
                Fire();
            }
        }
    }
    protected void Aiming()
    {
        // 检测范围内的玩家
        Collider2D[] playersInRange = Physics2D.OverlapCircleAll
            (transform.position, Range, LayerMask.GetMask("Player"));
        if (playersInRange.Length > 0)
        {
            RangedEnemy.isInRange = true;
            // 找到最近的Player
            Collider2D nearestPlayer = playersInRange.OrderBy(p => Vector2.Distance(transform.position, p.transform.position)).First();
            Player = nearestPlayer.transform;

            Vector2 PlayerPos = Player.position;
            Vector2 direction = PlayerPos - (Vector2)transform.position;
            float angleDegrees = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            // 设置敌人的朝向为面向玩家的方向
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, angleDegrees + originZ);
        }
        else
        {
            RangedEnemy.isInRange = false;
            Player = null;
            // 重置敌人的朝向为原始的Z轴旋转值
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, originZ);
        }
    }
    protected void Fire()
    {

        if (isCooling)
        {
            return;
        }
        if (Dan == null)
        {
            return;
        }

        Dan = Instantiate(Dan, transform.position, transform.rotation);

        Rigidbody2D rb = Dan.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector2 direction = (Player.position - transform.position).normalized;
            rb.velocity = direction * Speed;
        }

        isCooling = true;
    }
}
