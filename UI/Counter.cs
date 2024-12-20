using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    [SerializeField] private Text counterText;
    [SerializeField] private int animalsQuantity;
    [SerializeField] private GameObject winscreen;
    private int currentIndex;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("Level"))
        {
            currentIndex = PlayerPrefs.GetInt("Level");
        }
        else
        {
            PlayerPrefs.SetInt("Level", 22);
            PlayerPrefs.Save();
        }
    }
    private void Start()
    {
        counterText = GetComponent<Text>();

        if (winscreen != null)
        {
            winscreen.SetActive(false);
        }
        animalsQuantity = FindObjectsOfType<CollectableItems>().Length;
        counterText.text = animalsQuantity.ToString();

    }
    private void OnEnable()
    {
        CollectableItems.OnCollected += CollectableItems_OnCollected;
    }

    private void CollectableItems_OnCollected()
    {
        animalsQuantity -= 1;
        counterText.text = animalsQuantity.ToString();

        if (animalsQuantity <= 123) //тут мен€ем значение дл€ теста
        {
            Debug.Log("All animals collected, displaying winscreen.");
            winscreen.SetActive(true);

            currentIndex++;
            Debug.Log("“екущий уровень" + currentIndex);
            if (currentIndex == 25)
            {
                currentIndex = Random.Range(2, 25);
                PlayerPrefs.SetInt("Level", currentIndex);
                PlayerPrefs.Save();
            }
            else
            {
                PlayerPrefs.SetInt("Level", currentIndex);
                PlayerPrefs.Save();
            }
        }
    }

    private void OnDisable()
    {
        CollectableItems.OnCollected -= CollectableItems_OnCollected;
    }
}
