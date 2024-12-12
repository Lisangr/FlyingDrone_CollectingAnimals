/*
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float keyboardSpeed = 5f; // Скорость клавишного передвижения
    public float swipeSpeed = 10f;   // Скорость перемещения на длину свайпа
    public float smoothTime = 0f;  // Время сглаживания движения

    [Header("Movement Bounds")]
    public Vector3 bottomLeftCorner = new Vector3(-68.25f, -0.25f, 4.5f); // Левый нижний угол квадрата
    public Vector3 topRightCorner = new Vector3(-9.625f, -0.25f, 61.75f); // Правый верхний угол квадрата

    private Vector3 targetPosition;  // Целевая позиция для игрока
    private Vector3 velocity = Vector3.zero; // Скорость для сглаживания


    private void Start()
    {
        // Устанавливаем текущую позицию как начальную целевую позицию
        targetPosition = transform.position;

    }

    private void Update()
    {
        HandleKeyboardMovement();
        HandleMouseMovement();

        // Плавное перемещение игрока
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }

    private void HandleKeyboardMovement()
    {
        // Получаем ввод пользователя (W/S для движения вперёд/назад, A/D для движения влево/вправо)
        float moveZ = Input.GetAxisRaw("Vertical");   // W/S или стрелки вверх/вниз
        float moveX = Input.GetAxisRaw("Horizontal"); // A/D или стрелки влево/вправо

        // Движение вперёд/назад в локальных координатах
        if (moveZ != 0)
        {
            Vector3 forwardMovement = transform.forward * moveZ * keyboardSpeed * Time.deltaTime;
            targetPosition += forwardMovement;
        }

        // Движение влево/вправо в локальных координатах
        if (moveX != 0)
        {
            Vector3 sideMovement = transform.right * moveX * keyboardSpeed * Time.deltaTime;
            targetPosition += sideMovement;
        }

        // Применяем ограничения на позицию
        targetPosition.x = Mathf.Clamp(targetPosition.x, bottomLeftCorner.x, topRightCorner.x);
        targetPosition.z = Mathf.Clamp(targetPosition.z, bottomLeftCorner.z, topRightCorner.z);
        targetPosition.y = bottomLeftCorner.y; // Фиксируем Y-координату
    }

    private void HandleMouseMovement()
    {
        // Проверяем зажатие левой кнопки мыши
        if (Input.GetMouseButton(0))
        {
            // Получаем отклонение курсора от центра экрана
            Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
            Vector2 mousePosition = Input.mousePosition;
            Vector2 direction = (mousePosition - screenCenter).normalized;

            // Преобразуем экранное направление в локальные координаты игрока
            Vector3 localDirection = new Vector3(direction.x, 0, direction.y); // направление
            Vector3 worldDirection = transform.TransformDirection(localDirection); // Переводим в мировые координаты

            // Рассчитываем целевую позицию
            targetPosition += worldDirection * swipeSpeed * Time.deltaTime;

            // Применяем ограничения
            targetPosition.x = Mathf.Clamp(targetPosition.x, bottomLeftCorner.x, topRightCorner.x);
            targetPosition.z = Mathf.Clamp(targetPosition.z, bottomLeftCorner.z, topRightCorner.z);
            targetPosition.y = bottomLeftCorner.y; // Фиксируем Y-координату
        }
    }

}
*/
/*
using UnityEngine;
using UnityEngine.EventSystems; // Пространство имен для работы с UI

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float keyboardSpeed = 5f; // Скорость клавишного передвижения
    public float swipeSpeed = 10f;   // Скорость перемещения на длину свайпа
    public float smoothTime = 0f;  // Время сглаживания движения

    [Header("Movement Bounds")]
    public Vector3 bottomLeftCorner = new Vector3(-68.25f, -0.25f, 4.5f); // Левый нижний угол квадрата
    public Vector3 topRightCorner = new Vector3(-9.625f, -0.25f, 61.75f); // Правый верхний угол квадрата

    private Vector3 targetPosition;  // Целевая позиция для игрока
    private Vector3 velocity = Vector3.zero; // Скорость для сглаживания

    private void Start()
    {
        // Устанавливаем текущую позицию как начальную целевую позицию
        targetPosition = transform.position;
    }

    private void Update()
    {
        HandleKeyboardMovement();

        // Проверяем, находится ли курсор над элементом UI
        if (!IsPointerOverUI())
        {
            HandleMouseMovement();
        }

        // Плавное перемещение игрока
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }

    private void HandleKeyboardMovement()
    {
        float moveZ = Input.GetAxisRaw("Vertical");   // W/S или стрелки вверх/вниз
        float moveX = Input.GetAxisRaw("Horizontal"); // A/D или стрелки влево/вправо

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

        // Применяем ограничения на позицию
        targetPosition.x = Mathf.Clamp(targetPosition.x, bottomLeftCorner.x, topRightCorner.x);
        targetPosition.z = Mathf.Clamp(targetPosition.z, bottomLeftCorner.z, topRightCorner.z);
        targetPosition.y = bottomLeftCorner.y; // Фиксируем Y-координату
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

    // Проверяем, находится ли указатель мыши над элементом UI
    private bool IsPointerOverUI()
    {
        return EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();
    }
}
*/

using UnityEngine;
using UnityEngine.EventSystems; // Пространство имен для работы с UI

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float keyboardSpeed = 5f;  // Скорость клавишного передвижения
    public float swipeSpeed = 10f;    // Скорость перемещения на длину свайпа
    public float smoothTime = 0f;     // Время сглаживания движения

    [Header("Movement Bounds")]
    public Vector3 bottomLeftCorner = new Vector3(-68.25f, -0.25f, 4.5f); // Левый нижний угол квадрата
    public Vector3 topRightCorner = new Vector3(-9.625f, -0.25f, 61.75f);  // Правый верхний угол квадрата

    private Vector3 targetPosition;    // Целевая позиция для игрока
    private Vector3 velocity = Vector3.zero; // Скорость для сглаживания

    private void Start()
    {
        // Устанавливаем текущую позицию как начальную целевую позицию
        targetPosition = transform.position;
    }

    private void Update()
    {
        HandleKeyboardMovement();

        // Проверка: движение мышью, только если курсор не над UI
        if (!IsPointerOverUI())
        {
            HandleMouseMovement();
        }

        // Плавное перемещение игрока
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }

    private void HandleKeyboardMovement()
    {
        float moveZ = Input.GetAxisRaw("Vertical");   // W/S или стрелки вверх/вниз
        float moveX = Input.GetAxisRaw("Horizontal"); // A/D или стрелки влево/вправо

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

        // Применяем ограничения на позицию
        targetPosition.x = Mathf.Clamp(targetPosition.x, bottomLeftCorner.x, topRightCorner.x);
        targetPosition.z = Mathf.Clamp(targetPosition.z, bottomLeftCorner.z, topRightCorner.z);
        targetPosition.y = bottomLeftCorner.y; // Фиксируем Y-координату
    }

    private void HandleMouseMovement()
    {
        if (Input.GetMouseButton(0)) // Обрабатываем перемещение при нажатой левой кнопке мыши
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

    // Проверяем, находится ли указатель мыши над элементом UI
    private bool IsPointerOverUI()
    {
        return EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();
    }
}
