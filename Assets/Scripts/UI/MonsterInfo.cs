using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MonsterInfo : MonoBehaviour
{
    public Unit playerUnit;
    
    public TextMeshProUGUI targetName;
    public TextMeshProUGUI targetLevel;
    public TextMeshProUGUI targetExp;
    public TextMeshProUGUI targetCurHp;
    public Image targetHpBar;
    
    void Update()
    {
        SetTargetInfo();
    }

    void SetTargetInfo()
    {
        if (playerUnit.target == null) return;

        Unit target = playerUnit.target;
        UnitData targetData = playerUnit.target.data;

        targetName.text = targetData.unitName;
        targetLevel.text = targetData.unitLevel < 10 ? "0" + targetData.unitLevel.ToString() : targetData.unitLevel.ToString();
        targetExp.text = targetData.unitExp.ToString();
        targetCurHp.text = target.health.ToString();

        targetHpBar.fillAmount = target.health / (float)targetData.unitHealth;
    }
}
