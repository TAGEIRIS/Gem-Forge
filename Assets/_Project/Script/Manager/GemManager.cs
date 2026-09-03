using System.Collections.Generic;
using UnityEngine;

public class GemManager : MonoBehaviour
{
    // ===== 单例 =====
    private static GemManager _instance;
    public static GemManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GemManager>();
                if (_instance == null)
                {
                    GameObject go = new GameObject("GemManager");
                    _instance = go.AddComponent<GemManager>();
                    DontDestroyOnLoad(go);
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // ========================================
    // 库存操作
    // ========================================

    public int GetGemCount(string gemId)
    {
        if (string.IsNullOrEmpty(gemId)) return 0;

        var runData = SaveManager.Instance.SaveData.runData;
        GemOwnership gem = runData.ownedGems.Find(g => g.gemId == gemId);
        return gem != null ? gem.count : 0;
    }

    public void AddOwnedGems(string gemId, int num)
    {
        if (num <= 0)
        {
            Debug.LogWarning($"AddOwnedGems: 数量 ({num}) 必须为正数");
            return;
        }

        if (GameConfig.Instance.GetGemConfigById(gemId) == null)
        {
            Debug.LogError($"宝石 {gemId} 不存在于配置表中");
            return;
        }

        var runData = SaveManager.Instance.SaveData.runData;
        GemOwnership existing = runData.ownedGems.Find(g => g.gemId == gemId);

        if (existing == null)
        {
            runData.ownedGems.Add(new GemOwnership { gemId = gemId, count = Mathf.Min(num, 99) });
        }
        else
        {
            existing.count = Mathf.Min(existing.count + num, 99);
        }

        SaveManager.Instance.SaveGame();
    }

    public void SubOwnedGems(string gemId, int num)
    {
        if (num <= 0)
        {
            Debug.LogWarning($"SubOwnedGems: 数量 ({num}) 必须为正数");
            return;
        }

        if (GameConfig.Instance.GetGemConfigById(gemId) == null)
        {
            Debug.LogError($"宝石 {gemId} 不存在于配置表中");
            return;
        }

        var runData = SaveManager.Instance.SaveData.runData;
        GemOwnership existing = runData.ownedGems.Find(g => g.gemId == gemId);

        if (existing == null)
        {
            Debug.LogError($"宝石 {gemId} 数量为 0，无法扣除");
            return;
        }

        existing.count = Mathf.Max(existing.count - num, 0);
        if (existing.count == 0)
        {
            runData.ownedGems.Remove(existing);
        }

        SaveManager.Instance.SaveGame();
    }

    // ========================================
    // 装备操作
    // ========================================

    /// <summary>
    /// 获取指定槽位的宝石 ID
    /// </summary>
    public string GetEquippedGem(int slotIndex)
    {
        var runData = SaveManager.Instance.SaveData.runData;
        if (slotIndex < 0 || slotIndex >= runData.equippedGems.Count) return null;
        return runData.equippedGems[slotIndex];
    }

    /// <summary>
    /// 查找某宝石所在的槽位索引
    /// </summary>
    public int FindEquippedSlot(string gemId)
    {
        if (string.IsNullOrEmpty(gemId)) return -1;

        var runData = SaveManager.Instance.SaveData.runData;
        for (int i = 0; i < runData.equippedGems.Count; i++)
        {
            if (runData.equippedGems[i] == gemId)
            {
                return i;
            }
        }
        return -1;
    }

    /// <summary>
    /// 查找第一个空槽位
    /// </summary>
    /// <returns>空槽位索引，-1 表示已满</returns>
    public int FindEmptySlot()
    {
        var runData = SaveManager.Instance.SaveData.runData;
        if (runData.equippedGems.Count < 4)
        {
            return runData.equippedGems.Count;
        }
        return -1;
    }

    /// <summary>
    /// 检查某宝石是否已装备
    /// </summary>
    public bool IsGemEquipped(string gemId)
    {
        if (string.IsNullOrEmpty(gemId)) return false;
        var runData = SaveManager.Instance.SaveData.runData;
        return runData.equippedGems.Contains(gemId);
    }

    /// <summary>
    /// 装备宝石到指定槽位
    /// </summary>
    public void EquipGem(string gemId, int slotIndex)
    {
        var runData = SaveManager.Instance.SaveData.runData;

        if (slotIndex < 0 || slotIndex > 4)
        {
            Debug.LogError($"EquipGem: 无效槽位 {slotIndex}");
            return;
        }

        if (slotIndex == 4)
        {
            Debug.LogError("EquipGem: 装备槽已满");
            return;
        }

        if (slotIndex < runData.equippedGems.Count)
        {
            Debug.LogError($"EquipGem: 槽位 {slotIndex} 已被占用");
            return;
        }

        if (slotIndex != runData.equippedGems.Count)
        {
            Debug.LogError($"EquipGem: 槽位不连续，当前数量 {runData.equippedGems.Count}，目标槽位 {slotIndex}");
            return;
        }

        runData.equippedGems.Add(gemId);
        SaveManager.Instance.SaveGame();
        Debug.Log($"✅ EquipGem: 将 {gemId} 装备到槽位 {slotIndex}");
    }

    /// <summary>
    /// 从指定槽位卸下宝石
    /// </summary>
    public void UnequipGem(int slotIndex)
    {
        var runData = SaveManager.Instance.SaveData.runData;

        if (slotIndex < 0 || slotIndex >= runData.equippedGems.Count)
        {
            Debug.LogError($"UnequipGem: 无效槽位 {slotIndex}");
            return;
        }

        string gemId = runData.equippedGems[slotIndex];
        runData.equippedGems.RemoveAt(slotIndex);
        SaveManager.Instance.SaveGame();
        Debug.Log($"✅ UnequipGem: 从槽位 {slotIndex} 卸下 {gemId}");
    }
}