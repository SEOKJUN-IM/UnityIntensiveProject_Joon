using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject("GameManager").AddComponent<GameManager>();
            }
            return _instance;
        }
    }

    private Player _player;
    public Player Player
    {
        get { return _player; }
        set { _player = value; }
    }

    public bool inMainMenuScene = true;
    public bool inGameScene = false;
    public bool isPaused = false;

    public bool onChar01 = true;
    public bool onChar02 = false;
    
    public GameObject uiObject;    
    public Camera gameCamera;
    public GameObject cameraContainer;

    private Vector3 playerFirstPos;
    private Vector3 playerFirstRot;
    private Vector3 cameraFirstPos;
    private Vector3 cameraFisrtRot;

    [Header("Char01 Inven")]
    public Item sword01;
    public Item shield01;
    public Item hpPotion01;
    public int hpPotionQuantity01;
    public Item manaPosion01;
    public int manaPotionQuantity01;
    public Item bigHpPotion01;
    public int bigHpPotionQuantity01;
    public Item attackScroll01;
    public int attackScrollQuantity01;
    public Item criticalScroll01;
    public int criticalScrollQuantity01;

    [Header("Char02 Inven")]
    public Item sword02;
    public Item shield02;
    public Item hpPotion02;
    public int hpPotionQuantity02;
    public Item manaPosion02;
    public int manaPotionQuantity02;
    public Item bigHpPotion02;
    public int bigHpPotionQuantity02;
    public Item attackScroll02;
    public int attackScrollQuantity02;
    public Item criticalScroll02;
    public int criticalScrollQuantity02;

    // 플레이어의 모든 타겟 죽었는지 확인
    public GameObject[] monsters;
    public int monsterCounts = 0;
    public int deadCounts = 0;    
    
    public bool allDead = false;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (_instance != this)
            {
                Destroy(gameObject);
            }
        }

        gameCamera = Camera.main;        
    }

    private void Update()
    {
        DistinChar();
        FindAllMonsterCounts();
        CheckAllDead();
    }

    private void Start()
    {
        playerFirstPos = Player.gameObject.transform.position;
        playerFirstRot = Player.gameObject.transform.eulerAngles;
        cameraFirstPos = Instance.gameCamera.transform.position;
        cameraFisrtRot = Instance.gameCamera.transform.eulerAngles;
    }

    public void SetData()
    {
        _player.character = CharacterManager.Instance.Character;
        
        _player.playerName = _player.character.charName;
        _player.playerJob = _player.character.charJob;
        _player.playerLevel = _player.character.charLevel;
        _player.playerCurExp = _player.character.charCurExp;
        _player.playerMaxExp = _player.character.charMaxExp;
        _player.playerInfo = _player.character.charInfo;
        _player.playerGold = _player.character.charGold;

        _player.playerAttackValue = _player.character.charAttackValue;
        _player.playerDefenseValue = _player.character.charDefenseValue;
        _player.playerHealthValue = _player.character.charHealthValue;
        _player.playerMaxHealthValue = _player.character.charMaxHealthValue;
        _player.playerManaValue = _player.character.charManaValue;
        _player.playerMaxManaValue = _player.character.charMaxManaValue;
        _player.playerCriticalValue = _player.character.charCriticalValue;

        _player.playerCurInvenQuantity = _player.character.charCurInvenQuantity;        
    }

    void DistinChar()
    {
        if (Player.transform.GetChild(0).gameObject.activeInHierarchy)
        {
            onChar01 = true;
            onChar02 = false;
        }
        else if (Player.transform.GetChild(1).gameObject.activeInHierarchy)
        {
            onChar02 = true;
            onChar01 = false;
        }
    }

    public void SetItems01()
    {
        List<Item> firstItems01 = _player.character.charInventory.items;
        firstItems01.Add(sword01);
        firstItems01.Add(shield01);
        
        for (int i = 0; i < hpPotionQuantity01; i++)
        {
            firstItems01.Add(hpPotion01);
        }

        for (int j = 0; j < manaPotionQuantity01; j++)
        {
            firstItems01.Add(manaPosion01);
        }

        for (int k = 0; k < bigHpPotionQuantity01; k++)
        {
            firstItems01.Add(bigHpPotion01);
        }

        for (int l = 0; l < attackScrollQuantity01; l++)
        {
            firstItems01.Add(attackScroll01);
        }

        for (int m = 0; m < criticalScrollQuantity01; m++)
        {
            firstItems01.Add(criticalScroll01);
        }
    }

    public void SetItems02()
    {
        List<Item> firstItems02 = _player.character.charInventory.items;
        firstItems02.Add(sword02);
        firstItems02.Add(shield02);

        for (int i = 0; i < hpPotionQuantity02; i++)
        {
            firstItems02.Add(hpPotion02);
        }

        for (int j = 0; j < manaPotionQuantity02; j++)
        {
            firstItems02.Add(manaPosion02);
        }

        for (int k = 0; k < bigHpPotionQuantity02; k++)
        {
            firstItems02.Add(bigHpPotion02);
        }

        for (int l = 0; l < attackScrollQuantity02; l++)
        {
            firstItems02.Add(attackScroll02);
        }

        for (int m = 0; m < criticalScrollQuantity02; m++)
        {
            firstItems02.Add(criticalScroll02);
        }
    }    

    public void GameStart()
    {
        SceneManager.LoadScene("GameScene");
        ResetGameScene();                      
    }

    public void ResetGameScene()
    {
        inGameScene = true;
        inMainMenuScene = false;
        DontDestroyOnLoad(Player.gameObject);
        DontDestroyOnLoad(uiObject.gameObject);

        UIManager.Instance.uiStat.gameObject.SetActive(false);
        UIManager.Instance.uiInven01.gameObject.SetActive(false);
        UIManager.Instance.uiInven02.gameObject.SetActive(false);
        UIManager.Instance.uiChange.gameObject.SetActive(false);

        UIManager.Instance.uiMain.ResetGameScene();
        UIManager.Instance.uiMain.backToMainBtn.transform.parent.gameObject.SetActive(true);
        SetGameCameraPosition();

        deadCounts = 0;
        allDead = false;        
    }

    public void SetGameCameraPosition()
    {        
        gameCamera.gameObject.transform.localPosition = new Vector3(3.25f, 5f, 6f);
        gameCamera.gameObject.transform.localEulerAngles = new Vector3(35f, 210f, 0f);
        gameCamera.fieldOfView = 50f;
    }

    public void BackToMainMenuScene()
    {              
        SceneManager.LoadScene("MainMenuScene");
        inMainMenuScene = true;
        inGameScene = false;
        DontDestroyOnLoad(Player.gameObject);
        DontDestroyOnLoad(uiObject.gameObject);

        UIManager.Instance.uiStat.gameObject.SetActive(false);
        UIManager.Instance.uiInven01.gameObject.SetActive(false);
        UIManager.Instance.uiInven02.gameObject.SetActive(false);
        UIManager.Instance.uiChange.gameObject.SetActive(false);
        
        UIManager.Instance.uiMain.ResetMainScene();
        UIManager.Instance.uiMain.backToMainBtn.transform.parent.gameObject.SetActive(false);

        PlayerPositionResetInMain();
        CameraPositionResetInMain();

        deadCounts = 0;       
        allDead = false;
    }

    public void PlayerPositionResetInMain()
    {
        Player.gameObject.transform.position = playerFirstPos;
        Player.gameObject.transform.eulerAngles = playerFirstRot;
    }

    public void CameraPositionResetInMain()
    {
        Instance.gameCamera.transform.position = cameraFirstPos;
        Instance.gameCamera.transform.eulerAngles = cameraFisrtRot;
        gameCamera.fieldOfView = 75f;
    }   

    // 게임 씬에서 총 몬스터 찾아오기
    public void FindAllMonsterCounts()
    {
        if (inGameScene)
        {
            monsters = GameObject.FindGameObjectsWithTag("Monster");
        }
        monsterCounts = monsters.Length;
    }    

    // 타겟 모두 죽었는지 아닌지 검사
    public void CheckAllDead()
    {
        if (monsterCounts != 0)
        {
            if (inGameScene && deadCounts == monsterCounts) allDead = true;
            else allDead = false;
        }
    }
}
