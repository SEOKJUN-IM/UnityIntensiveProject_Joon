using UnityEngine;

[CreateAssetMenu(fileName = "UnitData", menuName = "New UnitData")]
public class UnitData : ScriptableObject
{
    [Header("Unit Info")]
    public string unitName;
    public string unitType;
    public int moveSpeed;
    public int unitHealth;
    public int attackRange;
    public int unitAttackPower;    
    public int unitExp;        
}
