using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    public Transform target; // Объект, вокруг которого вращается камера
    public float angularSpeed = 30f; // Угловая скорость (градусы в секунду)
    public Vector3 axis = Vector3.up; // Ось вращения (по умолчанию вверх)

    void Update()
    {
        if (target != null)
        {
            // Рассчитываем угол поворота за текущий кадр
            float angle = angularSpeed * Time.deltaTime;

            // Вращаем камеру вокруг цели
            transform.RotateAround(target.position, axis, angle);
        }
    }
}
