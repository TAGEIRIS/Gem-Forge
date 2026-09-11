using System.Collections.Generic;
using UnityEngine;
using GameData;

public class GemWeapon : MonoBehaviour
{
    [Header("单宝石武器预制体")]
    [SerializeField] private GameObject singleGemWeaponPrefab;

    [Header("运行时状态")]
    [SerializeField] private List<SingleGemWeapon> activeWeapons = new List<SingleGemWeapon>();
    [SerializeField] private List<string> equippedGemIds = new List<string>();

    private void Start()
    {
        Initialize();
    }

    /// <summary>
    /// 初始化：为每颗装备的宝石创建独立的 SingleGemWeapon
    /// </summary>
    private void Initialize()
    {
        // 1. 从 GemManager 获取已装备的宝石 ID
        equippedGemIds = GemManager.Instance?.GetAllEquippedGems() ?? new List<string>();

        // 2. 过滤掉空字符串
        equippedGemIds.RemoveAll(id => string.IsNullOrEmpty(id));

        if (equippedGemIds.Count == 0)
        {
            Debug.Log("[GemWeapon] 没有装备任何宝石");
            return;
        }

        // 3. 为每颗宝石创建一个独立的 SingleGemWeapon
        foreach (string gemId in equippedGemIds)
        {
            CreateSingleWeapon(gemId);
        }

        Debug.Log($"[GemWeapon] 初始化完成，创建了 {activeWeapons.Count} 个单宝石武器");
    }

    /// <summary>
    /// 为单颗宝石创建独立的武器
    /// </summary>
    private void CreateSingleWeapon(string gemId)
    {
        if (singleGemWeaponPrefab == null)
        {
            Debug.LogError("[GemWeapon] singleGemWeaponPrefab 未设置");
            return;
        }

        // 从配置读取宝石信息
        GemConfig gemConfig = GameConfig.Instance.GetGemConfigById(gemId);
        if (gemConfig == null)
        {
            Debug.LogWarning($"[GemWeapon] 未找到宝石配置: {gemId}");
            return;
        }

        // 实例化单宝石武器（挂在容器下方）
        GameObject weaponObj = Instantiate(singleGemWeaponPrefab, transform);
        weaponObj.name = $"SingleWeapon_{gemId}";
        weaponObj.transform.localPosition = Vector3.zero;

        SingleGemWeapon singleWeapon = weaponObj.GetComponent<SingleGemWeapon>();
        if (singleWeapon == null)
        {
            Debug.LogError("[GemWeapon] singleGemWeaponPrefab 缺少 SingleGemWeapon 组件");
            Destroy(weaponObj);
            return;
        }

        // 初始化单宝石武器
        singleWeapon.Initialize(gemId, gemConfig);

        activeWeapons.Add(singleWeapon);
    }

    /// <summary>
    /// 停止所有武器（玩家死亡/胜利时调用）
    /// </summary>
    public void StopAllWeapons()
    {
        foreach (var weapon in activeWeapons)
        {
            if (weapon != null) weapon.Stop();
        }
    }

    /// <summary>
    /// 获取所有活跃的单宝石武器
    /// </summary>
    public List<SingleGemWeapon> GetActiveWeapons() => activeWeapons;
}