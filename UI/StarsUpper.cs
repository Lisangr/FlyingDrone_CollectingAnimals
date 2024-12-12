using UnityEngine;
using UnityEngine.UI;
using YG;

public class StarsUpper : MonoBehaviour
{   
    public Text starsText;              // Текст для отображения золота
    public Text upgradeCostText;       // Текст для отображения стоимости апгрейда
    public Button upgradeButton;       // Кнопка для апгрейда
    public Image StarsADImage;        // Изображение с рекламой
    public Text upgradeLVL;
    private int allExp;               // Весь опыт
    private int upgradeLevel = 1;      // Уровень апгрейда
    private const string AllExpKey = "PlayerExperience";  // Ключ для хранения золота в PlayerPrefs
    private const string UpgradeLevelKey = "StarsUpgradeLevel"; // Ключ для хранения уровня апгрейда

    private int AdID = 4; //Уровень звездности
    private void OnEnable()
    {
        YandexGame.RewardVideoEvent += Rewarded;
    }

    public void Rewarded(int id)
    {
        if (id == AdID)
            AddStarsLVL();
    }
    private void AddStarsLVL()
    {
        // Увеличиваем уровень апгрейда
        upgradeLevel++;

        // Сохраняем новое значение золота и уровня апгрейда
        PlayerPrefs.SetInt(AllExpKey, allExp);
        PlayerPrefs.SetInt(UpgradeLevelKey, upgradeLevel);
        PlayerPrefs.Save(); // Сохраняем изменения

        Debug.Log("Я увеличил уровень ЗВЕЗДНОСТИ за рекламу, текущий уровень " + upgradeLevel);
        upgradeLVL.text = upgradeLevel.ToString();
    }
    private void OnDisable()
    {
        YandexGame.RewardVideoEvent -= Rewarded;
    }
    private void Awake()
    {
        // Изначально скрываем изображение рекламы
        StarsADImage.gameObject.SetActive(false);

        // Загружаем значения золота и уровня апгрейда из PlayerPrefs
        allExp = PlayerPrefs.GetInt(AllExpKey, 1500);
        upgradeLevel = PlayerPrefs.GetInt(UpgradeLevelKey, 1);  // Уровень апгрейда начинается с 1
    }

    private void Start()
    {
        // Обновляем UI
        UpdateUI();
    }

    // Метод для обновления UI
    private void UpdateUI()
    {
        // Обновляем текст с количеством золота
        starsText.text = allExp.ToString();

        // Рассчитываем стоимость апгрейда в зависимости от уровня
        int upgradeCost = upgradeLevel * 217;
        upgradeCostText.text = upgradeCost.ToString();

        Debug.Log("Текущий уровень АПГРЕЙДА ЗВЕЗДНОСТИ " + upgradeLevel);
        upgradeLVL.text = upgradeLevel.ToString();
        // Проверяем, достаточно ли золота для апгрейда
        if (allExp >= upgradeCost)
        {
            // Если достаточно золота, активируем кнопку
            upgradeButton.interactable = true;
        }
        else
        {
            // Если золота недостаточно, показываем рекламу
            upgradeButton.interactable = false;
            StarsADImage.gameObject.SetActive(true);  // Показываем изображение рекламы
        }
    }

    // Метод для обработки нажатия на кнопку апгрейда
    public void OnUpgradeButtonPressed()
    {
        int upgradeCost = upgradeLevel * 217;

        // Проверяем, достаточно ли золота для списания
        if (allExp >= upgradeCost)
        {
            // Списываем золото
            allExp -= upgradeCost;

            // Увеличиваем уровень апгрейда
            upgradeLevel++;

            // Сохраняем новое значение золота и уровня апгрейда
            PlayerPrefs.SetInt(AllExpKey, allExp);
            PlayerPrefs.SetInt(UpgradeLevelKey, upgradeLevel);
            PlayerPrefs.Save(); // Сохраняем изменения
            YandexGame.NewLeaderboardScores("Levels", upgradeLevel);
            // Обновляем UI
            UpdateUI();
        }
        else
        {
            Debug.Log("Недостаточно золота для апгрейда!");
        }
    }
}