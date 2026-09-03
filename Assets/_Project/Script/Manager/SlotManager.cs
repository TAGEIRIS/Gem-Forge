using System;
using System.Collections.Generic;
using UnityEngine;

public class SlotManager : MonoBehaviour
{
    // ===== 单例 =====
    private static SlotManager _instance;
    public static SlotManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SlotManager>();
                if (_instance == null)
                {
                    GameObject go = new GameObject("SlotManager");
                    _instance = go.AddComponent<SlotManager>();
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
    // UI 引用（在 Inspector 中手动拖拽）
    // ========================================

    [Header("=== UI 引用 ===")]
    [SerializeField] private GameObject _gemSlotContainer;        // 所有 GemSlotUI 的父物体
    [SerializeField] private List<EquipmentSlotUI> _equipmentSlotUIs = new List<EquipmentSlotUI>(); // 4个装备槽，手动拖拽
    [SerializeField] private DetailTextUI _detailTextUI;

    // 运行时动态获取
    private List<GemSlotUI> _gemSlotUIs = new List<GemSlotUI>();

    // ========================================
    // 初始化
    // ========================================

    public void Initialize()
    {
        // 从容器获取所有 GemSlotUI
        if (_gemSlotContainer != null)
        {
            _gemSlotUIs = new List<GemSlotUI>(_gemSlotContainer.GetComponentsInChildren<GemSlotUI>());
        }
        else
        {
            Debug.LogWarning("SlotManager: _gemSlotContainer 未绑定");
        }

        RefreshAllUI();
    }

    // ========================================
    // 交互接口（由 UI 调用）
    // ========================================

    /// <summary>
    /// 悬停宝石槽位 → 更新详情文本（由 GemSlotUI 调用）
    /// </summary>
    public void UpdateDetailTextUI(string gemId)
    {
        if (_detailTextUI == null) return;

        if (string.IsNullOrEmpty(gemId))
        {
            _detailTextUI.UpdateDisplay(null, "");
            return;
        }

        GemConfig config = GameConfig.Instance.GetGemConfigById(gemId);
        if (config != null)
        {
            _detailTextUI.UpdateDisplay(config.icon, config.itemInfo);
        }
        else
        {
            _detailTextUI.UpdateDisplay(null, "未知宝石");
        }
    }

    /// <summary>
    /// 点击宝石槽位 → 装备宝石（由 GemSlotUI 调用）
    /// </summary>
    public void equipGem(string gemId)
    {
        if (string.IsNullOrEmpty(gemId))
        {
            Debug.LogWarning("equipGem: gemId 为空");
            return;
        }

        // 检查库存是否充足
        if (GemManager.Instance.GetGemCount(gemId) <= 0)
        {
            Debug.LogWarning($"equipGem: 宝石 {gemId} 数量为 0");
            return;
        }


        // 查找空槽位
        int emptySlot = GemManager.Instance.FindEmptySlot();
        if (emptySlot == -1)
        {
            Debug.LogWarning("equipGem: 装备槽已满（4/4）");
            return;
        }

        // 执行装备：先扣库存，再装备
        GemManager.Instance.SubOwnedGems(gemId, 1);
        GemManager.Instance.EquipGem(gemId, emptySlot);

        RefreshAllUI();
    }

    /// <summary>
    /// 点击装备槽 → 卸下宝石（由 EquipmentSlotUI 调用）
    /// </summary>
    public void unequipGem(string gemId)
    {
        if (string.IsNullOrEmpty(gemId))
        {
            Debug.LogWarning("unequipGem: gemId 为空");
            return;
        }

        // 查找该宝石在哪个槽位
        int slotIndex = GemManager.Instance.FindEquippedSlot(gemId);
        if (slotIndex == -1)
        {
            Debug.LogWarning($"unequipGem: 宝石 {gemId} 未装备");
            return;
        }

        // 执行卸下：先卸下，再加回库存
        GemManager.Instance.UnequipGem(slotIndex);
        GemManager.Instance.AddOwnedGems(gemId, 1);

        RefreshAllUI();
    }

    // ========================================
    // UI 刷新
    // ========================================

    public void RefreshAllUI()
    {
        RefreshGemSlots();
        RefreshEquipmentSlots();
    }

    private void RefreshGemSlots()
    {
        if (_gemSlotUIs == null || _gemSlotUIs.Count == 0) return;

        foreach (var gemSlot in _gemSlotUIs)
        {
            if (gemSlot == null) continue;

            int count = GemManager.Instance.GetGemCount(gemSlot.gemId);
            GemConfig config = GameConfig.Instance.GetGemConfigById(gemSlot.gemId);
            Sprite icon = config != null ? config.icon : null;

            gemSlot.UpdateDisplay(icon, count);
        }
    }

    private void RefreshEquipmentSlots()
    {
        if (_equipmentSlotUIs == null || _equipmentSlotUIs.Count == 0) return;

        for (int i = 0; i < _equipmentSlotUIs.Count; i++)
        {
            if (_equipmentSlotUIs[i] == null) continue;

            string gemId = GemManager.Instance.GetEquippedGem(i);
            if (!string.IsNullOrEmpty(gemId))
            {
                GemConfig config = GameConfig.Instance.GetGemConfigById(gemId);
                Sprite icon = config != null ? config.icon : null;
                _equipmentSlotUIs[i].UpdateDisplay(gemId, icon);
            }
            else
            {
                _equipmentSlotUIs[i].UpdateDisplay(null, null);
            }
        }
    }
}