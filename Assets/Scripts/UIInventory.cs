using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    public TextMeshProUGUI curQuantityText;

    public Button backBtn;

    void Awake()
    {
        UIManager.Instance.uiInven = this;
    }

    void Start()
    {
        backBtn.onClick.AddListener(backToMainMenu);
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
}
