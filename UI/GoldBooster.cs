using UnityEngine;
using UnityEngine.UI;
using YG;

public class GoldBooster : MonoBehaviour
{
    public Text goldText;              // Текст для отображения золота
    private int allGold;               // Все золото
    private const string AllGoldKey = "PlayerGold";  // Ключ для хранения золота в PlayerPrefs
    private const string UpgradeLevelKey = "EnergyUpgradeLevel"; // Ключ для хранения уровня апгрейда
    private int AdID = 5; //Количество золота
    public Image EnergyADImage;        // Изображение с рекламой
    public delegate void ADAction();
    public static event ADAction OnCollected;
    public Button upgradeButton;       // Кнопка для апгрейда
    private void OnEnable()
    {
        YandexGame.RewardVideoEvent += Rewarded;
    }

    public void Rewarded(int id)
    {
        if (id == AdID)
            AddEnergyLVL();
    }
    public void AddEnergyLVL()
    {
        allGold = PlayerPrefs.GetInt(AllGoldKey, 1000);
        allGold += 2500;
        OnCollected?.Invoke();  // Уведомляем об изменении золота (событие)

        // Сохраняем новое значение золота и уровня апгрейда
        PlayerPrefs.SetInt(AllGoldKey, allGold);
        PlayerPrefs.Save(); // Сохраняем изменения

        UpdateUI();
        Debug.Log("Я увеличил уровень ЗОЛОТО за рекламу, всего золота " + PlayerPrefs.GetInt(AllGoldKey, 1000));
    }
    private void OnDisable()
    {
        YandexGame.RewardVideoEvent -= Rewarded;
    }
    private void Awake()
    {
        allGold = PlayerPrefs.GetInt(AllGoldKey, 1000);
        UpdateUI();
    }
    private void UpdateUI()
    {
        allGold = PlayerPrefs.GetInt(AllGoldKey, 1000);  // Перезагружаем актуальное значение золота
        goldText.text = allGold.ToString();
        int upgradeLevel = PlayerPrefs.GetInt(UpgradeLevelKey, 1);  // Уровень апгрейда начинается с 1
        // Рассчитываем стоимость апгрейда в зависимости от уровня
        int upgradeCost = upgradeLevel * 100;

        Debug.Log("Текущий уровень АПГРЕЙДА ЭНЕРГИИ " + upgradeLevel);
        // Проверяем, достаточно ли золота для апгрейда

        if (allGold >= upgradeCost)
        {
            // Если золота недостаточно, показываем рекламу
            upgradeButton.interactable = true;
            EnergyADImage.gameObject.SetActive(false);
        }
        else
        {
            EnergyADImage.gameObject.SetActive(true);  // Показываем изображение рекламы
        }
    }

}
