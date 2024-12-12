using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    [SerializeField] private Text counterText;
    [SerializeField] private int animalsQuantity;
    [SerializeField] private GameObject winscreen;
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

        if (animalsQuantity <= 0) //тут меняем значение для теста
        {
            Debug.Log("All animals collected, displaying winscreen.");
            winscreen.SetActive(true);
        }
    }

    private void OnDisable()
    {
        CollectableItems.OnCollected -= CollectableItems_OnCollected;
    }
}
