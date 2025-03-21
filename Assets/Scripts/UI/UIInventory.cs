using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    public TextMeshProUGUI curQuantityText;

    public Button backBtn;

    public UISlot uiSlotPrefab;
    public List<UISlot> uiSlots;
    public Transform content;

    [Header("Selected Item")]
    public TextMeshProUGUI selectedItemName;
    public TextMeshProUGUI selectedItemInfo;
    public TextMeshProUGUI selectedStatName;
    public TextMeshProUGUI selectedStatValue;
    public GameObject useBtn;
    public GameObject equipBtn;
    public GameObject unequipBtn;

    public List<Item> items;

    void Awake()
    {
        UIManager.Instance.uiInven = this;
    }

    void Start()
    {
        backBtn.onClick.AddListener(backToMainMenu);
        InitInventoryUI();
        CharacterManager.Instance.Character.AddItem();
    }

    void Update()
    {
        SetCharInfo();        
    }

    public void backToMainMenu()
    {
        this.gameObject.SetActive(false);
        UIManager.Instance.uiMain.OpenMainMenu();
    }

    public void SetCharInfo()
    {
        curQuantityText.text = $"{GameManager.Instance.Player.playerCurInvenQuantity}";
    }

    void InitInventoryUI()
    {
        for (int i = 0; i < 24; i++)
        {
            Instantiate(uiSlotPrefab, content);
            uiSlots.Add(content.GetChild(i).GetComponent<UISlot>());
            uiSlots[i].index = i;
            uiSlots[i].uiInventory = this;
        }
        ClearSelectedItemWindow();
    }

    void ClearSelectedItemWindow()
    {
        selectedItemName.text = string.Empty;
        selectedItemInfo.text = string.Empty;
        selectedStatName.text = string.Empty;
        selectedStatValue.text = string.Empty;
        useBtn.SetActive(false);
        equipBtn.SetActive(false);
        unequipBtn.SetActive(false);
    }
}
