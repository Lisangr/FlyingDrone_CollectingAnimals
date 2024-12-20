using System;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class StarsUpper : MonoBehaviour
{
    [System.Serializable]
    public class OptionObject
    {
        public string optionName; // Ключ для сохранения
        public GameObject targetObject; // Объект, который будет активироваться
    }

    public Text starsText;              // Текст для отображения золота
    public Text upgradeCostText;        // Текст для отображения стоимости апгрейда
    public Button upgradeButton;        // Кнопка для апгрейда
    public Image StarsADImage;          // Изображение с рекламой
    public Text upgradeLVL;             // Текст уровня апгрейда
    public OptionObject[] optionObjects; // Массив объектов для управления доступностью
    public Button StartButton;          // Кнопка Start

    private int allExp;                 // Весь опыт
    private int upgradeLevel = 1;       // Уровень апгрейда
    private int currentIndex = 0;       // Индекс текущего выбранного объекта
    private const string AllExpKey = "PlayerExperience";  // Ключ для хранения золота
    private const string UpgradeLevelKey = "StarsUpgradeLevel"; // Ключ для уровня апгрейда
    private const string SelectedOptionKey = "SelectedOption";  // Ключ для выбранного дрона
    private const int LevelsPerDrone = 7; // Количество уровней на активацию нового дрона
    private int AdID = 4;               // Уровень звездности

    private void OnEnable()
    {
        YandexGame.RewardVideoEvent += Rewarded;
    }

    private void OnDisable()
    {
        YandexGame.RewardVideoEvent -= Rewarded;
    }

    private void Awake()
    {
        StarsADImage.gameObject.SetActive(false);

        // Загружаем сохраненные данные
        allExp = PlayerPrefs.GetInt(AllExpKey, 0);
        upgradeLevel = PlayerPrefs.GetInt(UpgradeLevelKey, 1);
        string selectedOption = PlayerPrefs.GetString(SelectedOptionKey, optionObjects[0].optionName);

        // Определяем текущий индекс на основе сохраненного выбора
        for (int i = 0; i < optionObjects.Length; i++)
        {
            if (optionObjects[i].optionName == selectedOption)
            {
                currentIndex = i;
                break;
            }
        }
    }

    private void Start()
    {
        UpdateUI();
        UpdateOptionObjects();
    }

    public void Rewarded(int id)
    {
        if (id == AdID)
        {
            AddStarsLVL();
        }
    }

    private void AddStarsLVL()
    {
        upgradeLevel++;
        PlayerPrefs.SetInt(AllExpKey, allExp);
        PlayerPrefs.SetInt(UpgradeLevelKey, upgradeLevel);
        PlayerPrefs.Save();

        Debug.Log("Я увеличил уровень ЗВЕЗДНОСТИ за рекламу, текущий уровень " + upgradeLevel);
        upgradeLVL.text = upgradeLevel.ToString();

        UpdateOptionObjects();
    }

    private void UpdateUI()
    {
        starsText.text = allExp.ToString();

        int upgradeCost = upgradeLevel * 210;
        upgradeCostText.text = upgradeCost.ToString();

        Debug.Log("Текущий уровень АПГРЕЙДА ЗВЕЗДНОСТИ " + upgradeLevel);
        upgradeLVL.text = upgradeLevel.ToString();

        if (allExp >= upgradeCost)
        {
            upgradeButton.interactable = true;
        }
        else
        {
            upgradeButton.interactable = false;
            StarsADImage.gameObject.SetActive(true);
        }
    }
    private void UpdateOptionObjects()
    {
        // Обновляем доступность объектов в StarsUpper
        for (int i = 0; i < optionObjects.Length; i++)
        {
            if (i == 0 || upgradeLevel >= (i * LevelsPerDrone))
            {
                optionObjects[i].targetObject.SetActive(true);
            }
            else
            {
                optionObjects[i].targetObject.SetActive(false);
            }
        }

        // Уведомляем AimChanger об изменении уровня
        AimChanger aimChanger = FindObjectOfType<AimChanger>();
        if (aimChanger != null)
        {
            aimChanger.UpdateUpgradeLevel(upgradeLevel);
        }
    }

    public void OnUpgradeButtonPressed()
    {
        int upgradeCost = upgradeLevel * 210;

        if (allExp >= upgradeCost)
        {
            allExp -= upgradeCost;
            upgradeLevel++;

            PlayerPrefs.SetInt(AllExpKey, allExp);
            PlayerPrefs.SetInt(UpgradeLevelKey, upgradeLevel);
            PlayerPrefs.Save();

            YandexGame.NewLeaderboardScores("Levels", upgradeLevel);

            UpdateUI();
            UpdateOptionObjects();
        }
        else
        {
            Debug.Log("Недостаточно золота для апгрейда!");
        }
    }

    public void SetCurrentDrone(int index)
    {
        currentIndex = index;

        // Сохраняем выбор
        PlayerPrefs.SetString(SelectedOptionKey, optionObjects[currentIndex].optionName);
        PlayerPrefs.Save();

        UpdateOptionObjects();
    }
}
























/*
using UnityEngine;
using UnityEngine.UI;
using YG;

public class StarsUpper : MonoBehaviour
{
    [System.Serializable]
    public class OptionObject
    {
        public string optionName; // Ключ для сохранения
        public GameObject targetObject; // Объект, который будет активироваться
    }

    public Text starsText;              // Текст для отображения золота
    public Text upgradeCostText;        // Текст для отображения стоимости апгрейда
    public Button upgradeButton;        // Кнопка для апгрейда
    public Image StarsADImage;          // Изображение с рекламой
    public Text upgradeLVL;             // Текст уровня апгрейда
    public OptionObject[] optionObjects; // Массив объектов для управления доступностью
    public Button StartButton;          // Кнопка Start

    private int allExp;                 // Весь опыт
    private int upgradeLevel = 1;       // Уровень апгрейда
    private const string AllExpKey = "PlayerExperience";  // Ключ для хранения золота
    private const string UpgradeLevelKey = "StarsUpgradeLevel"; // Ключ для уровня апгрейда
    private int AdID = 4;               // Уровень звездности

    private void OnEnable()
    {
        YandexGame.RewardVideoEvent += Rewarded;
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
        upgradeLevel = PlayerPrefs.GetInt(UpgradeLevelKey, 1); // Уровень апгрейда начинается с 1
    }

    private void Start()
    {
        // Обновляем UI
        UpdateUI();

        // Проверяем доступность объектов в массиве
        UpdateOptionObjects();
    }

    public void Rewarded(int id)
    {
        if (id == AdID)
        {
            AddStarsLVL();
        }
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

        // Проверяем доступность объектов в массиве
        UpdateOptionObjects();
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
            StarsADImage.gameObject.SetActive(true); // Показываем изображение рекламы
        }
    }

    // Метод для проверки доступности объектов в массиве и управления кнопкой Start
    private void UpdateOptionObjects()
    {
        for (int i = 0; i < optionObjects.Length; i++)
        {
            // Первый объект всегда активен, остальные каждые 7 уровней
            if (i == 0 || upgradeLevel >= (i * 7))
            {
                optionObjects[i].targetObject.SetActive(true);
            }
            else
            {
                optionObjects[i].targetObject.SetActive(false);
            }
        }

        // Управление активацией кнопки StartButton
        if (StartButton != null)
        {
            if (upgradeLevel < (currentIndex * 7))
            {
                StartButton.interactable = false; // Деактивируем кнопку
            }
            else
            {
                StartButton.interactable = true; // Активируем кнопку
            }
        }

        // Уведомляем AimChanger об изменении уровня
        AimChanger aimChanger = FindObjectOfType<AimChanger>();
        if (aimChanger != null)
        {
            aimChanger.UpdateUpgradeLevel(upgradeLevel);
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

            // Проверяем доступность объектов в массиве
            UpdateOptionObjects();
        }
        else
        {
            Debug.Log("Недостаточно золота для апгрейда!");
        }
    }
}
*/

























/*using UnityEngine;
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
}*/