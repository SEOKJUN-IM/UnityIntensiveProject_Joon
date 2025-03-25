using TMPro;
using UnityEngine;

public class UIStage : MonoBehaviour
{
    public Stage stage01;
    public Stage stage02;
    public Stage stage03;
    public Stage stage04;
    public Stage stage05;
    public Stage stage06;

    public Stage[] stages;

    public int selectedStageOrder;

    public TextMeshProUGUI selectedStageText;

    void Awake()
    {
        UIManager.Instance.uiStage = this;
        stages = new Stage[6] { stage01, stage02, stage03, stage04, stage05, stage06 };
    }

    void Update()
    {
        ShowSelectStageTextInUIMain();
    }

    public void SelectOnlyOneStage()
    {
        for (int i = 0; i < stages.Length; i++)
        {
            if (stages[i].isSelected) stages[i].isSelected = false;
        }       
    }

    public void ShowSelectStageTextInUIMain()
    {
        for (int i = 0; i < stages.Length; i++)
        {
            if (stages[i].isSelected)
            {
                selectedStageOrder = (int)stages[i].selectedStage + 1;
                selectedStageText.text = selectedStageOrder.ToString();
            }
        }
    }    
}
