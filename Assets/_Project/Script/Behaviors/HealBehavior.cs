using UnityEngine;
using GameData;

public class HealBehavior : IBehavior
{
    private int healAmount = 5;
    private float cooldown = 1f;
    private float timer = 0f;
    private GameObject player;

    public void OnInitialize(GameObject owner)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogWarning("[HealBehavior] 未找到 Player");
        }
    }

    public void SetHealAmount(int amount)
    {
        healAmount = amount;
    }

    public void OnUpdate()
    {
        if (player == null) return;

        timer += Time.deltaTime;
        if (timer >= cooldown)
        {
            timer = 0f;
            PlayerHealth health = player.GetComponent<PlayerHealth>();
            if (health != null)
            {
                health.Heal(healAmount);
                Debug.Log($"[HealBehavior] 回复 {healAmount} 点生命值");
            }
        }
    }

    public void OnHit(GameObject target) { }
    public void OnDispose() { }
}