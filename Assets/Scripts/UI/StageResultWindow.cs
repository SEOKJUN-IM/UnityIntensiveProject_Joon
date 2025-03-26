using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageResultWindow : MonoBehaviour
{
    public GameObject clearWindow;
    public GameObject failWindow;    
    public Button goMainBtn;
    public Button goNextBtn;    

    void Start()
    {
        goMainBtn.onClick.AddListener(onMain);
        goNextBtn.onClick.AddListener(onNext);
    }

    void Update()
    {
        OnClearWindow();
        OffResultWindow();
    }    

    // 클리어 창 띄우기
    void OnClearWindow()
    {
        if (GameManager.Instance.inGameScene && GameManager.Instance.allDead)
        {
            clearWindow.SetActive(true);
            GameManager.Instance.allDead = false;
        }        
    }        

    // 메인씬에선 안 보이게
    void OffResultWindow()
    {
        if (GameManager.Instance.inMainMenuScene)
        {
            clearWindow.SetActive(false);
            failWindow.SetActive(false);
        }
    }

    // 메인으로 가는 메서드
    public void onMain()
    {        
        GameManager.Instance.BackToMainMenuScene();        
    }

    // 다음으로 가는 메서드
    public void onNext()
    {
        // 클리어 창 떠있을 때
        if (clearWindow.activeInHierarchy)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            GameManager.Instance.ResetGameScene();
            GameManager.Instance.ResetPlayerCameraPosRotInGame();
            clearWindow.SetActive(false);
        }                
        
        // 실패 창 떠있을 때
        if (failWindow.activeInHierarchy)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            GameManager.Instance.ResetGameScene();
            GameManager.Instance.ResetPlayerCameraPosRotInGame();
            failWindow.SetActive(false);

            CharacterManager.Instance.Character.charHealthValue = GameManager.Instance.gameStartHp;
            GameManager.Instance.Player.gameObject.GetComponent<Unit>().isDead = false;
            GameManager.Instance.Player.gameObject.GetComponent<Unit>().state = Unit.State.Idle;
        }                
    }

    // 창을 띄우고 5초 후 다음으로
    public void OpenResultWindow()
    {
        if (GameManager.Instance.inGameScene && CharacterManager.Instance.Character.charHealthValue == 0)
            failWindow.SetActive(true);

        Invoke("onNext", 5f);
    }

    // 실패 창은 플레이어 죽은 후 3초 후에 뜨고, 그 5초 뒤에 다음으로, DeadState에서 불러줄 것
    public void SlowOnResultWindow()
    {
        Invoke("OpenResultWindow", 3f);
    }
}
