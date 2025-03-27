using TMPro;
using UnityEngine;

public class UIStage : MonoBehaviour
{
    public StageCell stage01Cell;
    public StageCell stage02Cell;
    public StageCell stage03Cell;
    public StageCell stage04Cell;
    public StageCell stage05Cell;
    public StageCell stage06Cell;

    public StageCell[] stageCells;    

    public TextMeshProUGUI selectedStageText;

    void Awake()
    {
        UIManager.Instance.uiStage = this;
        stageCells = new StageCell[6] { stage01Cell, stage02Cell, stage03Cell, stage04Cell, stage05Cell, stage06Cell };
    }

    void Update()
    {
        selectedStageText.text = StageManager.Instance.curStageData.stageNum.ToString();
    }

    public void SelectOnlyOneStage()
    {
        for (int i = 0; i < stageCells.Length; i++)
        {
            if (stageCells[i].isSelected) stageCells[i].isSelected = false;
        }       
    } 
}
