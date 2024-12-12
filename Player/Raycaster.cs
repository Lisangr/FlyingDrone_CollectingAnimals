using UnityEngine;
using YG;

public class Raycaster : MonoBehaviour
{
    [Header("Raycast Settings")]
    public float rayDistance = 100f; // Дальность рейкаста
    private ArrowsSpawner arrowsSpawner;
    private void Start()
    {
        arrowsSpawner = FindObjectOfType<ArrowsSpawner>();
    }
    private void Update()
    {
        if (YandexGame.EnvironmentData.isDesktop)
        {
            OnKeyboardActive();
        }
    }
    void OnKeyboardActive()
    {
        // Проверяем нажатие клавиши F
        if (Input.GetKeyDown(KeyCode.F))
        {
            arrowsSpawner.SpawnArrow();
            // Создаем луч из центра экрана
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            RaycastHit hit;

            // Если луч попал в объект
            if (Physics.Raycast(ray, out hit, rayDistance))
            {
                // Выводим имя объекта в консоль
                Debug.Log("Hit Object: " + hit.collider.gameObject.name);
            }
            else
            {
                // Если ни в какой объект не попали
                Debug.Log("No object hit.");
            }
        }
    }

    public void OnButtonClick()
    {
        arrowsSpawner.SpawnArrow();
        // Создаем луч из центра экрана
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        // Если луч попал в объект
        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            // Выводим имя объекта в консоль
            Debug.Log("Hit Object: " + hit.collider.gameObject.name);
        }
        else
        {
            // Если ни в какой объект не попали
            Debug.Log("No object hit.");
        }

    }
}