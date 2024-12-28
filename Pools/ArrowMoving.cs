using UnityEngine;

public class ArrowMoving : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 3f;
    public int arrowDamage = 100;

    private Vector3 targetPosition; // ������� ����
    private bool hasTarget = false; // ���� ��� �������� ����

    private Rigidbody rb;

    private void Awake()
    {
        // ���� ������������ Rigidbody ��� ���������� ���������
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        // ������ ��� �������� ������ � ���
        Invoke(nameof(ReturnToPool), lifetime);

        // ����� ��������� ����������
        speed = 10f;
        hasTarget = false;

        if (rb != null)
        {
            rb.velocity = Vector3.zero; // �������� ��������
            rb.angularVelocity = Vector3.zero; // �������� ������� ��������
        }
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
        // ���������� ������ � ��� � ���������� ���������
        ArrowsPool.Instance.ReturnArrow(this);
    }

    private void OnDisable()
    {
        // ������� ��������� ��� ����������
        CancelInvoke();

        if (rb != null)
        {
            rb.velocity = Vector3.zero; // �������� ��������
            rb.angularVelocity = Vector3.zero; // �������� ������� ��������
        }

        hasTarget = false;
        speed = 10f; // ����� ��������� ��������
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
        ArrowsPool.Instance.ReturnArrow(this); // ������ Destroy ���������� � ���
    }
}
