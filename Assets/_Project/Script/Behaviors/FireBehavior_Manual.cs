using UnityEngine;
using GameData;

public class FireBehavior_Manual : IBehavior
{
    private SingleGemWeapon weapon;
    private string fireButton = "Fire1";
    private float fireCooldown = 0.2f;

    public void OnInitialize(GameObject owner)
    {
        weapon = owner.GetComponent<SingleGemWeapon>();
        if (weapon == null)
        {
            Debug.LogError("[FireBehavior_Manual] 需要 SingleGemWeapon 组件");
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

        if (Input.GetButtonDown(fireButton) && weapon.IsReadyToFire())
        {
            if (weapon.GetAimDirection().magnitude > 0.1f)
            {
                weapon.Fire();
                weapon.SetCooldown(fireCooldown);
            }
        }
    }

    public void OnHit(GameObject target) { }
    public void OnDispose() { }
}