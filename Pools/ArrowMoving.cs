using UnityEngine;

public class ArrowMoving : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 3f;
    public int arrowDamage = 100;

    private Vector3 targetPosition; // Позиция цели
    private bool hasTarget = false; // Флаг для проверки цели

    private Rigidbody rb;

    private void Awake()
    {
        // Если используется Rigidbody для управления движением
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        // Таймер для возврата стрелы в пул
        Invoke(nameof(ReturnToPool), lifetime);

        // Сброс начальных параметров
        speed = 10f;
        hasTarget = false;

        if (rb != null)
        {
            rb.velocity = Vector3.zero; // Обнуляем скорость
            rb.angularVelocity = Vector3.zero; // Обнуляем угловую скорость
        }
    }

    private void Update()
    {
        // Если цель задана, двигаемся к ней
        if (hasTarget)
        {
            Vector3 direction = (targetPosition - transform.position).normalized;
            transform.Translate(direction * speed * Time.deltaTime, Space.World);

            // Поворачиваем стрелу в направлении цели
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
        }
    }

    public void SetTarget(Vector3 position)
    {
        targetPosition = position;
        hasTarget = true;
    }

    private void ReturnToPool()
    {
        // Возвращаем стрелу в пул и сбрасываем состояние
        ArrowsPool.Instance.ReturnArrow(this);
    }

    private void OnDisable()
    {
        // Очищаем состояния при отключении
        CancelInvoke();

        if (rb != null)
        {
            rb.velocity = Vector3.zero; // Обнуляем скорость
            rb.angularVelocity = Vector3.zero; // Обнуляем угловую скорость
        }

        hasTarget = false;
        speed = 10f; // Сброс начальной скорости
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            DestroyBullet();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            DestroyBullet();
        }
    }

    private void DestroyBullet()
    {
        ArrowsPool.Instance.ReturnArrow(this); // Вместо Destroy возвращаем в пул
    }
}
