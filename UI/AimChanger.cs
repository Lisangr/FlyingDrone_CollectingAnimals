using UnityEngine;

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
