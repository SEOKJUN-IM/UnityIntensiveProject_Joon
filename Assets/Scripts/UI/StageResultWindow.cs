using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageResultWindow : MonoBehaviour
{
    public GameObject clearWindow;
    public GameObject failWindow;
    public TextMeshProUGUI stageValueText;
    public TextMeshProUGUI secondsText;
    public Button goMainBtn;
    public Button goNextBtn;

    void Start()
    {
        goMainBtn.onClick.AddListener(onClickMainBtn);
        goNextBtn.onClick.AddListener(onClickNextBtn);
    }

    void Update()
    {
        stageValueText.text = $"0{UIManager.Instance.uiStage.selectedStageOrder}";
        SetSecondsText();
        OpenStageResultWindow();
        OffStageResultWindow();
    }

    public void SetSecondsText()
    {        
        int intSeconds = 5;
        secondsText.text = intSeconds.ToString();
    }

    void OpenStageResultWindow()
    {
        if (GameManager.Instance.inGameScene && GameManager.Instance.allDead)
        {
            clearWindow.SetActive(true);
            GameManager.Instance.allDead = false;

        }

        if (GameManager.Instance.inGameScene && CharacterManager.Instance.Character.charHealthValue == 0)
        {
            failWindow.SetActive(true);
        }
        else failWindow.SetActive(false);
    }    

    void OffStageResultWindow()
    {
        if (GameManager.Instance.inMainMenuScene)
        {
            clearWindow.SetActive(false);
            failWindow.SetActive(false);
        }
    }

    public void onClickMainBtn()
    {        
        GameManager.Instance.BackToMainMenuScene();        
    }

    public void onClickNextBtn()
    {
        if (clearWindow.activeInHierarchy)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            GameManager.Instance.ResetGameScene();
            clearWindow.SetActive(false);
        }                
        
        if (failWindow.activeInHierarchy)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            GameManager.Instance.ResetGameScene();
            failWindow.SetActive(false);

            CharacterManager.Instance.Character.charHealthValue = GameManager.Instance.gameStartHp;
            GameManager.Instance.Player.gameObject.GetComponent<Unit>().isDead = false;
            GameManager.Instance.Player.gameObject.GetComponent<Unit>().state = Unit.State.Idle;
        }                
    }
}
