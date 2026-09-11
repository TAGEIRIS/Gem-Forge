using UnityEngine;
using GameData;

public class FireBehavior_Auto : IBehavior
{
    private SingleGemWeapon weapon;
    private float fireCooldown = 0.3f;

    public void OnInitialize(GameObject owner)
    {
        weapon = owner.GetComponent<SingleGemWeapon>();
        if (weapon == null)
        {
            Debug.LogError("[FireBehavior_Auto] 需要 SingleGemWeapon 组件");
        }
    }

    public void OnUpdate()
    {
        if (weapon == null) return;

        float cd = weapon.GetCooldown();
        if (cd > 0f)
        {
            cd -= Time.deltaTime;
            weapon.SetCooldown(cd);
        }

        if (weapon.IsReadyToFire() && weapon.GetAimDirection().magnitude > 0.1f)
        {
            weapon.Fire();
            weapon.SetCooldown(fireCooldown);
        }
    }

    public void OnHit(GameObject target) { }
    public void OnDispose() { }
}