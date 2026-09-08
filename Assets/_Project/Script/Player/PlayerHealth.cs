using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("配置")]
    [SerializeField] private int maxHp = 100;

    [Header("状态")]
    [SerializeField] private int currentHp;

    public System.Action OnDeath;
    public System.Action<int, int> OnHealthChanged;  // current, max

    private void Awake()
    {
        currentHp = maxHp;
    }

    public void TakeDamage(int damage)
    {
        if (currentHp <= 0) return;

        currentHp = Mathf.Max(0, currentHp - damage);
        OnHealthChanged?.Invoke(currentHp, maxHp);

        if (currentHp <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        if (currentHp <= 0) return;

        currentHp = Mathf.Min(maxHp, currentHp + amount);
        OnHealthChanged?.Invoke(currentHp, maxHp);
    }

    private void Die()
    {
        OnDeath?.Invoke();
    }

    public int GetCurrentHp() => currentHp;
    public int GetMaxHp() => maxHp;
    public float GetHpRatio() => (float)currentHp / maxHp;
}