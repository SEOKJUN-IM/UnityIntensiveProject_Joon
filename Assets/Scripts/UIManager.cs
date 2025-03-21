using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [field: SerializeField] public UIMainMenu uiMain { get; set; }
    [field: SerializeField] public UIStatus uiStat { get; set; }
    [field: SerializeField] public UIInventory uiInven { get; set; }

    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject("UIManager").AddComponent<UIManager>();
            }
            return _instance;
        }
    }

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
    }

    private void Start()
    {
        uiStat.gameObject.SetActive(false);
        uiInven.gameObject.SetActive(false);
    }
}
