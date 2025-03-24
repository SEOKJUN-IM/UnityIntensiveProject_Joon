using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    public TextMeshProUGUI curQuantityText;
    public TextMeshProUGUI useWarningText;

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
    
    public bool alreadyUsedAttackConsumable = false;

    void Awake()
    {
        InitInventoryUI();
    }

    void Start()
    {
        if (this.gameObject.name == "UIInventory01") GameManager.Instance.SetItems01();
        else if (this.gameObject.name == "UIInventory02") GameManager.Instance.SetItems02();

        backBtn.onClick.AddListener(backToMainMenu);
        useBtn.onClick.AddListener(OnUseBtn);
        equipBtn.onClick.AddListener(OnEquipBtn);
        unequipBtn.onClick.AddListener(OnUnequipBtn);
        
        if (CharacterManager.Instance.Character != null) CharacterManager.Instance.Character.AddItem();
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
                
                if (selectedItem.consumables[i].consumableType == ConsumableType.Attack) selectedStatValue.text += "%";                
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

    void OffUseWarningText()
    {
        useWarningText.text = string.Empty;
        useWarningText.gameObject.SetActive(false);
    }

    public void AddHealthItemValue(ItemDataConsumable selectedItemDataConsumable)
    {
        if (CharacterManager.Instance.Character.charHealthValue == 100)
        {
            useWarningText.gameObject.SetActive(true);
            useWarningText.text = "체력이 이미 최대치입니다.\n[ 아이템 사용 불가 ]";
            Invoke("OffUseWarningText", 2f);
        }
        else
        {
            CharacterManager.Instance.Character.charHealthValue = Mathf.Min(CharacterManager.Instance.Character.charHealthValue + selectedItemDataConsumable.itemValue, 100);
            RemoveSelectedItem();
        }        
    }

    public void AddManaItemValue(ItemDataConsumable selectedItemDataConsumable)
    {
        if (CharacterManager.Instance.Character.charManaValue == 100)
        {
            useWarningText.gameObject.SetActive(true);
            useWarningText.text = "마나가 이미 최대치입니다.\n[ 아이템 사용 불가 ]";
            Invoke("OffUseWarningText", 2f);
        }
        else
        {
            CharacterManager.Instance.Character.charManaValue = Mathf.Min(CharacterManager.Instance.Character.charManaValue + selectedItemDataConsumable.itemValue, 100);
            RemoveSelectedItem();
        }
    }

    public void AddAttackItemValue(ItemDataConsumable selectedItemDataConsumable)
    {
        if (alreadyUsedAttackConsumable)
        {
            useWarningText.gameObject.SetActive(true);
            useWarningText.text = "동일 아이템의 효과가\n아직 끝나지 않았습니다.\n[ 아이템 사용 불가 ]";
            Invoke("OffUseWarningText", 2f);
        }
        else
        {        
            alreadyUsedAttackConsumable = true;

            CharacterManager.Instance.Character.charAttackValue += (int)(CharacterManager.Instance.Character.charAttackValue * 0.1f);
            RemoveSelectedItem();
            Invoke("ResetAttackValue", selectedItemDataConsumable.itemDuration);
        }
    }

    public void ResetAttackValue()
    {
        CharacterManager.Instance.Character.charAttackValue -= (int)(CharacterManager.Instance.Character.charAttackValue * 0.1f);
        alreadyUsedAttackConsumable = false;
    }

    public void AddCriticalItemValue(ItemDataConsumable selectedItemDataConsumable)
    {
        if (CharacterManager.Instance.Character.charCriticalValue == 100)
        {
            useWarningText.gameObject.SetActive(true);
            useWarningText.text = "치명타 확률이 이미 최대치입니다.\n[ 아이템 사용 불가 ]";
            Invoke("OffUseWarningText", 2f);
        }
        else
        {
            CharacterManager.Instance.Character.charCriticalValue = Mathf.Min(CharacterManager.Instance.Character.charCriticalValue + selectedItemDataConsumable.itemValue, 100);
            RemoveSelectedItem();
        }        
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
                        AddHealthItemValue(selectedItem.consumables[i]);
                        break;
                    case ConsumableType.Mana:
                        AddManaItemValue(selectedItem.consumables[i]);
                        break;
                    case ConsumableType.Attack:
                        AddAttackItemValue(selectedItem.consumables[i]);
                        break;
                    case ConsumableType.Critical:
                        AddCriticalItemValue(selectedItem.consumables[i]);
                        break;
                }
            }            
        }
    }

    public void OnEquipBtn()
    {
        if (selectedItem.itemDataType == ItemType.Equipable)
        {
            for (int i = 0; i < selectedItem.equipables.Length; i++)
            {
                switch (selectedItem.equipables[i].equipableType)
                {
                    case EquipableType.Attack:
                        CharacterManager.Instance.Character.charAttackValue += selectedItem.equipables[i].itemValue;
                        break;
                    case EquipableType.Defense:
                        CharacterManager.Instance.Character.charDefenseValue += selectedItem.equipables[i].itemValue;
                        break;
                }
            }
            uiSlots[selectedItemIndex].equipped = true;
            uiSlots[selectedItemIndex].equipIcon.SetActive(true);
            equipBtn.gameObject.SetActive(false);
            unequipBtn.gameObject.SetActive(true);
        }
    }

    public void OnUnequipBtn()
    {
        if (selectedItem.itemDataType == ItemType.Equipable)
        {
            for (int i = 0; i < selectedItem.equipables.Length; i++)
            {
                switch (selectedItem.equipables[i].equipableType)
                {
                    case EquipableType.Attack:
                        CharacterManager.Instance.Character.charAttackValue -= selectedItem.equipables[i].itemValue;
                        break;
                    case EquipableType.Defense:
                        CharacterManager.Instance.Character.charDefenseValue -= selectedItem.equipables[i].itemValue;
                        break;
                }
            }
            uiSlots[selectedItemIndex].equipped = false;
            uiSlots[selectedItemIndex].equipIcon.SetActive(false);
            equipBtn.gameObject.SetActive(true);
            unequipBtn.gameObject.SetActive(false);
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
