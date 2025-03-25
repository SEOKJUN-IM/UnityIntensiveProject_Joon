using UnityEngine;

[CreateAssetMenu(fileName = "UnitData", menuName = "New UnitData")]
public class UnitData : ScriptableObject, ISerializationCallbackReceiver
{
    [Header("Unit Info")]
    public string unitName;
    public string unitType;
    public int unitLevel;
    public int moveSpeed;
    public int unitHealth;
    public int attackRange;
    public int unitAttackPower;    
    public int unitExp;

    public int UnitHp { get; set; }
    public int UnitExp { get; set; }   

    public void OnAfterDeserialize()
    {
        UnitHp = unitHealth;
        UnitExp = unitExp;
    }

    public void OnBeforeSerialize()
    {
        
    }
}
