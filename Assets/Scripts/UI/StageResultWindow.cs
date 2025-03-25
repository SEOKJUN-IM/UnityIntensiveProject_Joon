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
        stageValueText.text = $"{UIManager.Instance.uiStage.selectedStageOrder + 1}";
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
        if (GameManager.Instance.inGameScene)
        {
            if (GameManager.Instance.allDead)
            {
                clearWindow.SetActive(true);
                GameManager.Instance.allDead = false;
            }
            else if (CharacterManager.Instance.Character.charHealthValue == 0)
            {
                failWindow.SetActive(true);
            }
        }
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);        
        GameManager.Instance.ResetGameScene();
        clearWindow.SetActive(false);
        failWindow.SetActive(false);        
    }
}
