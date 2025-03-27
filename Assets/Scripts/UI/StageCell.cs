using UnityEngine;
using UnityEngine.UI;

public class StageCell : MonoBehaviour
{
    public StageType selectedStage;
    public bool isSelected;

    public Button stageBtn;    
    public Outline selectedOutline;
    public GameObject selectedBG;
    public GameObject selectedText;    

    void Start()
    {
        stageBtn.onClick.AddListener(ChangeCurStage);
    }

    void Update()
    {        
        if (isSelected) StageManager.Instance.curStageType = selectedStage;
        SetSelected();        
    }

    void ChangeCurStage()
    {        
        if (!isSelected)
        {
            UIManager.Instance.uiStage.SelectOnlyOneStage();
            isSelected = true;            
        }                
    }

    public void SetSelected()
    {
        selectedOutline.enabled = isSelected;
        selectedBG.SetActive(isSelected);
        selectedText.SetActive(isSelected);       
    }
}
