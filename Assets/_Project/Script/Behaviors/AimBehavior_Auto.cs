using UnityEngine;
using GameData;

public class AimBehavior_Auto : IBehavior
{
    private SingleGemWeapon weapon;
    private float searchRadius = 20f;

    public void OnInitialize(GameObject owner)
    {
        weapon = owner.GetComponent<SingleGemWeapon>();
        if (weapon == null)
        {
            Debug.LogError("[AimBehavior_Auto] 需要 SingleGemWeapon 组件");
        }
    }

    public void OnUpdate()
    {
        if (weapon == null) return;

        // 搜索最近的敌人
        Collider2D[] hits = Physics2D.OverlapCircleAll(weapon.GetFirePoint().position, searchRadius);
        Transform nearest = null;
        float nearestDist = float.MaxValue;

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                float dist = Vector2.Distance(weapon.GetFirePoint().position, hit.transform.position);
                if (dist < nearestDist)
                {
                    nearestDist = dist;
                    nearest = hit.transform;
                }
            }
        }

        if (nearest != null)
        {
            Vector2 direction = (nearest.position - weapon.GetFirePoint().position).normalized;
            weapon.SetAimDirection(direction);
        }
    }

    public void OnHit(GameObject target) { }
    public void OnDispose() { }
}