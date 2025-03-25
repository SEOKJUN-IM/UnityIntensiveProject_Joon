using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIStatus : MonoBehaviour
{
    public TextMeshProUGUI charAttackValueText;
    public TextMeshProUGUI charDefenseValueText;
    public TextMeshProUGUI charHealthValueText;
    public TextMeshProUGUI charMaxHealthValueText;
    public TextMeshProUGUI charManaValueText;
    public TextMeshProUGUI charMaxManaValueText;
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
        charMaxHealthValueText.text = $"{GameManager.Instance.Player.playerMaxHealthValue}";
        charManaValueText.text = $"{GameManager.Instance.Player.playerManaValue}";
        charMaxManaValueText.text = $"{GameManager.Instance.Player.playerMaxManaValue}";
        charCriticalValueText.text = $"{GameManager.Instance.Player.playerCriticalValue}";
    }
}
