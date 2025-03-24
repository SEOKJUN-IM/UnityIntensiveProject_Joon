using UnityEngine;
using UnityEngine.UI;

public enum StageType
{
    Stage01,
    Stage02,
    Stage03,
    Stage04,
    Stage05,
    Stage06
}

public class Stage : MonoBehaviour
{
    public Button stageBtn;
    public StageType selectedStage;
    public Outline selectedOutline;
    public GameObject selectedBG;
    public GameObject selectedText;
    public bool isSelected;

    void Start()
    {
        stageBtn.onClick.AddListener(SelectBtn);
    }

    void Update()
    {
        SetSelected();
    }

    void SelectBtn()
    {        
        if (!isSelected)
        {
            UIManager.Instance.uiStage.SelectOnlyOneStage();
            isSelected = true;            
        }
        else
        {
            isSelected = false;            
        }        
    }

    public void SetSelected()
    {
        selectedOutline.enabled = isSelected;
        selectedBG.SetActive(isSelected);
        selectedText.SetActive(isSelected);       
    }
}
