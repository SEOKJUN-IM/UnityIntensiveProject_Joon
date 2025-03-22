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
    public Button changeBtn;
    public Button gameStartBtn;

    public Image curExpBar;

    void Awake()
    {
        UIManager.Instance.uiMain = this;
    }

    void Start()
    {
        statBtn.onClick.AddListener(OpenStatus);
        invenBtn.onClick.AddListener(OpenInventory);
        changeBtn.onClick.AddListener(OpenChange);
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
        if (GameManager.Instance.Player.gameObject.transform.GetChild(0).gameObject.activeInHierarchy) UIManager.Instance.uiInven01.gameObject.SetActive(true);
        else if (GameManager.Instance.Player.gameObject.transform.GetChild(1).gameObject.activeInHierarchy) UIManager.Instance.uiInven02.gameObject.SetActive(true);
    }

    public void OpenChange()
    {
        statBtn.gameObject.SetActive(false);
        invenBtn.gameObject.SetActive(false);
        UIManager.Instance.uiChange.gameObject.SetActive(true);
    }

    public void SetCharInfo()
    {
        charNameText.text = GameManager.Instance.Player.playerName;
        charJobText.text = GameManager.Instance.Player.playerJob;
        charLevelText.text = GameManager.Instance.Player.playerLevel < 10 ? $"0{GameManager.Instance.Player.playerLevel}" : $"{GameManager.Instance.Player.playerLevel}";
        charCurExpText.text = $"{GameManager.Instance.Player.playerCurExp}";
        charInfoText.text = GameManager.Instance.Player.playerInfo;
        charGoldText.text = $"{GameManager.Instance.Player.playerGold:N0}";

        curExpBar.fillAmount = GameManager.Instance.Player.playerCurExp / 100f;
    }
}
