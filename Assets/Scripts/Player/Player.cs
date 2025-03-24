using UnityEngine;

public class Player : MonoBehaviour
{
    public Character character;
    
    public string playerName;
    public string playerJob;
    public int playerLevel;
    public int playerCurExp;
    public string playerInfo;
    public int playerGold;

    public int playerAttackValue;
    public int playerDefenseValue;
    public int playerHealthValue;
    public int playerManaValue;
    public int playerCriticalValue;

    public int playerCurInvenQuantity;
    public PlayerController Controller;   

    private void Awake()
    {
        GameManager.Instance.Player = this;
        Controller = GetComponent<PlayerController>();        
    }

    private void LateUpdate()
    {
        GameManager.Instance.SetData();
    }
}
