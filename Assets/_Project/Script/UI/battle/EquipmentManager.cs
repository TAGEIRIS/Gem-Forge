using System.Collections.Generic;
using UnityEngine;
using GameData;

public class EquipmentManager : MonoBehaviour
{
    [Header("槽位位置")]
    [SerializeField] private Transform[] slotPositions = new Transform[4];  // 4个位置节点，放在玩家周围

    [Header("宝石图标预制体")]
    [SerializeField] private GameObject gemIconPrefab;  // 包含 Image 组件的预制体

    [Header("运行时状态")]
    [SerializeField] private List<GameObject> currentIcons = new List<GameObject>();

    private void Start()
    {
        RefreshDisplay();
    }

    /// <summary>
    /// 刷新装备显示（由 LevelManager 或 GemManager 调用）
    /// </summary>
    public void RefreshDisplay()
    {
        // 清除旧的图标
        ClearIcons();

        // 获取已装备的宝石 ID 列表（长度为4，空槽位为 null 或空字符串）
        List<string> equippedGems = GemManager.Instance?.GetAllEquippedGems() ?? new List<string>();

        // 补齐到4个（确保索引安全）
        while (equippedGems.Count < 4)
        {
            equippedGems.Add(null);
        }

        // 遍历4个槽位
        for (int i = 0; i < 4 && i < slotPositions.Length; i++)
        {
            string gemId = equippedGems[i];
            if (string.IsNullOrEmpty(gemId))
            {
                // 空槽位：不创建任何东西
                continue;
            }

            // 从 GameConfig 获取宝石图标
            GemConfig config = GameConfig.Instance.GetGemConfigById(gemId);
            if (config == null || config.icon == null)
            {
                Debug.LogWarning($"[EquipmentManager] 宝石 {gemId} 的配置或图标为空");
                continue;
            }

            // 实例化图标
            if (gemIconPrefab == null)
            {
                Debug.LogError("[EquipmentManager] gemIconPrefab 未设置");
                return;
            }

            GameObject icon = Instantiate(gemIconPrefab, slotPositions[i]);
            icon.transform.localPosition = Vector3.zero;
            icon.transform.localScale = Vector3.one;

            // 设置图标
            UnityEngine.UI.Image image = icon.GetComponent<UnityEngine.UI.Image>();
            if (image != null)
            {
                image.sprite = config.icon;
            }

            currentIcons.Add(icon);
        }

        Debug.Log($"[EquipmentManager] 刷新完成，显示了 {currentIcons.Count} 个宝石图标");
    }

    /// <summary>
    /// 清除所有图标
    /// </summary>
    private void ClearIcons()
    {
        foreach (GameObject icon in currentIcons)
        {
            if (icon != null)
            {
                Destroy(icon);
            }
        }
        currentIcons.Clear();
    }

    /// <summary>
    /// 获取某个槽位的宝石ID（外部查询用）
    /// </summary>
    public string GetGemAtSlot(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= 4) return null;
        var equippedGems = GemManager.Instance?.GetAllEquippedGems() ?? new List<string>();
        if (slotIndex < equippedGems.Count)
        {
            return equippedGems[slotIndex];
        }
        return null;
    }
}