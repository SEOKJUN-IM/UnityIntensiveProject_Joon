using UnityEngine;

public class Character : MonoBehaviour
{
    public string charName;
    public string charJob;
    public int charLevel;
    [Range(0, 100)] public int charCurExp;
    public string charInfo;
    public int charGold;

    public int charAttackValue;
    public int charDefenseValue;
    public int charHealthValue;
    public int charCriticalValue;

    public int charCurInvenQuantity;

    private void Awake()
    {
        CharacterManager.Instance.Character = this;
    }
}
