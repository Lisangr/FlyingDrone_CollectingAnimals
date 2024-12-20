using UnityEngine;
using UnityEngine.SceneManagement;
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

    private int currentIndex;
    private void Awake()
    {
        if (PlayerPrefs.HasKey("Level"))
        {
            currentIndex = PlayerPrefs.GetInt("Level");
        }
        else
        {
            currentIndex = PlayerPrefs.GetInt("Level", 22);
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
        winPanel.SetActive(true);
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
        SceneManager.LoadScene(currentIndex, LoadSceneMode.Single);
    }

    private const string CurrentExperienceKey = "CurrentPlayerExperience";
    private const string CurrentGoldKey = "CurrentPlayerGold";
    private const string AllExperienceKey = "PlayerExperience";
    private const string AllGoldKey = "PlayerGold";

    public void OnExitButtonClick()
    {      
        // для сохранения данных глобально
        int currentGold = PlayerPrefs.GetInt(CurrentGoldKey, 0);
        int currentExp = PlayerPrefs.GetInt(CurrentExperienceKey, 0);
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
        
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
    public void OnPauseMenuClick()
    {
        menuPanel.SetActive(true);
    }
    public void RestartCurrentScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
