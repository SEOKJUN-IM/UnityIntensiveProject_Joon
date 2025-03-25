using UnityEngine;

[CreateAssetMenu(fileName = "UnitData", menuName = "New UnitData")]
public class UnitData : ScriptableObject
{
    [Header("Unit Info")]
    public string unitName;
    public string unitType;
    public float moveSpeed;
    public int unitHealth;
    public float attackRange;
    public float unitAttackPower;
    public int unitExp;        
}
