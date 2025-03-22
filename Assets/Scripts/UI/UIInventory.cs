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
    public Button useBtn;
    public Button equipBtn;
    public Button unequipBtn;

    public List<Item> items;

    ItemData selectedItem;
    int selectedItemIndex;

    void Awake()
    {
        UIManager.Instance.uiInven = this;
    }

    void Start()
    {
        backBtn.onClick.AddListener(backToMainMenu);
        useBtn.onClick.AddListener(OnUseBtn);
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
        useBtn.gameObject.SetActive(false);
        equipBtn.gameObject.SetActive(false);
        unequipBtn.gameObject.SetActive(false);
    }

    public void SelectItem(int index)
    {
        if (uiSlots[index].itemdata == null) return;

        selectedItem = uiSlots[index].itemdata;
        selectedItemIndex = index;

        selectedItemName.text = selectedItem.itemDataName;
        selectedItemInfo.text = selectedItem.itemDataInfo;
        selectedStatName.text = string.Empty;
        selectedStatValue.text = string.Empty;

        if (selectedItem.itemDataType == ItemType.Consumable)
        {
            for (int i = 0; i < selectedItem.consumables.Length; i++)
            {
                selectedStatName.text += selectedItem.consumables[i].consumableType.ToString();
                selectedStatValue.text += "+" + selectedItem.consumables[i].itemValue.ToString();
            }
        }

        if (selectedItem.itemDataType == ItemType.Equipable)
        {
            for (int i = 0; i < selectedItem.equipables.Length; i++)
            {
                selectedStatName.text += selectedItem.equipables[i].equipableType.ToString();
                selectedStatValue.text += "+" + selectedItem.equipables[i].itemValue.ToString();
            }
        }

        useBtn.gameObject.SetActive(selectedItem.itemDataType == ItemType.Consumable);
        equipBtn.gameObject.SetActive(selectedItem.itemDataType == ItemType.Equipable && !uiSlots[index].equipped);
        unequipBtn.gameObject.SetActive(selectedItem.itemDataType == ItemType.Equipable && uiSlots[index].equipped);
    }

    public void CancleItem()
    {
        selectedItem = null;
        selectedItemIndex = -1;
        ClearSelectedItemWindow();
    }

    public void OnUseBtn()
    {
        if (selectedItem.itemDataType == ItemType.Consumable)
        {
            for (int i = 0; i < selectedItem.consumables.Length; i++)
            {
                switch (selectedItem.consumables[i].consumableType)
                {                    
                    case ConsumableType.Health:
                        CharacterManager.Instance.Character.charHealthValue += selectedItem.consumables[i].itemValue;
                        break;
                    case ConsumableType.Critical:
                        CharacterManager.Instance.Character.charCriticalValue += selectedItem.consumables[i].itemValue;
                        break;
                }
            }
            RemoveSelectedItem();
        }
    }

    void RemoveSelectedItem()
    {
        uiSlots[selectedItemIndex].quantity--;
        if (uiSlots[selectedItemIndex].quantity <= 0)
        {
            selectedItem = null;
            uiSlots[selectedItemIndex].itemdata = null;
            uiSlots[selectedItemIndex].selected = false;
            CharacterManager.Instance.Character.charItemDatas[selectedItemIndex] = null;
            selectedItemIndex = -1;
            ClearSelectedItemWindow();                        
        }
        CharacterManager.Instance.Character.UpdateUI();
    }    
}
