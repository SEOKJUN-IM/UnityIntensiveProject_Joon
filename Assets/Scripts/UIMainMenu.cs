using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    public TextMeshProUGUI charNameText;
    public TextMeshProUGUI charJobText;
    public TextMeshProUGUI charLevelText;
    public TextMeshProUGUI charCurExpText;
    public TextMeshProUGUI charInfoText;
    public TextMeshProUGUI charGoldText;

    public Button statBtn;
    public Button invenBtn;

    void Awake()
    {
        UIManager.Instance.uiMain = this;
    }

    void Start()
    {
        statBtn.onClick.AddListener(OpenStatus);
        invenBtn.onClick.AddListener(OpenInventory);        
    }

    void Update()
    {
        SetCharInfo();
    }

    public void OpenMainMenu()
    {
        statBtn.gameObject.SetActive(true);
        invenBtn.gameObject.SetActive(true);
    }

    public void OpenStatus()
    {
        statBtn.gameObject.SetActive(false);
        invenBtn.gameObject.SetActive(false);
        UIManager.Instance.uiStat.gameObject.SetActive(true);
    }

    public void OpenInventory()
    {
        statBtn.gameObject.SetActive(false);
        invenBtn.gameObject.SetActive(false);
        UIManager.Instance.uiInven.gameObject.SetActive(true);
    }

    public void SetCharInfo()
    {
        charNameText.text = GameManager.Instance.Player.playerName;
        charJobText.text = GameManager.Instance.Player.playerJob;
        charLevelText.text = GameManager.Instance.Player.playerLevel < 10 ? $"0{GameManager.Instance.Player.playerLevel}" : $"{GameManager.Instance.Player.playerLevel}";
        charCurExpText.text = $"{GameManager.Instance.Player.playerCurExp}";
        charInfoText.text = GameManager.Instance.Player.playerInfo;
        charGoldText.text = $"{GameManager.Instance.Player.playerGold:N0}";
    }
}
