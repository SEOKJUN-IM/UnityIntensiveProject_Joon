using UnityEngine;

[CreateAssetMenu(fileName = "UnitData", menuName = "New UnitData")]
public class UnitData : ScriptableObject
{
    [Header("Base Info")]
    public string unitName;
    public string unitInfo;
    public float moveSpeed;
    public int unitHealth;
    public float attackRange;
    public float unitAttackPower;        
}
