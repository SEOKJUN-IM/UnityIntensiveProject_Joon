using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject("GameManager").AddComponent<GameManager>();
            }
            return _instance;
        }
    }

    private Player _player;
    public Player Player
    {
        get { return _player; }
        set { _player = value; }
    }

    [Header("Char01 Inven")]
    public Item sword01;
    public Item shield01;
    public Item potion01;
    public Item bigPotion01;
    public Item scroll01;

    public int potionQuantity01;
    public int bigPotionQuantity01;
    public int scrollQuantity01;

    [Header("Char02 Inven")]
    public Item sword02;
    public Item shield02;
    public Item potion02;
    public Item bigPotion02;
    public Item scroll02;

    public int potionQuantity02;
    public int bigPotionQuantity02;
    public int scrollQuantity02;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (_instance != this)
            {
                Destroy(gameObject);
            }
        }
    } 

    public void SetData()
    {
        _player.character = CharacterManager.Instance.Character;

        _player.playerName = _player.character.charName;
        _player.playerJob = _player.character.charJob;
        _player.playerLevel = _player.character.charLevel;
        _player.playerCurExp = _player.character.charCurExp;
        _player.playerInfo = _player.character.charInfo;
        _player.playerGold = _player.character.charGold;

        _player.playerAttackValue = _player.character.charAttackValue;
        _player.playerDefenseValue = _player.character.charDefenseValue;
        _player.playerHealthValue = _player.character.charHealthValue;
        _player.playerCriticalValue = _player.character.charCriticalValue;

        _player.playerCurInvenQuantity = _player.character.charCurInvenQuantity;        
    }

    public void SetItems01()
    {
        List<Item> firstItems01 = _player.character.charInventory.items;
        firstItems01.Add(sword01);
        firstItems01.Add(shield01);
        
        for (int i = 0; i < potionQuantity01; i++)
        {
            firstItems01.Add(potion01);
        }

        for (int j = 0; j < bigPotionQuantity01; j++)
        {
            firstItems01.Add(bigPotion01);
        }

        for (int k = 0; k < scrollQuantity01; k++)
        {
            firstItems01.Add(scroll01);
        }        
    }

    public void SetItems02()
    {
        List<Item> firstItems02 = _player.character.charInventory.items;
        firstItems02.Add(sword02);
        firstItems02.Add(shield02);

        for (int i = 0; i < potionQuantity02; i++)
        {
            firstItems02.Add(potion02);
        }

        for (int j = 0; j < bigPotionQuantity02; j++)
        {
            firstItems02.Add(bigPotion02);
        }

        for (int k = 0; k < scrollQuantity02; k++)
        {
            firstItems02.Add(scroll02);
        }
    }
}
