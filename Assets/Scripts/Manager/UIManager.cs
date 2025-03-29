using UnityEngine;
using UniRx;
using OpenCover.Framework.Model;

public class UIManager : MonoBehaviour
{
    [field: SerializeField] public UIMainMenu uiMain { get; set; }
    [field: SerializeField] public UIStatus uiStat { get; set; }
    public UIInventory uiInven01;
    public UIInventory uiInven02;
    [field: SerializeField] public UIChangeChar uiChange { get; set; }
    [field: SerializeField] public UIStage uiStage { get; set; }

    public StageResultWindow stageResultWindow;

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

        uiInven01 = GameObject.Find("UIInventory01").GetComponent<UIInventory>();
        uiInven02 = GameObject.Find("UIInventory02").GetComponent<UIInventory>();
        stageResultWindow = GameObject.Find("UIMainMenu").transform.Find("StageFail").GetComponent<StageResultWindow>();        
    }

    private void Start()
    {
        uiStat.gameObject.SetActive(false);
        uiInven01.gameObject.SetActive(false);
        uiInven02.gameObject.SetActive(false);
        uiChange.gameObject.SetActive(false);
        uiStage.gameObject.SetActive(false);
    }
}
