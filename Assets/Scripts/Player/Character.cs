using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [field: SerializeField] public string charName { get; private set; }
    [field: SerializeField] public string charJob { get; private set; }
    [field: SerializeField] public int charLevel { get; private set; }
    [field: SerializeField] public int charCurExp { get; private set; }
    [field: SerializeField, TextArea] public string charInfo { get; private set; }
    [field: SerializeField] public int charGold { get; private set; }

    [field: SerializeField] public int charAttackValue { get; set; }
    [field: SerializeField] public int charDefenseValue { get; set; }
    [field: SerializeField] public int charHealthValue { get; set; }
    [field: SerializeField] public int charCriticalValue { get; set; }

    public int charCurInvenQuantity = 0;

    public UIInventory charInventory;
    public List<ItemData> charItemDatas;
    public List<GameObject> charEquipments; 

    private void Update()
    {
        if (this.gameObject.activeInHierarchy == true) CharacterManager.Instance.Character = this;
        CalCurInvenQuantity();
        Equip();
        Unequip();
    }

    public void AddItem()
    {
        for (int i = 0; i < charInventory.items.Count; i++)
        {            
            charItemDatas.Add(charInventory.items[i].itemData);
        }

        var dupItemDataName = charItemDatas.GroupBy(x => x).Where(g => g.Count() > 1).Select(x => new { Element = x.Key }).ToList();

        var dupItemDataCount = charItemDatas.GroupBy(x => x).Where(g => g.Count() > 1).Select(x => new { Count = x.Count() }).ToList();

        charItemDatas = charItemDatas.Distinct().ToList();            

        for (int i = 0; i < charItemDatas.Count; i++)
        {
            UISlot emptySlot = GetEmptySlot();
            if (emptySlot != null)
            {
                emptySlot.itemdata = charItemDatas[i];
                emptySlot.quantity = 1;                
                UpdateUI();
            }
        }

        for (int i = 0; i < charInventory.uiSlots.Count; i++)
        {
            for (int j = 0; j < dupItemDataName.Count; j++)
            {
                if (charInventory.uiSlots[i].itemdata == dupItemDataName[j].Element) charInventory.uiSlots[i].quantity = dupItemDataCount[j].Count;
            }
            UpdateUI();
        }                        
    }

    public void Equip()
    {
        //  uiSlot.equipped true고 uiSlots와 charEquipments의 itemdata 비교하여 일치하면 그 게임오브젝트 켠다      
        for (int i = 0; i < charInventory.uiSlots.Count; i++)
        {
            for (int j = 0; j < charEquipments.Count; j++)
            {
                if (charInventory.uiSlots[i].equipped && charInventory.uiSlots[i].itemdata == charEquipments[j].GetComponent<Item>().itemData)
                {
                    charEquipments[j].gameObject.SetActive(true);
                }
            }            
        }        
    }

    public void Unequip()
    {
        //  uiSlot.equipped false고 uiSlots와 charEquipments의 itemdata 비교하여 일치하면 그 게임오브젝트 끈다     
        for (int i = 0; i < charInventory.uiSlots.Count; i++)
        {
            for (int j = 0; j < charEquipments.Count; j++)
            {
                if (!charInventory.uiSlots[i].equipped && charInventory.uiSlots[i].itemdata == charEquipments[j].GetComponent<Item>().itemData)
                {
                    charEquipments[j].gameObject.SetActive(false);
                }
            }
        }
    }

    public void UpdateUI()
    {
        for (int i = 0; i < charInventory.uiSlots.Count; i++)
        {
            if (charInventory.uiSlots[i].itemdata != null) charInventory.uiSlots[i].SetItem();
            else charInventory.uiSlots[i].RefreshUI();
        }
        
    }

    UISlot GetItemStack(ItemData itemData)
    {
        for (int i = 0; i < charInventory.uiSlots.Count; i++)
        {
            if (charInventory.uiSlots[i] == itemData && charInventory.uiSlots[i].quantity < itemData.maxStackAmount)
            {
                return charInventory.uiSlots[i];
            }            
        }
        return null;
    }

    UISlot GetEmptySlot()
    {
        for (int i = 0; i < charInventory.uiSlots.Count; i++)
        {
            if (charInventory.uiSlots[i].itemdata == null)
            {
                return charInventory.uiSlots[i];
            }
        }
        return null;
    }

    void CalCurInvenQuantity()
    {
        charCurInvenQuantity = CharacterManager.Instance.Character.charItemDatas.Count;
        for (int i = 0; i < CharacterManager.Instance.Character.charItemDatas.Count; i++)
        {
            if (CharacterManager.Instance.Character.charItemDatas[i] == null)
            {
                charCurInvenQuantity--;
            }
        }        
    }
}
