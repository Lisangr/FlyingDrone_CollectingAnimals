using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class CanvasButtons : MonoBehaviour
{
    public GameObject helpPanel;
    public GameObject settingsPanel;
    public GameObject menuPanel;
    public GameObject leadersPanel;
    public GameObject defeatPanel;
    public GameObject winPanel;
    public GameObject attackButton;
    public GameObject[] rewardButtons;
    public Text goldText;
    public Text expText;
    private int currentIndex;
    private const string CurrentExperienceKey = "CurrentPlayerExperience";
    private const string CurrentGoldKey = "CurrentPlayerGold";
    private const string AllExperienceKey = "PlayerExperience";
    private const string AllGoldKey = "PlayerGold";
    private int currentGold;
    private int currentExp;
    private void OnEnable()
    {
        PlayerData.OnRewardedDone += PlayerData_OnRewardedDone;
    }

    private void PlayerData_OnRewardedDone()
    {
        currentGold = PlayerPrefs.GetInt(CurrentGoldKey);
        currentExp = PlayerPrefs.GetInt(CurrentExperienceKey);

        goldText.text = currentGold.ToString();
        expText.text = currentExp.ToString();
    }
    private void OnDisable()
    {
        PlayerData.OnRewardedDone -= PlayerData_OnRewardedDone;
    }
    private void Awake()
    {
        if (PlayerPrefs.HasKey("Level"))
        {
            currentIndex = PlayerPrefs.GetInt("Level");
        }
        else
        {
            currentIndex = PlayerPrefs.GetInt("Level", 1);
        }
    }
    private void Start()
    {
        OnCloseButtonClick();       
    }

    public void OnCloseButtonClick()
    {
        if (helpPanel != null) helpPanel.SetActive(false);
        if (settingsPanel != null) settingsPanel.SetActive(false);
        if (menuPanel != null) menuPanel.SetActive(false);
        if (leadersPanel != null) leadersPanel.SetActive(false);
        if (defeatPanel != null) defeatPanel.SetActive(false);
        if (winPanel != null) winPanel.SetActive(false);
        
        if ((YandexGame.EnvironmentData.isMobile || YandexGame.EnvironmentData.isTablet)
            && attackButton != null) attackButton.SetActive(true);

        // Если уровень меньше 2, делаем кнопки из массива rewardButtons неактивными
        if (currentIndex < 2 && rewardButtons != null)
        {
            foreach (GameObject button in rewardButtons)
            {
                if (button != null) button.SetActive(false);
            }
        }
    }
    public void ShowWinPanel()
    {
        currentGold = PlayerPrefs.GetInt(CurrentGoldKey);
        currentExp = PlayerPrefs.GetInt(CurrentExperienceKey);

        Debug.Log("ДЛЯ ВИН ПАНЕЛ Золото: " + currentGold + ", Опыт: " + currentExp);

        // Проверяем, активен ли winPanel
        if (winPanel != null)
        {
            winPanel.SetActive(true);
        }

        goldText.text = currentGold.ToString();       
        expText.text = currentExp.ToString();       
    }

    public void ShowDefeatPanel()
    {
        defeatPanel.SetActive(true);
    }
    public void OnHelpButtonClick()
    {
        helpPanel.SetActive(true);
    }
    public void OnSettingsButtonClick()
    {
        settingsPanel.SetActive(true);
    }
    public void OnLeadersButtonClick()
    {
        leadersPanel.SetActive(true);
    }
    public void OnStartButtonClick()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex == 0)
        {
            SceneManager.LoadScene(currentIndex, LoadSceneMode.Single);
        }
        else
        {
            YandexGame.FullscreenShow();
            SceneManager.LoadScene(currentIndex, LoadSceneMode.Single);
        }
    }

    public void OnExitButtonClick()
    {      
        // для сохранения данных глобально        
        int allGold = PlayerPrefs.GetInt(AllGoldKey, 0);
        int allExp = PlayerPrefs.GetInt(AllExperienceKey, 0);

        int newGold = currentGold + allGold;
        int newExp = currentExp + allExp;
        Debug.Log("СУММАРНО ОПЫТА " + newExp + " СУММАРНО ЗОЛОТА " + newGold);

        PlayerPrefs.SetInt(AllGoldKey, newGold);
        PlayerPrefs.SetInt(AllExperienceKey, newExp);

        PlayerPrefs.DeleteKey(CurrentGoldKey);
        PlayerPrefs.DeleteKey(CurrentExperienceKey);
        PlayerPrefs.Save(); // Сохраняем изменения
        Debug.Log("СУММАРНО сохранено ЗОЛОТА " + PlayerPrefs.GetInt(AllGoldKey, 0) + " ОПЫТА " + PlayerPrefs.GetInt(AllExperienceKey, 0));
        
        YandexGame.FullscreenShow();
        SceneManager.LoadScene(0);
    }
    public void OnPauseMenuClick()
    {
        menuPanel.SetActive(true);
    }
    public void RestartCurrentScene()
    {
        YandexGame.FullscreenShow();
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
