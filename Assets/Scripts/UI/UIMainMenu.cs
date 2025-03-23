using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    public GameObject charInfoWindow;

    public TextMeshProUGUI charNameText;
    public TextMeshProUGUI charJobText;
    public TextMeshProUGUI charLevelText;
    public TextMeshProUGUI charCurExpText;
    public TextMeshProUGUI charInfoText;
    public TextMeshProUGUI charGoldText;

    public Button statBtn;
    public Button invenBtn;
    public Button changeBtn;
    public Button stageBtn;
    public Button gameStartBtn;

    public GameObject stageBtnText;
    public TextMeshProUGUI curStageText;
    public GameObject backToMainText;

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
        stageBtn.onClick.AddListener(OpenStage);
        gameStartBtn.onClick.AddListener(GameManager.Instance.GameStart);
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
        if (!UIManager.Instance.uiStat.gameObject.activeInHierarchy) UIManager.Instance.uiStat.gameObject.SetActive(true);
        else UIManager.Instance.uiStat.gameObject.SetActive(false);
        UIManager.Instance.uiInven01.gameObject.SetActive(false);
        UIManager.Instance.uiInven02.gameObject.SetActive(false);
        UIManager.Instance.uiChange.gameObject.SetActive(false);
    }

    public void OpenInventory()
    {        
        if (GameManager.Instance.Player.gameObject.transform.GetChild(0).gameObject.activeInHierarchy)
        {
            if (!UIManager.Instance.uiInven01.gameObject.activeInHierarchy)
                UIManager.Instance.uiInven01.gameObject.SetActive(true);
            else
                UIManager.Instance.uiInven01.gameObject.SetActive(false);
        }
        else if (GameManager.Instance.Player.gameObject.transform.GetChild(1).gameObject.activeInHierarchy)
        {
            if (!UIManager.Instance.uiInven02.gameObject.activeInHierarchy)
                UIManager.Instance.uiInven02.gameObject.SetActive(true);
            else
                UIManager.Instance.uiInven02.gameObject.SetActive(false);
        }

        UIManager.Instance.uiStat.gameObject.SetActive(false);
        UIManager.Instance.uiChange.gameObject.SetActive(false);
    }

    public void OpenChange()
    {        
        if (!UIManager.Instance.uiChange.gameObject.activeInHierarchy) UIManager.Instance.uiChange.gameObject.SetActive(true);
        else UIManager.Instance.uiChange.gameObject.SetActive(false);
        UIManager.Instance.uiStat.gameObject.SetActive(false);
        UIManager.Instance.uiInven01.gameObject.SetActive(false);
        UIManager.Instance.uiInven02.gameObject.SetActive(false);
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

    public void OpenStage()
    {
        if (!UIManager.Instance.uiStage.gameObject.activeInHierarchy)
        {        
            charInfoWindow.gameObject.SetActive(false);

            statBtn.gameObject.transform.localScale = Vector3.one * 0.7f;
            invenBtn.gameObject.transform.localScale = Vector3.one * 0.7f;
            changeBtn.gameObject.transform.localScale = Vector3.one * 0.7f;

            statBtn.gameObject.transform.localPosition = new Vector3(840f, -310f, 0f);
            invenBtn.gameObject.transform.localPosition = new Vector3(840f, -395f, 0f);
            changeBtn.gameObject.transform.localPosition = new Vector3(840f, -480f, 0f);

            stageBtnText.SetActive(false);
            curStageText.gameObject.SetActive(false);
            backToMainText.SetActive(true);

            GameManager.Instance.gameCamera.transform.position += Vector3.right * 4;
            GameManager.Instance.Player.transform.localEulerAngles = new Vector3(0f, -30f, 0f);

            UIManager.Instance.uiStage.gameObject.SetActive(true);
        }
        else
        {
            charInfoWindow.gameObject.SetActive(true);

            statBtn.gameObject.transform.localScale = Vector3.one;
            invenBtn.gameObject.transform.localScale = Vector3.one;
            changeBtn.gameObject.transform.localScale = Vector3.one;

            statBtn.gameObject.transform.localPosition = new Vector3(675f, 120f, 0f);
            invenBtn.gameObject.transform.localPosition = new Vector3(675f, 0f, 0f);
            changeBtn.gameObject.transform.localPosition = new Vector3(675f, -120f, 0f);

            stageBtnText.SetActive(true);
            curStageText.gameObject.SetActive(true);
            backToMainText.SetActive(false);

            GameManager.Instance.gameCamera.transform.position -= Vector3.right * 4;
            GameManager.Instance.Player.transform.localEulerAngles = Vector3.zero;

            UIManager.Instance.uiStage.gameObject.SetActive(false);
        }       
    }

    public void ResetGameScene()
    {
        if(UIManager.Instance.uiStage.gameObject.activeInHierarchy) UIManager.Instance.uiStage.gameObject.SetActive(false);

        stageBtn.gameObject.SetActive(false);
        gameStartBtn.gameObject.SetActive(false);
        charInfoWindow.gameObject.SetActive(false);

        statBtn.gameObject.transform.localScale = Vector3.one * 0.7f;
        invenBtn.gameObject.transform.localScale = Vector3.one * 0.7f;
        changeBtn.gameObject.transform.localScale = Vector3.one * 0.7f;

        statBtn.gameObject.transform.localPosition = new Vector3(840f, -310f, 0f);
        invenBtn.gameObject.transform.localPosition = new Vector3(840f, -395f, 0f);
        changeBtn.gameObject.transform.localPosition = new Vector3(840f, -480f, 0f);
    }
}
