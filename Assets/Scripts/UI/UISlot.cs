using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISlot : MonoBehaviour
{
    public ItemData itemdata;
    public UIInventory uiInventory;
    public int index;
    public bool selected = false;
    public bool equipped = false;
    public int quantity;

    public Button button;
    public Image icon;
    public TextMeshProUGUI quantityText;
    public GameObject equipIcon;
    private Outline outline;

    private void Awake()
    {
        outline = GetComponent<Outline>();
    }

    private void Start()
    {
        button.onClick.AddListener(SelectAndCancle);
    }

    private void Update()
    {
        OnOutline();
        OnEquipIcon();
    }

    public void SelectAndCancle()
    {
        if (itemdata != null)
        {
            if (!selected)
            {                
                foreach (UISlot slot in uiInventory.uiSlots)
                {
                    slot.selected = false;
                }
                uiInventory.SelectItem(index);
                selected = true;            
            }
            else
            {
                uiInventory.CancleItem();
                selected = false;
            }
        }       
    }

    private void OnOutline()
    {
        outline.enabled = selected;
    }

    private void OnEquipIcon()
    {
        equipIcon.SetActive(equipped);
    }

    public void SetItem()
    {
        icon.gameObject.SetActive(true);
        icon.sprite = itemdata.itemDataIcon;
        quantityText.text = quantity > 1 ? quantity.ToString() : string.Empty;

        if (outline != null) outline.enabled = selected;
    }

    public void RefreshUI()
    {
        itemdata = null;
        icon.gameObject.SetActive(false);
        quantityText.text = string.Empty;
    }
}
