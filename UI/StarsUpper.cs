using System;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class StarsUpper : MonoBehaviour
{
    [System.Serializable]
    public class OptionObject
    {
        public string optionName; // ���� ��� ����������
        public GameObject targetObject; // ������, ������� ����� ��������������
    }

    public Text starsText;              // ����� ��� ����������� ������
    public Text upgradeCostText;        // ����� ��� ����������� ��������� ��������
    public Button upgradeButton;        // ������ ��� ��������
    public Image StarsADImage;          // ����������� � ��������
    public Text upgradeLVL;             // ����� ������ ��������
    public OptionObject[] optionObjects; // ������ �������� ��� ���������� ������������
    public Button StartButton;          // ������ Start

    private int allExp;                 // ���� ����
    private int upgradeLevel = 1;       // ������� ��������
    private int currentIndex = 0;       // ������ �������� ���������� �������
    private const string AllExpKey = "PlayerExperience";  // ���� ��� �������� ������
    private const string UpgradeLevelKey = "StarsUpgradeLevel"; // ���� ��� ������ ��������
    private const string SelectedOptionKey = "SelectedOption";  // ���� ��� ���������� �����
    private const int LevelsPerDrone = 7; // ���������� ������� �� ��������� ������ �����
    private int AdID = 4;               // ������� ����������

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

        // ��������� ����������� ������
        allExp = PlayerPrefs.GetInt(AllExpKey, 0);
        upgradeLevel = PlayerPrefs.GetInt(UpgradeLevelKey, 1);
        string selectedOption = PlayerPrefs.GetString(SelectedOptionKey, optionObjects[0].optionName);

        // ���������� ������� ������ �� ������ ������������ ������
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

        Debug.Log("� �������� ������� ���������� �� �������, ������� ������� " + upgradeLevel);
        upgradeLVL.text = upgradeLevel.ToString();

        UpdateOptionObjects();
    }

    private void UpdateUI()
    {
        starsText.text = allExp.ToString();

        int upgradeCost = upgradeLevel * 210;
        upgradeCostText.text = upgradeCost.ToString();

        Debug.Log("������� ������� �������� ���������� " + upgradeLevel);
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
        // ��������� ����������� �������� � StarsUpper
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

        // ���������� AimChanger �� ��������� ������
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
            Debug.Log("������������ ������ ��� ��������!");
        }
    }

    public void SetCurrentDrone(int index)
    {
        currentIndex = index;

        // ��������� �����
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
        public string optionName; // ���� ��� ����������
        public GameObject targetObject; // ������, ������� ����� ��������������
    }

    public Text starsText;              // ����� ��� ����������� ������
    public Text upgradeCostText;        // ����� ��� ����������� ��������� ��������
    public Button upgradeButton;        // ������ ��� ��������
    public Image StarsADImage;          // ����������� � ��������
    public Text upgradeLVL;             // ����� ������ ��������
    public OptionObject[] optionObjects; // ������ �������� ��� ���������� ������������
    public Button StartButton;          // ������ Start

    private int allExp;                 // ���� ����
    private int upgradeLevel = 1;       // ������� ��������
    private const string AllExpKey = "PlayerExperience";  // ���� ��� �������� ������
    private const string UpgradeLevelKey = "StarsUpgradeLevel"; // ���� ��� ������ ��������
    private int AdID = 4;               // ������� ����������

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
        // ���������� �������� ����������� �������
        StarsADImage.gameObject.SetActive(false);

        // ��������� �������� ������ � ������ �������� �� PlayerPrefs
        allExp = PlayerPrefs.GetInt(AllExpKey, 1500);
        upgradeLevel = PlayerPrefs.GetInt(UpgradeLevelKey, 1); // ������� �������� ���������� � 1
    }

    private void Start()
    {
        // ��������� UI
        UpdateUI();

        // ��������� ����������� �������� � �������
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
        // ����������� ������� ��������
        upgradeLevel++;

        // ��������� ����� �������� ������ � ������ ��������
        PlayerPrefs.SetInt(AllExpKey, allExp);
        PlayerPrefs.SetInt(UpgradeLevelKey, upgradeLevel);
        PlayerPrefs.Save(); // ��������� ���������

        Debug.Log("� �������� ������� ���������� �� �������, ������� ������� " + upgradeLevel);
        upgradeLVL.text = upgradeLevel.ToString();

        // ��������� ����������� �������� � �������
        UpdateOptionObjects();
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
            StarsADImage.gameObject.SetActive(true); // ���������� ����������� �������
        }
    }

    // ����� ��� �������� ����������� �������� � ������� � ���������� ������� Start
    private void UpdateOptionObjects()
    {
        for (int i = 0; i < optionObjects.Length; i++)
        {
            // ������ ������ ������ �������, ��������� ������ 7 �������
            if (i == 0 || upgradeLevel >= (i * 7))
            {
                optionObjects[i].targetObject.SetActive(true);
            }
            else
            {
                optionObjects[i].targetObject.SetActive(false);
            }
        }

        // ���������� ���������� ������ StartButton
        if (StartButton != null)
        {
            if (upgradeLevel < (currentIndex * 7))
            {
                StartButton.interactable = false; // ������������ ������
            }
            else
            {
                StartButton.interactable = true; // ���������� ������
            }
        }

        // ���������� AimChanger �� ��������� ������
        AimChanger aimChanger = FindObjectOfType<AimChanger>();
        if (aimChanger != null)
        {
            aimChanger.UpdateUpgradeLevel(upgradeLevel);
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

            // ��������� ����������� �������� � �������
            UpdateOptionObjects();
        }
        else
        {
            Debug.Log("������������ ������ ��� ��������!");
        }
    }
}
*/

























/*using UnityEngine;
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
}*/