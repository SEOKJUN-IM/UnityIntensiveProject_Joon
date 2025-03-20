using UnityEngine;

public class Character : MonoBehaviour
{
    private void Awake()
    {
        CharacterManager.Instance.Character = this;
    }
}
