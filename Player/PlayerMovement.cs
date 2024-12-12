/*
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float keyboardSpeed = 5f; // �������� ���������� ������������
    public float swipeSpeed = 10f;   // �������� ����������� �� ����� ������
    public float smoothTime = 0f;  // ����� ����������� ��������

    [Header("Movement Bounds")]
    public Vector3 bottomLeftCorner = new Vector3(-68.25f, -0.25f, 4.5f); // ����� ������ ���� ��������
    public Vector3 topRightCorner = new Vector3(-9.625f, -0.25f, 61.75f); // ������ ������� ���� ��������

    private Vector3 targetPosition;  // ������� ������� ��� ������
    private Vector3 velocity = Vector3.zero; // �������� ��� �����������


    private void Start()
    {
        // ������������� ������� ������� ��� ��������� ������� �������
        targetPosition = transform.position;

    }

    private void Update()
    {
        HandleKeyboardMovement();
        HandleMouseMovement();

        // ������� ����������� ������
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }

    private void HandleKeyboardMovement()
    {
        // �������� ���� ������������ (W/S ��� �������� �����/�����, A/D ��� �������� �����/������)
        float moveZ = Input.GetAxisRaw("Vertical");   // W/S ��� ������� �����/����
        float moveX = Input.GetAxisRaw("Horizontal"); // A/D ��� ������� �����/������

        // �������� �����/����� � ��������� �����������
        if (moveZ != 0)
        {
            Vector3 forwardMovement = transform.forward * moveZ * keyboardSpeed * Time.deltaTime;
            targetPosition += forwardMovement;
        }

        // �������� �����/������ � ��������� �����������
        if (moveX != 0)
        {
            Vector3 sideMovement = transform.right * moveX * keyboardSpeed * Time.deltaTime;
            targetPosition += sideMovement;
        }

        // ��������� ����������� �� �������
        targetPosition.x = Mathf.Clamp(targetPosition.x, bottomLeftCorner.x, topRightCorner.x);
        targetPosition.z = Mathf.Clamp(targetPosition.z, bottomLeftCorner.z, topRightCorner.z);
        targetPosition.y = bottomLeftCorner.y; // ��������� Y-����������
    }

    private void HandleMouseMovement()
    {
        // ��������� ������� ����� ������ ����
        if (Input.GetMouseButton(0))
        {
            // �������� ���������� ������� �� ������ ������
            Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
            Vector2 mousePosition = Input.mousePosition;
            Vector2 direction = (mousePosition - screenCenter).normalized;

            // ����������� �������� ����������� � ��������� ���������� ������
            Vector3 localDirection = new Vector3(direction.x, 0, direction.y); // �����������
            Vector3 worldDirection = transform.TransformDirection(localDirection); // ��������� � ������� ����������

            // ������������ ������� �������
            targetPosition += worldDirection * swipeSpeed * Time.deltaTime;

            // ��������� �����������
            targetPosition.x = Mathf.Clamp(targetPosition.x, bottomLeftCorner.x, topRightCorner.x);
            targetPosition.z = Mathf.Clamp(targetPosition.z, bottomLeftCorner.z, topRightCorner.z);
            targetPosition.y = bottomLeftCorner.y; // ��������� Y-����������
        }
    }

}
*/
/*
using UnityEngine;
using UnityEngine.EventSystems; // ������������ ���� ��� ������ � UI

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float keyboardSpeed = 5f; // �������� ���������� ������������
    public float swipeSpeed = 10f;   // �������� ����������� �� ����� ������
    public float smoothTime = 0f;  // ����� ����������� ��������

    [Header("Movement Bounds")]
    public Vector3 bottomLeftCorner = new Vector3(-68.25f, -0.25f, 4.5f); // ����� ������ ���� ��������
    public Vector3 topRightCorner = new Vector3(-9.625f, -0.25f, 61.75f); // ������ ������� ���� ��������

    private Vector3 targetPosition;  // ������� ������� ��� ������
    private Vector3 velocity = Vector3.zero; // �������� ��� �����������

    private void Start()
    {
        // ������������� ������� ������� ��� ��������� ������� �������
        targetPosition = transform.position;
    }

    private void Update()
    {
        HandleKeyboardMovement();

        // ���������, ��������� �� ������ ��� ��������� UI
        if (!IsPointerOverUI())
        {
            HandleMouseMovement();
        }

        // ������� ����������� ������
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }

    private void HandleKeyboardMovement()
    {
        float moveZ = Input.GetAxisRaw("Vertical");   // W/S ��� ������� �����/����
        float moveX = Input.GetAxisRaw("Horizontal"); // A/D ��� ������� �����/������

        if (moveZ != 0)
        {
            Vector3 forwardMovement = transform.forward * moveZ * keyboardSpeed * Time.deltaTime;
            targetPosition += forwardMovement;
        }

        if (moveX != 0)
        {
            Vector3 sideMovement = transform.right * moveX * keyboardSpeed * Time.deltaTime;
            targetPosition += sideMovement;
        }

        // ��������� ����������� �� �������
        targetPosition.x = Mathf.Clamp(targetPosition.x, bottomLeftCorner.x, topRightCorner.x);
        targetPosition.z = Mathf.Clamp(targetPosition.z, bottomLeftCorner.z, topRightCorner.z);
        targetPosition.y = bottomLeftCorner.y; // ��������� Y-����������
    }

    private void HandleMouseMovement()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
            Vector2 mousePosition = Input.mousePosition;
            Vector2 direction = (mousePosition - screenCenter).normalized;

            Vector3 localDirection = new Vector3(direction.x, 0, direction.y);
            Vector3 worldDirection = transform.TransformDirection(localDirection);

            targetPosition += worldDirection * swipeSpeed * Time.deltaTime;

            targetPosition.x = Mathf.Clamp(targetPosition.x, bottomLeftCorner.x, topRightCorner.x);
            targetPosition.z = Mathf.Clamp(targetPosition.z, bottomLeftCorner.z, topRightCorner.z);
            targetPosition.y = bottomLeftCorner.y;
        }
    }

    // ���������, ��������� �� ��������� ���� ��� ��������� UI
    private bool IsPointerOverUI()
    {
        return EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();
    }
}
*/

using UnityEngine;
using UnityEngine.EventSystems; // ������������ ���� ��� ������ � UI

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float keyboardSpeed = 5f;  // �������� ���������� ������������
    public float swipeSpeed = 10f;    // �������� ����������� �� ����� ������
    public float smoothTime = 0f;     // ����� ����������� ��������

    [Header("Movement Bounds")]
    public Vector3 bottomLeftCorner = new Vector3(-68.25f, -0.25f, 4.5f); // ����� ������ ���� ��������
    public Vector3 topRightCorner = new Vector3(-9.625f, -0.25f, 61.75f);  // ������ ������� ���� ��������

    private Vector3 targetPosition;    // ������� ������� ��� ������
    private Vector3 velocity = Vector3.zero; // �������� ��� �����������

    private void Start()
    {
        // ������������� ������� ������� ��� ��������� ������� �������
        targetPosition = transform.position;
    }

    private void Update()
    {
        HandleKeyboardMovement();

        // ��������: �������� �����, ������ ���� ������ �� ��� UI
        if (!IsPointerOverUI())
        {
            HandleMouseMovement();
        }

        // ������� ����������� ������
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }

    private void HandleKeyboardMovement()
    {
        float moveZ = Input.GetAxisRaw("Vertical");   // W/S ��� ������� �����/����
        float moveX = Input.GetAxisRaw("Horizontal"); // A/D ��� ������� �����/������

        if (moveZ != 0)
        {
            Vector3 forwardMovement = transform.forward * moveZ * keyboardSpeed * Time.deltaTime;
            targetPosition += forwardMovement;
        }

        if (moveX != 0)
        {
            Vector3 sideMovement = transform.right * moveX * keyboardSpeed * Time.deltaTime;
            targetPosition += sideMovement;
        }

        // ��������� ����������� �� �������
        targetPosition.x = Mathf.Clamp(targetPosition.x, bottomLeftCorner.x, topRightCorner.x);
        targetPosition.z = Mathf.Clamp(targetPosition.z, bottomLeftCorner.z, topRightCorner.z);
        targetPosition.y = bottomLeftCorner.y; // ��������� Y-����������
    }

    private void HandleMouseMovement()
    {
        if (Input.GetMouseButton(0)) // ������������ ����������� ��� ������� ����� ������ ����
        {
            Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
            Vector2 mousePosition = Input.mousePosition;
            Vector2 direction = (mousePosition - screenCenter).normalized;

            Vector3 localDirection = new Vector3(direction.x, 0, direction.y);
            Vector3 worldDirection = transform.TransformDirection(localDirection);

            targetPosition += worldDirection * swipeSpeed * Time.deltaTime;

            targetPosition.x = Mathf.Clamp(targetPosition.x, bottomLeftCorner.x, topRightCorner.x);
            targetPosition.z = Mathf.Clamp(targetPosition.z, bottomLeftCorner.z, topRightCorner.z);
            targetPosition.y = bottomLeftCorner.y;
        }
    }

    // ���������, ��������� �� ��������� ���� ��� ��������� UI
    private bool IsPointerOverUI()
    {
        return EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();
    }
}
