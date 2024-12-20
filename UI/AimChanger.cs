using UnityEngine;
using UnityEngine.UI;

public class AimChanger : MonoBehaviour
{
    [System.Serializable]
    public class OptionObject
    {
        public string optionName; // Ключ для сохранения
        public GameObject targetObject; // Объект, который будет активироваться
    }

    public OptionObject[] optionObjects; // Массив опций
    public Text currentOptionText;       // Текст для отображения текущего выбора (опционально)
    public Button startButton;           // Кнопка Start для управления доступностью
    public int upgradeLevel;             // Уровень апгрейда, который синхронизируется

    private int currentIndex = 0;        // Текущий индекс выбранного объекта
    private const int LevelsPerDrone = 7; // Количество уровней на активацию нового дрона

    void Start()
    {
        // Загружаем сохраненный выбор или берем "BaseAim" по умолчанию
        string selectedOption = PlayerPrefs.GetString("SelectedOption", "BaseAim");

        // Загружаем текущий уровень апгрейда
        upgradeLevel = PlayerPrefs.GetInt("StarsUpgradeLevel", 1);

        // Находим индекс объекта, соответствующего сохраненному ключу
        for (int i = 0; i < optionObjects.Length; i++)
        {
            if (optionObjects[i].optionName == selectedOption)
            {
                currentIndex = i;
                break;
            }
        }

        // Обновляем состояние кнопки и объектов
        UpdateAimObjects();
    }

    // Метод для переключения на следующий объект
    public void NextOption()
    {
        currentIndex = (currentIndex + 1) % optionObjects.Length;
        UpdateAimObjects();
    }

    // Метод для переключения на предыдущий объект
    public void PreviousOption()
    {
        currentIndex = (currentIndex - 1 + optionObjects.Length) % optionObjects.Length;
        UpdateAimObjects();
    }

    // Метод для обновления состояния объектов и кнопки StartButton
    private void UpdateAimObjects()
    {
        for (int i = 0; i < optionObjects.Length; i++)
        {
            // Активируем только текущий объект
            optionObjects[i].targetObject.SetActive(i == currentIndex);
        }

        // Проверяем доступность кнопки Start в зависимости от уровня
        if (startButton != null)
        {
            if (currentIndex > 0 && upgradeLevel < (currentIndex * LevelsPerDrone))
            {
                startButton.interactable = false; // Деактивируем кнопку, если уровень недостаточен
            }
            else
            {
                startButton.interactable = true; // Активируем кнопку
            }
        }

        // Сохраняем текущий выбор в PlayerPrefs
        PlayerPrefs.SetString("SelectedOption", optionObjects[currentIndex].optionName);
        PlayerPrefs.Save();

        // Обновляем текст (если предусмотрено)
        if (currentOptionText != null)
        {
            currentOptionText.text = $"Current Aim: {optionObjects[currentIndex].optionName}";
        }
    }

    // Метод для синхронизации уровня апгрейда
    public void UpdateUpgradeLevel(int level)
    {
        upgradeLevel = level;
        UpdateAimObjects(); // Перепроверяем доступность кнопки и объектов
    }
}




















/*using UnityEngine;
using UnityEngine.UI;

public class AimChanger : MonoBehaviour
{
    [System.Serializable]
    public class OptionObject
    {
        public string optionName; // Ключ для сохранения
        public GameObject targetObject; // Объект, который будет активироваться
    }

    public OptionObject[] optionObjects; // Массив опций
    public Text currentOptionText; // Текст для отображения текущего выбора (опционально)

    private int currentIndex = 0; // Текущий индекс выбранного объекта

    void Start()
    {
        // Загружаем сохраненный выбор или берем "BaseAim" по умолчанию
        string selectedOption = PlayerPrefs.GetString("SelectedOption", "BaseAim");

        // Находим индекс объекта, соответствующего сохраненному ключу
        for (int i = 0; i < optionObjects.Length; i++)
        {
            if (optionObjects[i].optionName == selectedOption)
            {
                currentIndex = i;
                break;
            }
        }

        // Активируем объект на основе сохраненного ключа
        UpdateAimObjects();
    }

    // Метод для переключения на следующий объект
    public void NextOption()
    {
        currentIndex = (currentIndex + 1) % optionObjects.Length;
        UpdateAimObjects();
    }

    // Метод для переключения на предыдущий объект
    public void PreviousOption()
    {
        currentIndex = (currentIndex - 1 + optionObjects.Length) % optionObjects.Length;
        UpdateAimObjects();
    }

    // Метод для обновления состояния объектов
    private void UpdateAimObjects()
    {
        // Перебираем все объекты и включаем только текущий
        for (int i = 0; i < optionObjects.Length; i++)
        {
            optionObjects[i].targetObject.SetActive(i == currentIndex);
        }

        // Сохраняем текущий выбор в PlayerPrefs
        PlayerPrefs.SetString("SelectedOption", optionObjects[currentIndex].optionName);
        PlayerPrefs.Save();

        // Обновляем текст (если предусмотрено)
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