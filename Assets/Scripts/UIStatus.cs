using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIStatus : MonoBehaviour
{
    public TextMeshProUGUI charAttackValueText;
    public TextMeshProUGUI charDefenseValueText;
    public TextMeshProUGUI charHealthValueText;
    public TextMeshProUGUI charCriticalValueText;

    public Button backBtn;

    void Awake()
    {
        UIManager.Instance.uiStat = this;
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
        charAttackValueText.text = $"{GameManager.Instance.Player.playerAttackValue}";
        charDefenseValueText.text = $"{GameManager.Instance.Player.playerDefenseValue}";
        charHealthValueText.text = $"{GameManager.Instance.Player.playerHealthValue}";
        charCriticalValueText.text = $"{GameManager.Instance.Player.playerCriticalValue}";
    }
}
