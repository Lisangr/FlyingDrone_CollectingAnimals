using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public Transform player; // Игрок
    public Vector3 offset = new Vector3(0, 40, -10); // Смещение камеры относительно игрока
    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>().GetComponent<Transform>();
    }
    void LateUpdate()
    {
        // Если игрок не указан, не выполняем обновление
        if (player == null) return;

        // Обновляем позицию камеры с учётом смещения
        Vector3 desiredPosition = player.position + player.rotation * offset;
        transform.position = desiredPosition;

        // Устанавливаем фиксированный угол наклона по оси X
        Quaternion fixedRotation = Quaternion.Euler(90, player.eulerAngles.y, 0);
        transform.rotation = fixedRotation;
    }
}
