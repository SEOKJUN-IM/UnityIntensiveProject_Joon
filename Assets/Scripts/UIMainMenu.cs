using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
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
}
