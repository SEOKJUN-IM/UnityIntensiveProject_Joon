using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIChangeChar : MonoBehaviour
{
    public List<Character> characters = new List<Character>();

    public TextMeshProUGUI char01JobText;
    public TextMeshProUGUI char01LevelText;
    public TextMeshProUGUI char01NameText;
    public Button char01SelectBtn;
    public Outline outline01;

    public TextMeshProUGUI char02JobText;
    public TextMeshProUGUI char02LevelText;
    public TextMeshProUGUI char02NameText;
    public Button char02SelectBtn;
    public Outline outline02;

    public Button backBtn;

    void Awake()
    {
        UIManager.Instance.uiChange = this;        
    }

    void Start()
    {
        CharConnect();
        char01SelectBtn.onClick.AddListener(SelectCharacter01);
        char02SelectBtn.onClick.AddListener(SelectCharacter02);
        backBtn.onClick.AddListener(backToMainMenu);        
    }

    void Update()
    {
        SetCharInfo();
        SetOutline();
    }

    public void backToMainMenu()
    {
        this.gameObject.SetActive(false);
        UIManager.Instance.uiMain.OpenMainMenu();
    }

    public void SetCharInfo()
    {
        char01JobText.text = characters[0].charJob;
        char01LevelText.text = characters[0].charLevel < 10 ? $"0{characters[0].charLevel}" : $"{characters[0].charLevel}";
        char01NameText.text = characters[0].charName;

        char02JobText.text = characters[1].charJob;
        char02LevelText.text = characters[1].charLevel < 10 ? $"0{characters[1].charLevel}" : $"{characters[1].charLevel}";
        char02NameText.text = characters[1].charName;
    }

    public void SetOutline()
    {
        if (characters[0].gameObject.activeInHierarchy) outline01.enabled = true;        
        else outline01.enabled = false;

        if (characters[1].gameObject.activeInHierarchy) outline02.enabled = true;
        else outline02.enabled = false;
    }

    public void SelectCharacter01()
    {        
        if (GameManager.Instance.Player.transform.GetChild(0).gameObject.activeInHierarchy) return;
        else if (GameManager.Instance.Player.transform.GetChild(1).gameObject.activeInHierarchy)
        {
            GameManager.Instance.Player.transform.GetChild(1).gameObject.SetActive(false);
            GameManager.Instance.Player.transform.GetChild(0).gameObject.SetActive(true);
            backToMainMenu();
        }                      
    }

    public void SelectCharacter02()
    {        
        if (GameManager.Instance.Player.transform.GetChild(1).gameObject.activeInHierarchy) return;
        else if (GameManager.Instance.Player.transform.GetChild(0).gameObject.activeInHierarchy)
        {
            GameManager.Instance.Player.transform.GetChild(0).gameObject.SetActive(false);
            GameManager.Instance.Player.transform.GetChild(1).gameObject.SetActive(true);
            backToMainMenu();
        }
    }

    public void CharConnect()
    {
        characters.Add(GameManager.Instance.Player.transform.GetChild(0).GetComponent<Character>());
        characters.Add(GameManager.Instance.Player.transform.GetChild(1).GetComponent<Character>());
    }
}
