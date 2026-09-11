using UnityEngine;
using GameData;

public class AimBehavior_Manual : IBehavior
{
    private SingleGemWeapon weapon;
    private string horizontalAxis = "Horizontal";
    private string verticalAxis = "Vertical";

    public void OnInitialize(GameObject owner)
    {
        weapon = owner.GetComponent<SingleGemWeapon>();
        if (weapon == null)
        {
            Debug.LogError("[AimBehavior_Manual] 需要 SingleGemWeapon 组件");
        }
    }

    public void OnUpdate()
    {
        if (weapon == null) return;

        float h = Input.GetAxis(horizontalAxis);
        float v = Input.GetAxis(verticalAxis);

        Vector2 input = new Vector2(h, v);
        if (input.magnitude > 0.1f)
        {
            weapon.SetAimDirection(input.normalized);
        }
    }

    public void OnHit(GameObject target) { }
    public void OnDispose() { }
}