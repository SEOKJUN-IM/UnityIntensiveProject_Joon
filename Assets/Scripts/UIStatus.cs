using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStatus : MonoBehaviour
{
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
        
    }

    public void backToMainMenu()
    {
        this.gameObject.SetActive(false);
        UIManager.Instance.uiMain.OpenMainMenu();
    }
}
