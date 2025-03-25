using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameInfo : MonoBehaviour
{
    public Image char01Icon;
    public Image char02Icon;

    public TextMeshProUGUI charNameText;
    public TextMeshProUGUI charJobText;
    public TextMeshProUGUI charLevelText;
    public TextMeshProUGUI charCurExpText;
    public TextMeshProUGUI charMaxExpText;
    public Image curExpBar;
    public TextMeshProUGUI charCurHpText;
    public TextMeshProUGUI charMaxHpText;
    public Image curHpBar;
    public TextMeshProUGUI charCurMpText;
    public TextMeshProUGUI charMaxMpText;
    public Image curMpBar;    

    void Update()
    {
        SetGameInfo();
    }

    void SetGameInfo()
    {
        if (GameManager.Instance.onChar01)
        {
            char01Icon.enabled = true;
            char02Icon.enabled = false;
        }
        else if (GameManager.Instance.onChar02)
        {
            char01Icon.enabled = false;
            char02Icon.enabled = true;
        }
        charNameText.text = GameManager.Instance.Player.playerName;
        charJobText.text = GameManager.Instance.Player.playerJob;
        charLevelText.text = GameManager.Instance.Player.playerLevel < 10 ? $"0{GameManager.Instance.Player.playerLevel}" : $"{GameManager.Instance.Player.playerLevel}";
        charCurExpText.text = $"{GameManager.Instance.Player.playerCurExp}";
        curExpBar.fillAmount = GameManager.Instance.Player.playerCurExp / (float)GameManager.Instance.Player.playerMaxExp;
        charCurHpText.text = $"{GameManager.Instance.Player.playerHealthValue}";
        curHpBar.fillAmount = GameManager.Instance.Player.playerHealthValue / (float)GameManager.Instance.Player.playerMaxHealthValue;
        charCurMpText.text = $"{GameManager.Instance.Player.playerManaValue}";
        curMpBar.fillAmount = GameManager.Instance.Player.playerManaValue / (float)GameManager.Instance.Player.playerMaxManaValue;
    }
}
