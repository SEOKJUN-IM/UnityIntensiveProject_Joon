using UnityEngine;

public class Character : MonoBehaviour
{
    [field: SerializeField] public string charName { get; private set; }
    [field: SerializeField] public string charJob { get; private set; }
    [field: SerializeField] public int charLevel { get; private set; }
    [field: SerializeField] public int charCurExp { get; private set; }
    [field: SerializeField, TextArea] public string charInfo { get; private set; }
    [field: SerializeField] public int charGold { get; private set; }

    [field: SerializeField] public int charAttackValue { get; private set; }
    [field: SerializeField] public int charDefenseValue { get; private set; }
    [field: SerializeField] public int charHealthValue { get; private set; }
    [field: SerializeField] public int charCriticalValue { get; private set; }

    [field: SerializeField] public int charCurInvenQuantity { get; private set; }

    private void Update()
    {
        if (this.gameObject.activeInHierarchy == true) CharacterManager.Instance.Character = this;
    }
}
