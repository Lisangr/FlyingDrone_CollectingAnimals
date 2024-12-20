using UnityEngine;
using UnityEngine.UI;
using YG;

public class EnergyUpper : MonoBehaviour
{
    public Text goldText;              // Текст для отображения золота
    public Text upgradeCostText;       // Текст для отображения стоимости апгрейда
    public Button upgradeButton;       // Кнопка для апгрейда
    public Image EnergyADImage;        // Изображение с рекламой
    public Text upgradeLVL;

    private int allGold;               // Все золото
    private int upgradeLevel = 1;      // Уровень апгрейда
    private const string AllGoldKey = "PlayerGold";  // Ключ для хранения золота в PlayerPrefs
    private const string UpgradeLevelKey = "EnergyUpgradeLevel"; // Ключ для хранения уровня апгрейда

    private int AdID = 3; //Уровень энергии
    private void OnEnable()
    {
        YandexGame.RewardVideoEvent += Rewarded;
        GoldBooster.OnCollected += GoldBooster_OnCollected;
    }

    public void GoldBooster_OnCollected()
    {
        allGold = PlayerPrefs.GetInt(AllGoldKey, 0);  // Обновляем значение золота
        UpdateUI();  // Обновляем UI, теперь картинка рекламы исчезнет, если золота достаточно
    }

    public void Rewarded(int id)
    {
        if (id == AdID)
            AddEnergyLVL();
    }
    private void AddEnergyLVL()
    {
        // Увеличиваем уровень апгрейда
        upgradeLevel++;

        // Сохраняем новое значение золота и уровня апгрейда
        PlayerPrefs.SetInt(AllGoldKey, allGold);
        PlayerPrefs.SetInt(UpgradeLevelKey, upgradeLevel);
        PlayerPrefs.Save(); // Сохраняем изменения

        Debug.Log("Я увеличил уровень ЭНЕРГИИ за рекламу, текущий уровень" + upgradeLevel);
        upgradeLVL.text = upgradeLevel.ToString();
    }
    private void OnDisable()
    {
        YandexGame.RewardVideoEvent -= Rewarded;
        GoldBooster.OnCollected -= GoldBooster_OnCollected;
    }
    private void Awake()
    {
        // Изначально скрываем изображение рекламы
        EnergyADImage.gameObject.SetActive(false);

        // Загружаем значения золота и уровня апгрейда из PlayerPrefs
        allGold = PlayerPrefs.GetInt(AllGoldKey, 0);
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
        allGold = PlayerPrefs.GetInt(AllGoldKey, 0);  // Перезагружаем актуальное значение золота
        goldText.text = allGold.ToString();

        // Рассчитываем стоимость апгрейда в зависимости от уровня
        int upgradeCost = upgradeLevel * 100;
        upgradeCostText.text = upgradeCost.ToString();
        upgradeLVL.text = upgradeLevel.ToString();
        Debug.Log("Текущий уровень АПГРЕЙДА ЭНЕРГИИ " + upgradeLevel);
        // Проверяем, достаточно ли золота для апгрейда

        if (allGold >= upgradeCost)
        {
            // Если достаточно золота, активируем кнопку
            upgradeButton.interactable = true;
        }
        else
        {
            // Если золота недостаточно, показываем рекламу
            upgradeButton.interactable = false;
            EnergyADImage.gameObject.SetActive(true);  // Показываем изображение рекламы
        }
    }

    // Метод для обработки нажатия на кнопку апгрейда
    public void OnUpgradeButtonPressed()
    {
        allGold = PlayerPrefs.GetInt(AllGoldKey, 0);  // Перезагружаем актуальное значение золота
        int upgradeCost = upgradeLevel * 100;

        // Проверяем, достаточно ли золота для списания
        if (allGold >= upgradeCost)
        {
            // Списываем золото
            allGold -= upgradeCost;

            // Увеличиваем уровень апгрейда
            upgradeLevel++;

            // Сохраняем новое значение золота и уровня апгрейда
            PlayerPrefs.SetInt(AllGoldKey, allGold);
            PlayerPrefs.SetInt(UpgradeLevelKey, upgradeLevel);
            PlayerPrefs.Save(); // Сохраняем изменения

            // Обновляем UI
            UpdateUI();
        }
        else
        {
            Debug.Log("Недостаточно золота для апгрейда!");
        }
    }
}
