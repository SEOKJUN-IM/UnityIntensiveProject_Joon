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
}
