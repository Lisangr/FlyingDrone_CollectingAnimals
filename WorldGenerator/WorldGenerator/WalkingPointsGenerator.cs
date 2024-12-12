using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static WorldGenerator;

public class WalkingPointsGenerator : MonoBehaviour
{
    public GameObject pointPrefab;
    public GameObject navMeshSurfaceObject;
    public float pointSpacing = 5f;  // ���������� ����� �������
    public GameObject walkingPointsParent;

    public static List<Vector3> walkingPoints = new List<Vector3>();
    public float navMeshSampleDistance = 1.0f;  // ������������ ��������� ��� �������� NavMesh
    public float pointCheckRadius = 5f; // ������ ��� �������� ����������� � ���������
    public float raycastHeight = 1f;  // ������, � ������� ����� ����������� Raycast

    public delegate void PontsCreated();
    public static event PontsCreated OnPontsCreated;
    private void OnEnable()
    {
        WorldGenerator.OnWorldCreated += WorldGenerator_OnWorldCreated;
    }

    private void WorldGenerator_OnWorldCreated()
    {
        // ����� ������� "Ground" � �������� � ��������� ��� � navMeshSurfaceObject
        SetNavMeshSurfaceObject();

        if (walkingPointsParent == null)
        {
            walkingPointsParent = new GameObject("WalkingPoints");
        }

        GenerateGridPoints();
    }

    // ����� ��� ������ ������� "GroundFlor" � ��������
    private void SetNavMeshSurfaceObject()
    {
        GameObject groundObject = GameObject.Find("GroundFlor");

        if (groundObject != null)
        {
            navMeshSurfaceObject = groundObject;
        }
        else
        {
            Debug.LogError("Ground not found in the hierarchy!");
        }
    }
    private void OnDisable()
    {
        WorldGenerator.OnWorldCreated -= WorldGenerator_OnWorldCreated;
    }
    
    void Start()
    {
        if (walkingPointsParent == null)
        {
            walkingPointsParent = new GameObject("WalkingPoints");
        }

        GenerateGridPoints();
    }
    
    // ����� ��������� �����
    public void GenerateGridPoints()
    {
        // ������� ������ �����
        ClearWalkingPoints();

        // �������� ������� ������� � NavMesh
        MeshRenderer navMeshRenderer = navMeshSurfaceObject.GetComponent<MeshRenderer>();

        Bounds bounds = navMeshRenderer.bounds;

        Vector3 minBounds = bounds.min;
        Vector3 maxBounds = bounds.max;

        // ��������� ����� ������ ������ ������� � NavMesh
        for (float x = minBounds.x; x <= maxBounds.x; x += pointSpacing)
        {
            for (float z = minBounds.z; z <= maxBounds.z; z += pointSpacing)
            {
                Vector3 pointPosition = new Vector3(x, navMeshSurfaceObject.transform.position.y, z);

                // ���������, ���� �� ���������� NavMesh � ���� �������, ����� �� ������������ � �������� � ��������� �� �����������
                if (IsNavMeshAtPosition(pointPosition) && !IsPointIntersectingWithEnvironment(pointPosition) && IsPointOnSurface(pointPosition))
                {
                    // ������ �����, ���� ��� ������� ���������
                    GameObject point = Instantiate(pointPrefab, pointPosition, Quaternion.identity, walkingPointsParent.transform);
                    point.name = $"WalkingPoint_{x}_{z}";
                    walkingPoints.Add(pointPosition); // ��������� ����� � ������
                }
            }
        }
        OnPontsCreated?.Invoke();

        //EnemyPool.Instance.SetPoolSizeBasedOnPoints(walkingPoints.Count);      
    }

    // ����� ��� ��������, ���� �� NavMesh � ������ �������
    bool IsNavMeshAtPosition(Vector3 position)
    {
        NavMeshHit hit;
        // ���������, ���� �� NavMesh � ��������� ������� � ������������ ����������
        if (NavMesh.SamplePosition(position, out hit, navMeshSampleDistance, NavMesh.AllAreas))
        {
            return true;  // NavMesh ������
        }
        return false;  // NavMesh �����������
    }

    // ����� ��� �������� ����������� ����� � ��������� � ����� "Env"
    bool IsPointIntersectingWithEnvironment(Vector3 pointPosition)
    {
        // ���������� OverlapSphere ��� �������� �������� � ����� "Env" � ��������� �����
        Collider[] hitColliders = Physics.OverlapSphere(pointPosition, pointCheckRadius);
        foreach (Collider collider in hitColliders)
        {
            if (collider.CompareTag("Env"))
            {
                return true;  // ����� ������������ � �������� � ����� "Env"
            }
        }
        return false;  // ��� �����������
    }

    // ����� ��� ��������, ��� ����� ��������� �� ����������� (�� ������ �������)
    bool IsPointOnSurface(Vector3 pointPosition)
    {
        RaycastHit hit;
        Vector3 rayOrigin = pointPosition + Vector3.up * raycastHeight;

        // ��������� Raycast ������ ����, ����� ���������, ��� ����� �� ������ �������
        if (Physics.Raycast(rayOrigin, Vector3.down, out hit, raycastHeight + 1.0f))
        {
            if (hit.point.y <= pointPosition.y + 0.1f && hit.point.y >= pointPosition.y - 0.1f)
            {
                return true;  // ����� �� �����������
            }
        }
        return false;  // ����� ������ �������
    }

    // ����� ��� ������� ����� ��������������� �����
    public void ClearWalkingPoints()
    {
        for (int i = walkingPointsParent.transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(walkingPointsParent.transform.GetChild(i).gameObject);
        }

        walkingPoints.Clear();
    }
}
