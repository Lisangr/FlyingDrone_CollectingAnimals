using UnityEngine;
using UnityEngine.UI;
using YG;

public class GoldBooster : MonoBehaviour
{
    public Text goldText;              // ����� ��� ����������� ������
    private int allGold;               // ��� ������
    private const string AllGoldKey = "PlayerGold";  // ���� ��� �������� ������ � PlayerPrefs
    private const string UpgradeLevelKey = "EnergyUpgradeLevel"; // ���� ��� �������� ������ ��������
    private int AdID = 5; //���������� ������
    public Image EnergyADImage;        // ����������� � ��������
    public delegate void ADAction();
    public static event ADAction OnCollected;
    public Button upgradeButton;       // ������ ��� ��������
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
        OnCollected?.Invoke();  // ���������� �� ��������� ������ (�������)

        // ��������� ����� �������� ������ � ������ ��������
        PlayerPrefs.SetInt(AllGoldKey, allGold);
        PlayerPrefs.Save(); // ��������� ���������

        UpdateUI();
        Debug.Log("� �������� ������� ������ �� �������, ����� ������ " + PlayerPrefs.GetInt(AllGoldKey, 1000));
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
        allGold = PlayerPrefs.GetInt(AllGoldKey, 1000);  // ������������� ���������� �������� ������
        goldText.text = allGold.ToString();
        int upgradeLevel = PlayerPrefs.GetInt(UpgradeLevelKey, 1);  // ������� �������� ���������� � 1
        // ������������ ��������� �������� � ����������� �� ������
        int upgradeCost = upgradeLevel * 100;

        Debug.Log("������� ������� �������� ������� " + upgradeLevel);
        // ���������, ���������� �� ������ ��� ��������

        if (allGold >= upgradeCost)
        {
            // ���� ������ ������������, ���������� �������
            upgradeButton.interactable = true;
            EnergyADImage.gameObject.SetActive(false);
        }
        else
        {
            EnergyADImage.gameObject.SetActive(true);  // ���������� ����������� �������
        }
    }

}
