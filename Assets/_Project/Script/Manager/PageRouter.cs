using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PageRouter : MonoBehaviour
{
    // ===== 单例 =====
    private static PageRouter _instance;
    public static PageRouter Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<PageRouter>();
                if (_instance == null)
                {
                    GameObject go = new GameObject("PageRouter");
                    _instance = go.AddComponent<PageRouter>();
                }
            }
            return _instance;
        }
    }

    [Header("页面列表")]
    public List<GameObject> Pages = new List<GameObject>();
    public Button tonightButton;

    public GameObject UnWeaponButtons;

    void Awake()
    {
        tonightButton.onClick.AddListener(()=>GameManager.Instance.StartBattle());
    }

    public void Initialize(GameSaveData data)
    {
        Debug.Log("fuck");
        Transfer("MainCanvas");
    }

    public void Transfer(string pageName)
    {
        foreach (var page in Pages)
        {
            page.SetActive(false);
        }

        foreach (var page in Pages)
        {
            if (page.name == pageName)
            {
                page.SetActive(true);
                Debug.Log($"切换到页面：{pageName}");
                return;
            }
        }

        Debug.LogWarning($"未找到名为 {pageName} 的页面");
    }

}
