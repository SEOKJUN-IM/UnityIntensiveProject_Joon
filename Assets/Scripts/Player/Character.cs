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

    [field: SerializeField] public int charCurInvenQuantity { get; set; }

    public UIInventory charInventory;
    public List<ItemData> charItemDatas;    

    private void Update()
    {
        if (this.gameObject.activeInHierarchy == true) CharacterManager.Instance.Character = this;
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

    public void SelectItem()
    {

    }

    public void Equip()
    {

    }

    public void Unequip()
    {

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
}
