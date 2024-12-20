using UnityEngine;
using UnityEngine.UI;

public class AimChanger : MonoBehaviour
{
    [System.Serializable]
    public class OptionObject
    {
        public string optionName; // ���� ��� ����������
        public GameObject targetObject; // ������, ������� ����� ��������������
    }

    public OptionObject[] optionObjects; // ������ �����
    public Text currentOptionText;       // ����� ��� ����������� �������� ������ (�����������)
    public Button startButton;           // ������ Start ��� ���������� ������������
    public int upgradeLevel;             // ������� ��������, ������� ����������������

    private int currentIndex = 0;        // ������� ������ ���������� �������
    private const int LevelsPerDrone = 7; // ���������� ������� �� ��������� ������ �����

    void Start()
    {
        // ��������� ����������� ����� ��� ����� "BaseAim" �� ���������
        string selectedOption = PlayerPrefs.GetString("SelectedOption", "BaseAim");

        // ��������� ������� ������� ��������
        upgradeLevel = PlayerPrefs.GetInt("StarsUpgradeLevel", 1);

        // ������� ������ �������, ���������������� ������������ �����
        for (int i = 0; i < optionObjects.Length; i++)
        {
            if (optionObjects[i].optionName == selectedOption)
            {
                currentIndex = i;
                break;
            }
        }

        // ��������� ��������� ������ � ��������
        UpdateAimObjects();
    }

    // ����� ��� ������������ �� ��������� ������
    public void NextOption()
    {
        currentIndex = (currentIndex + 1) % optionObjects.Length;
        UpdateAimObjects();
    }

    // ����� ��� ������������ �� ���������� ������
    public void PreviousOption()
    {
        currentIndex = (currentIndex - 1 + optionObjects.Length) % optionObjects.Length;
        UpdateAimObjects();
    }

    // ����� ��� ���������� ��������� �������� � ������ StartButton
    private void UpdateAimObjects()
    {
        for (int i = 0; i < optionObjects.Length; i++)
        {
            // ���������� ������ ������� ������
            optionObjects[i].targetObject.SetActive(i == currentIndex);
        }

        // ��������� ����������� ������ Start � ����������� �� ������
        if (startButton != null)
        {
            if (currentIndex > 0 && upgradeLevel < (currentIndex * LevelsPerDrone))
            {
                startButton.interactable = false; // ������������ ������, ���� ������� ������������
            }
            else
            {
                startButton.interactable = true; // ���������� ������
            }
        }

        // ��������� ������� ����� � PlayerPrefs
        PlayerPrefs.SetString("SelectedOption", optionObjects[currentIndex].optionName);
        PlayerPrefs.Save();

        // ��������� ����� (���� �������������)
        if (currentOptionText != null)
        {
            currentOptionText.text = $"Current Aim: {optionObjects[currentIndex].optionName}";
        }
    }

    // ����� ��� ������������� ������ ��������
    public void UpdateUpgradeLevel(int level)
    {
        upgradeLevel = level;
        UpdateAimObjects(); // ������������� ����������� ������ � ��������
    }
}




















/*using UnityEngine;
using UnityEngine.UI;

public class AimChanger : MonoBehaviour
{
    [System.Serializable]
    public class OptionObject
    {
        public string optionName; // ���� ��� ����������
        public GameObject targetObject; // ������, ������� ����� ��������������
    }

    public OptionObject[] optionObjects; // ������ �����
    public Text currentOptionText; // ����� ��� ����������� �������� ������ (�����������)

    private int currentIndex = 0; // ������� ������ ���������� �������

    void Start()
    {
        // ��������� ����������� ����� ��� ����� "BaseAim" �� ���������
        string selectedOption = PlayerPrefs.GetString("SelectedOption", "BaseAim");

        // ������� ������ �������, ���������������� ������������ �����
        for (int i = 0; i < optionObjects.Length; i++)
        {
            if (optionObjects[i].optionName == selectedOption)
            {
                currentIndex = i;
                break;
            }
        }

        // ���������� ������ �� ������ ������������ �����
        UpdateAimObjects();
    }

    // ����� ��� ������������ �� ��������� ������
    public void NextOption()
    {
        currentIndex = (currentIndex + 1) % optionObjects.Length;
        UpdateAimObjects();
    }

    // ����� ��� ������������ �� ���������� ������
    public void PreviousOption()
    {
        currentIndex = (currentIndex - 1 + optionObjects.Length) % optionObjects.Length;
        UpdateAimObjects();
    }

    // ����� ��� ���������� ��������� ��������
    private void UpdateAimObjects()
    {
        // ���������� ��� ������� � �������� ������ �������
        for (int i = 0; i < optionObjects.Length; i++)
        {
            optionObjects[i].targetObject.SetActive(i == currentIndex);
        }

        // ��������� ������� ����� � PlayerPrefs
        PlayerPrefs.SetString("SelectedOption", optionObjects[currentIndex].optionName);
        PlayerPrefs.Save();

        // ��������� ����� (���� �������������)
        if (currentOptionText != null)
        {
            currentOptionText.text = $"Current Aim: {optionObjects[currentIndex].optionName}";
        }
    }
}
*/























/*using UnityEngine;

public class AimChanger : MonoBehaviour
{
    [System.Serializable]
    public class OptionObject
    {
        public string optionName;
        public GameObject targetObject;
    }

    public OptionObject[] optionObjects;

    void Start()
    {
        string selectedOption = PlayerPrefs.GetString("SelectedOption", "BaseAim");

        foreach (var option in optionObjects)
        {
            if (option.optionName == selectedOption)
            {
                option.targetObject.SetActive(true);
            }
            else
            {
                option.targetObject.SetActive(false);
            }
        }
    }
}
*/