using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public Transform player; // �����
    public Vector3 offset = new Vector3(0, 40, -10); // �������� ������ ������������ ������
    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>().GetComponent<Transform>();
    }
    void LateUpdate()
    {
        // ���� ����� �� ������, �� ��������� ����������
        if (player == null) return;

        // ��������� ������� ������ � ������ ��������
        Vector3 desiredPosition = player.position + player.rotation * offset;
        transform.position = desiredPosition;

        // ������������� ������������� ���� ������� �� ��� X
        Quaternion fixedRotation = Quaternion.Euler(90, player.eulerAngles.y, 0);
        transform.rotation = fixedRotation;
    }
}
