using UnityEngine;

public class ArrowMoving : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 3f;
    public int arrowDamage = 100;

    private Vector3 targetPosition; // Позиция цели
    private bool hasTarget = false; // Флаг для проверки цели

    private void OnEnable()
    {
        // Таймер для возврата стрелы в пул
        Invoke(nameof(ReturnToPool), lifetime);
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
        ArrowsPool.Instance.ReturnArrow(this);
    }

    private void OnDisable()
    {
        CancelInvoke();
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
        Destroy(this.gameObject);
    }
}
