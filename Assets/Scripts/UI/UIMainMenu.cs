using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    public Image char01Icon;
    public Image char02Icon;

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
    public Button backToMainBtn;

    public GameObject stageBtnText;
    public TextMeshProUGUI curStageText;
    public GameObject backToMainIcon;
    public GameObject backToMainText;    

    public Image curExpBar;

    public GameObject charInfoWindow;
    public GameObject gameInfoWindow;
    public GameObject backInfoWindow;
    public Button AcceptGoMainBtn;
    public Button CancleGoMainBtn;

    public GameObject changeWindow;
    public Image char01ChangeIcon;
    public Image char02ChangeIcon;
    public TextMeshProUGUI changeText;

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
        backToMainBtn.onClick.AddListener(OpenTryBackMain);
        AcceptGoMainBtn.onClick.AddListener(GameManager.Instance.BackToMainScene);
        CancleGoMainBtn.onClick.AddListener(CancleGoMain);

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
        if (GameManager.Instance.Player.gameObject.transform.GetChild(0).gameObject.activeInHierarchy)
        {
            char01Icon.enabled = true;
            char02Icon.enabled = false;
        }
        else if (GameManager.Instance.Player.gameObject.transform.GetChild(1).gameObject.activeInHierarchy)
        {
            char01Icon.enabled = false;
            char02Icon.enabled = true;
        }
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
            gameStartBtn.gameObject.SetActive(false);
            stageBtn.gameObject.transform.localPosition = new Vector3(0f, -460f, 0f);

            statBtn.gameObject.transform.localScale = Vector3.one * 0.7f;
            invenBtn.gameObject.transform.localScale = Vector3.one * 0.7f;
            changeBtn.gameObject.transform.localScale = Vector3.one * 0.7f;

            statBtn.gameObject.transform.localPosition = new Vector3(840f, -310f, 0f);
            invenBtn.gameObject.transform.localPosition = new Vector3(840f, -395f, 0f);
            changeBtn.gameObject.transform.localPosition = new Vector3(840f, -480f, 0f);

            stageBtnText.SetActive(false);
            curStageText.gameObject.SetActive(false);
            backToMainIcon.SetActive(true);
            backToMainText.SetActive(true);

            GameManager.Instance.gameCamera.transform.position -= Vector3.right * 4;
            GameManager.Instance.gameCamera.fieldOfView = 80f;
            GameManager.Instance.Player.transform.GetChild(0).eulerAngles = new Vector3(0f, -30f, 0f);
            GameManager.Instance.Player.transform.GetChild(1).eulerAngles = new Vector3(0f, -30f, 0f);

            UIManager.Instance.uiStage.gameObject.SetActive(true);
        }
        else
        {
            charInfoWindow.gameObject.SetActive(true);
            gameStartBtn.gameObject.SetActive(true);
            stageBtn.gameObject.transform.localPosition = new Vector3(-150f, -460f, 0f);

            statBtn.gameObject.transform.localScale = Vector3.one;
            invenBtn.gameObject.transform.localScale = Vector3.one;
            changeBtn.gameObject.transform.localScale = Vector3.one;

            statBtn.gameObject.transform.localPosition = new Vector3(675f, 120f, 0f);
            invenBtn.gameObject.transform.localPosition = new Vector3(675f, 0f, 0f);
            changeBtn.gameObject.transform.localPosition = new Vector3(675f, -120f, 0f);

            stageBtnText.SetActive(true);
            curStageText.gameObject.SetActive(true);
            backToMainIcon.SetActive(false);
            backToMainText.SetActive(false);

            GameManager.Instance.gameCamera.transform.position += Vector3.right * 4;
            GameManager.Instance.gameCamera.fieldOfView = 75f;
            GameManager.Instance.Player.transform.GetChild(0).eulerAngles = Vector3.zero;
            GameManager.Instance.Player.transform.GetChild(1).eulerAngles = Vector3.zero;

            UIManager.Instance.uiStage.gameObject.SetActive(false);
        }       
    }

    void OffChangeWindow()
    {
        changeWindow.SetActive(false);
    }

    public void ResetGameScene()
    {
        if(UIManager.Instance.uiStage.gameObject.activeInHierarchy) UIManager.Instance.uiStage.gameObject.SetActive(false);
        
        gameStartBtn.gameObject.SetActive(false);
        changeBtn.gameObject.SetActive(false);
        charInfoWindow.SetActive(false);
        gameInfoWindow.SetActive(true);

        changeWindow.SetActive(true);
        if (GameManager.Instance.Player.gameObject.transform.GetChild(0).gameObject.activeInHierarchy)
        {
            char01ChangeIcon.enabled = true;
            char02ChangeIcon.enabled = false;
        }
        else if (GameManager.Instance.Player.gameObject.transform.GetChild(1).gameObject.activeInHierarchy)
        {
            char02ChangeIcon.enabled = true;
            char01ChangeIcon.enabled = false;
        }
        changeText.text = "Game Starting...";
        Invoke("OffChangeWindow", 0.75f);

        statBtn.gameObject.transform.localScale = Vector3.one * 0.7f;
        invenBtn.gameObject.transform.localScale = Vector3.one * 0.7f;        
        stageBtn.gameObject.transform.localScale = Vector3.one * 0.7f;

        statBtn.gameObject.transform.localPosition = new Vector3(840f, -395f, 0f);
        invenBtn.gameObject.transform.localPosition = new Vector3(840f, -480f, 0f);        
        stageBtn.gameObject.transform.localPosition = new Vector3(410f, 440f, 0f);
        stageBtn.enabled = false;
    }

    public void ResetMainScene()
    {
        stageBtn.gameObject.SetActive(true);
        gameStartBtn.gameObject.SetActive(true);
        changeBtn.gameObject.SetActive(true);
        charInfoWindow.SetActive(true);
        gameInfoWindow.SetActive(false);
        backInfoWindow.SetActive(false);

        changeWindow.SetActive(true);
        if (GameManager.Instance.Player.gameObject.transform.GetChild(0).gameObject.activeInHierarchy)
        {
            char01ChangeIcon.enabled = true;
            char02ChangeIcon.enabled = false;
        }
        else if (GameManager.Instance.Player.gameObject.transform.GetChild(1).gameObject.activeInHierarchy)
        {
            char02ChangeIcon.enabled = true;
            char01ChangeIcon.enabled = false;
        }
        changeText.text = "Going To Main...";
        Invoke("OffChangeWindow", 0.75f);

        statBtn.gameObject.transform.localScale = Vector3.one;
        invenBtn.gameObject.transform.localScale = Vector3.one;        
        stageBtn.gameObject.transform.localScale = Vector3.one;

        statBtn.gameObject.transform.localPosition = new Vector3(675f, 120f, 0f);
        invenBtn.gameObject.transform.localPosition = new Vector3(675f, 0f, 0f);        
        stageBtn.gameObject.transform.localPosition = new Vector3(-150f, -460f, 0f);
        stageBtn.enabled = true;
    }

    public void OpenTryBackMain()
    {
        if (!backInfoWindow.activeInHierarchy) backInfoWindow.SetActive(true);
        else backInfoWindow.SetActive(false);
    }

    public void CancleGoMain()
    {
        backInfoWindow.SetActive(false);
    }
}
