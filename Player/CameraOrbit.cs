using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    public Transform target; // ������, ������ �������� ��������� ������
    public float angularSpeed = 30f; // ������� �������� (������� � �������)
    public Vector3 axis = Vector3.up; // ��� �������� (�� ��������� �����)

    void Update()
    {
        if (target != null)
        {
            // ������������ ���� �������� �� ������� ����
            float angle = angularSpeed * Time.deltaTime;

            // ������� ������ ������ ����
            transform.RotateAround(target.position, axis, angle);
        }
    }
}
