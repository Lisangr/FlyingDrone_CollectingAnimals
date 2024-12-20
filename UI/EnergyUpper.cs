using UnityEngine;
using UnityEngine.UI;
using YG;

public class EnergyUpper : MonoBehaviour
{
    public Text goldText;              // ����� ��� ����������� ������
    public Text upgradeCostText;       // ����� ��� ����������� ��������� ��������
    public Button upgradeButton;       // ������ ��� ��������
    public Image EnergyADImage;        // ����������� � ��������
    public Text upgradeLVL;

    private int allGold;               // ��� ������
    private int upgradeLevel = 1;      // ������� ��������
    private const string AllGoldKey = "PlayerGold";  // ���� ��� �������� ������ � PlayerPrefs
    private const string UpgradeLevelKey = "EnergyUpgradeLevel"; // ���� ��� �������� ������ ��������

    private int AdID = 3; //������� �������
    private void OnEnable()
    {
        YandexGame.RewardVideoEvent += Rewarded;
        GoldBooster.OnCollected += GoldBooster_OnCollected;
    }

    public void GoldBooster_OnCollected()
    {
        allGold = PlayerPrefs.GetInt(AllGoldKey, 0);  // ��������� �������� ������
        UpdateUI();  // ��������� UI, ������ �������� ������� ��������, ���� ������ ����������
    }

    public void Rewarded(int id)
    {
        if (id == AdID)
            AddEnergyLVL();
    }
    private void AddEnergyLVL()
    {
        // ����������� ������� ��������
        upgradeLevel++;

        // ��������� ����� �������� ������ � ������ ��������
        PlayerPrefs.SetInt(AllGoldKey, allGold);
        PlayerPrefs.SetInt(UpgradeLevelKey, upgradeLevel);
        PlayerPrefs.Save(); // ��������� ���������

        Debug.Log("� �������� ������� ������� �� �������, ������� �������" + upgradeLevel);
        upgradeLVL.text = upgradeLevel.ToString();
    }
    private void OnDisable()
    {
        YandexGame.RewardVideoEvent -= Rewarded;
        GoldBooster.OnCollected -= GoldBooster_OnCollected;
    }
    private void Awake()
    {
        // ���������� �������� ����������� �������
        EnergyADImage.gameObject.SetActive(false);

        // ��������� �������� ������ � ������ �������� �� PlayerPrefs
        allGold = PlayerPrefs.GetInt(AllGoldKey, 0);
        upgradeLevel = PlayerPrefs.GetInt(UpgradeLevelKey, 1);  // ������� �������� ���������� � 1
    }

    private void Start()
    {
        // ��������� UI
        UpdateUI();
    }

    // ����� ��� ���������� UI
    private void UpdateUI()
    {
        allGold = PlayerPrefs.GetInt(AllGoldKey, 0);  // ������������� ���������� �������� ������
        goldText.text = allGold.ToString();

        // ������������ ��������� �������� � ����������� �� ������
        int upgradeCost = upgradeLevel * 100;
        upgradeCostText.text = upgradeCost.ToString();
        upgradeLVL.text = upgradeLevel.ToString();
        Debug.Log("������� ������� �������� ������� " + upgradeLevel);
        // ���������, ���������� �� ������ ��� ��������

        if (allGold >= upgradeCost)
        {
            // ���� ���������� ������, ���������� ������
            upgradeButton.interactable = true;
        }
        else
        {
            // ���� ������ ������������, ���������� �������
            upgradeButton.interactable = false;
            EnergyADImage.gameObject.SetActive(true);  // ���������� ����������� �������
        }
    }

    // ����� ��� ��������� ������� �� ������ ��������
    public void OnUpgradeButtonPressed()
    {
        allGold = PlayerPrefs.GetInt(AllGoldKey, 0);  // ������������� ���������� �������� ������
        int upgradeCost = upgradeLevel * 100;

        // ���������, ���������� �� ������ ��� ��������
        if (allGold >= upgradeCost)
        {
            // ��������� ������
            allGold -= upgradeCost;

            // ����������� ������� ��������
            upgradeLevel++;

            // ��������� ����� �������� ������ � ������ ��������
            PlayerPrefs.SetInt(AllGoldKey, allGold);
            PlayerPrefs.SetInt(UpgradeLevelKey, upgradeLevel);
            PlayerPrefs.Save(); // ��������� ���������

            // ��������� UI
            UpdateUI();
        }
        else
        {
            Debug.Log("������������ ������ ��� ��������!");
        }
    }
}
