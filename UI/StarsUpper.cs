using UnityEngine;
using UnityEngine.UI;
using YG;

public class StarsUpper : MonoBehaviour
{   
    public Text starsText;              // ����� ��� ����������� ������
    public Text upgradeCostText;       // ����� ��� ����������� ��������� ��������
    public Button upgradeButton;       // ������ ��� ��������
    public Image StarsADImage;        // ����������� � ��������
    public Text upgradeLVL;
    private int allExp;               // ���� ����
    private int upgradeLevel = 1;      // ������� ��������
    private const string AllExpKey = "PlayerExperience";  // ���� ��� �������� ������ � PlayerPrefs
    private const string UpgradeLevelKey = "StarsUpgradeLevel"; // ���� ��� �������� ������ ��������

    private int AdID = 4; //������� ����������
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
        // ����������� ������� ��������
        upgradeLevel++;

        // ��������� ����� �������� ������ � ������ ��������
        PlayerPrefs.SetInt(AllExpKey, allExp);
        PlayerPrefs.SetInt(UpgradeLevelKey, upgradeLevel);
        PlayerPrefs.Save(); // ��������� ���������

        Debug.Log("� �������� ������� ���������� �� �������, ������� ������� " + upgradeLevel);
        upgradeLVL.text = upgradeLevel.ToString();
    }
    private void OnDisable()
    {
        YandexGame.RewardVideoEvent -= Rewarded;
    }
    private void Awake()
    {
        // ���������� �������� ����������� �������
        StarsADImage.gameObject.SetActive(false);

        // ��������� �������� ������ � ������ �������� �� PlayerPrefs
        allExp = PlayerPrefs.GetInt(AllExpKey, 1500);
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
        // ��������� ����� � ����������� ������
        starsText.text = allExp.ToString();

        // ������������ ��������� �������� � ����������� �� ������
        int upgradeCost = upgradeLevel * 217;
        upgradeCostText.text = upgradeCost.ToString();

        Debug.Log("������� ������� �������� ���������� " + upgradeLevel);
        upgradeLVL.text = upgradeLevel.ToString();
        // ���������, ���������� �� ������ ��� ��������
        if (allExp >= upgradeCost)
        {
            // ���� ���������� ������, ���������� ������
            upgradeButton.interactable = true;
        }
        else
        {
            // ���� ������ ������������, ���������� �������
            upgradeButton.interactable = false;
            StarsADImage.gameObject.SetActive(true);  // ���������� ����������� �������
        }
    }

    // ����� ��� ��������� ������� �� ������ ��������
    public void OnUpgradeButtonPressed()
    {
        int upgradeCost = upgradeLevel * 217;

        // ���������, ���������� �� ������ ��� ��������
        if (allExp >= upgradeCost)
        {
            // ��������� ������
            allExp -= upgradeCost;

            // ����������� ������� ��������
            upgradeLevel++;

            // ��������� ����� �������� ������ � ������ ��������
            PlayerPrefs.SetInt(AllExpKey, allExp);
            PlayerPrefs.SetInt(UpgradeLevelKey, upgradeLevel);
            PlayerPrefs.Save(); // ��������� ���������
            YandexGame.NewLeaderboardScores("Levels", upgradeLevel);
            // ��������� UI
            UpdateUI();
        }
        else
        {
            Debug.Log("������������ ������ ��� ��������!");
        }
    }
}