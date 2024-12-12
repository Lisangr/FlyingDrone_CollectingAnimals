using UnityEngine;
using UnityEditor;

public class DeleteEverySecondChild : MonoBehaviour
{
    [ContextMenu("Delete Every Second Child")]
    private void DeleteEverySecondChildObjects()
    {
        // ��������� ���������� �����
        int childCount = transform.childCount;

        // ���������� �� ����� � �����, ����� �������� �� �������� ��������������
        for (int i = childCount - 1; i >= 0; i--)
        {
            // ������� ������ ������ ������
            if (i % 2 == 1) // ������� ������� � ��������� ���������
            {
                DestroyImmediate(transform.GetChild(i).gameObject);
            }
        }
    }
}
