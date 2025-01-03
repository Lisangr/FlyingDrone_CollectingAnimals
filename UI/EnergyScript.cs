using UnityEngine;
using UnityEngine.UI;
using YG;

public class EnergyScript : MonoBehaviour
{
    public Image energyImage;
    public GameObject losePanel;
    private float totalEnergy = 50f;
    [SerializeField] private float currentEnergy;
    private int AdID = 1; //Энергия
    private const string UpgradeLevelKey = "EnergyUpgradeLevel"; // Ключ для хранения уровня апгрейда
    private int upgradeLevel = 0;      // Уровень апгрейда
    private void OnEnable()
    {
        CollectableItems.OnCollectedEnergy += CollectableItems_OnCollectedEnergy;
        YandexGame.RewardVideoEvent += Rewarded;
    }

    private void OnDisable()
    {
        CollectableItems.OnCollectedEnergy -= CollectableItems_OnCollectedEnergy;
        YandexGame.RewardVideoEvent -= Rewarded;
    }
    public void Rewarded(int id)
    {
        if (id == AdID)
            AddEnergy();        
    }

    private void AddEnergy()
    {
        currentEnergy = totalEnergy;
        losePanel.SetActive(false);
    }

    void Start()
    {        
        losePanel.SetActive(false);

        upgradeLevel = PlayerPrefs.GetInt(UpgradeLevelKey, 0);
        totalEnergy += upgradeLevel;  
        currentEnergy = totalEnergy;
    }
    private void Update()
    {
        currentEnergy -= Time.deltaTime;

        if (currentEnergy <= 0)
        {
            losePanel.SetActive(true);
        }

        UpdateExpUI();
    }

    private void CollectableItems_OnCollectedEnergy(int t)
    {
        currentEnergy += t;
        if (currentEnergy > totalEnergy)
        {
            currentEnergy = totalEnergy;
        }
        UpdateExpUI();
    }

    private void UpdateExpUI()
    {
        energyImage.fillAmount = currentEnergy / totalEnergy;
        Time.timeScale = 1f;
    }
}
