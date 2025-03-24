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
    public Image curExpBar;
    public TextMeshProUGUI charCurHpText;
    public Image curHpBar;
    public TextMeshProUGUI charCurMpText;
    public Image curMpBar;    

    void Update()
    {
        SetGameInfo();
    }

    void SetGameInfo()
    {
        if (GameManager.Instance.Player.gameObject.transform.GetChild(0).gameObject.activeInHierarchy)
        {
            char01Icon.enabled = true;
            char02Icon.enabled = false;
        }
        else if (GameManager.Instance.Player.gameObject.transform.GetChild(1).gameObject.activeInHierarchy)
        {
            char01Icon.enabled = false;
            char02Icon.enabled = true;
        }
        charNameText.text = GameManager.Instance.Player.playerName;
        charJobText.text = GameManager.Instance.Player.playerJob;
        charLevelText.text = GameManager.Instance.Player.playerLevel < 10 ? $"0{GameManager.Instance.Player.playerLevel}" : $"{GameManager.Instance.Player.playerLevel}";
        charCurExpText.text = $"{GameManager.Instance.Player.playerCurExp}";
        curExpBar.fillAmount = GameManager.Instance.Player.playerCurExp / 100f;
        charCurHpText.text = $"{GameManager.Instance.Player.playerHealthValue}";
        curHpBar.fillAmount = GameManager.Instance.Player.playerHealthValue / 100f;
        charCurMpText.text = $"{GameManager.Instance.Player.playerManaValue}";
        curMpBar.fillAmount = GameManager.Instance.Player.playerManaValue / 100f;
    }
}
