using UnityEngine;
using YG;

public class Raycaster : MonoBehaviour
{
    [Header("Raycast Settings")]
    public float rayDistance = 100f; // ��������� ��������
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
        // ��������� ������� ������� F
        if (Input.GetKeyDown(KeyCode.F))
        {
            arrowsSpawner.SpawnArrow();
            // ������� ��� �� ������ ������
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            RaycastHit hit;

            // ���� ��� ����� � ������
            if (Physics.Raycast(ray, out hit, rayDistance))
            {
                // ������� ��� ������� � �������
                Debug.Log("Hit Object: " + hit.collider.gameObject.name);
            }
            else
            {
                // ���� �� � ����� ������ �� ������
                Debug.Log("No object hit.");
            }
        }
    }

    public void OnButtonClick()
    {
        arrowsSpawner.SpawnArrow();
        // ������� ��� �� ������ ������
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        // ���� ��� ����� � ������
        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            // ������� ��� ������� � �������
            Debug.Log("Hit Object: " + hit.collider.gameObject.name);
        }
        else
        {
            // ���� �� � ����� ������ �� ������
            Debug.Log("No object hit.");
        }

    }
}