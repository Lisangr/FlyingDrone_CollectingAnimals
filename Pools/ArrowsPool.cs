using System.Collections.Generic;
using UnityEngine;

public class ArrowsPool : MonoBehaviour
{
    public static ArrowsPool Instance;

    // ������ ������
    [SerializeField] private ArrowMoving arrowPrefab;
    [SerializeField] private int poolSize = 10;

    // ������� ��� ���� �����
    private Queue<ArrowMoving> arrowPool = new Queue<ArrowMoving>();

    // ������ ��� �������� ����� � �������� (Bullets)
    [SerializeField] private Transform bulletsParent;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            InitializePool();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            // ������� ���������� ������� ������ � ��������� ��
            ArrowMoving arrow = Instantiate(arrowPrefab, bulletsParent);  // ������� ������ ��� �������� ������ bulletsParent
            arrow.gameObject.SetActive(false);
            arrowPool.Enqueue(arrow);
        }
    }

    // �������� ������ �� ����
    public ArrowMoving GetArrow()
    {
        if (arrowPool.Count > 0)
        {
            ArrowMoving arrow = arrowPool.Dequeue();
            arrow.gameObject.SetActive(true);
            return arrow;
        }
        else
        {
            // ���� ����� � ���� �� �������, ������� �����
            ArrowMoving arrow = Instantiate(arrowPrefab, bulletsParent);  // ������� ������ ��� �������� ������ bulletsParent
            return arrow;
        }
    }

    // ���������� ������ � ���
    public void ReturnArrow(ArrowMoving arrow)
    {
        arrow.gameObject.SetActive(false);
        arrow.transform.SetParent(bulletsParent);  // ���������� ������ ��� �������� ������ bulletsParent
        arrowPool.Enqueue(arrow);
    }
}
