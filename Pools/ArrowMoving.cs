using UnityEngine;

public class ArrowMoving : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 3f;
    public int arrowDamage = 100;

    private Vector3 targetPosition; // ������� ����
    private bool hasTarget = false; // ���� ��� �������� ����

    private void OnEnable()
    {
        // ������ ��� �������� ������ � ���
        Invoke(nameof(ReturnToPool), lifetime);
    }

    private void Update()
    {
        // ���� ���� ������, ��������� � ���
        if (hasTarget)
        {
            Vector3 direction = (targetPosition - transform.position).normalized;
            transform.Translate(direction * speed * Time.deltaTime, Space.World);

            // ������������ ������ � ����������� ����
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
