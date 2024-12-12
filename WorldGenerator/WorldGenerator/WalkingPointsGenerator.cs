using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static WorldGenerator;

public class WalkingPointsGenerator : MonoBehaviour
{
    public GameObject pointPrefab;
    public GameObject navMeshSurfaceObject;
    public float pointSpacing = 5f;  // Расстояние между точками
    public GameObject walkingPointsParent;

    public static List<Vector3> walkingPoints = new List<Vector3>();
    public float navMeshSampleDistance = 1.0f;  // Максимальная дистанция для проверки NavMesh
    public float pointCheckRadius = 5f; // Радиус для проверки пересечения с объектами
    public float raycastHeight = 1f;  // Высота, с которой будет запускаться Raycast

    public delegate void PontsCreated();
    public static event PontsCreated OnPontsCreated;
    private void OnEnable()
    {
        WorldGenerator.OnWorldCreated += WorldGenerator_OnWorldCreated;
    }

    private void WorldGenerator_OnWorldCreated()
    {
        // Поиск объекта "Ground" в иерархии и установка его в navMeshSurfaceObject
        SetNavMeshSurfaceObject();

        if (walkingPointsParent == null)
        {
            walkingPointsParent = new GameObject("WalkingPoints");
        }

        GenerateGridPoints();
    }

    // Метод для поиска объекта "GroundFlor" в иерархии
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
    
    // Метод генерации точек
    public void GenerateGridPoints()
    {
        // Очищаем старые точки
        ClearWalkingPoints();

        // Получаем размеры объекта с NavMesh
        MeshRenderer navMeshRenderer = navMeshSurfaceObject.GetComponent<MeshRenderer>();

        Bounds bounds = navMeshRenderer.bounds;

        Vector3 minBounds = bounds.min;
        Vector3 maxBounds = bounds.max;

        // Генерация точек внутри границ объекта с NavMesh
        for (float x = minBounds.x; x <= maxBounds.x; x += pointSpacing)
        {
            for (float z = minBounds.z; z <= maxBounds.z; z += pointSpacing)
            {
                Vector3 pointPosition = new Vector3(x, navMeshSurfaceObject.transform.position.y, z);

                // Проверяем, есть ли запечённый NavMesh в этой позиции, точка не пересекается с объектом и находится на поверхности
                if (IsNavMeshAtPosition(pointPosition) && !IsPointIntersectingWithEnvironment(pointPosition) && IsPointOnSurface(pointPosition))
                {
                    // Создаём точку, если все условия выполнены
                    GameObject point = Instantiate(pointPrefab, pointPosition, Quaternion.identity, walkingPointsParent.transform);
                    point.name = $"WalkingPoint_{x}_{z}";
                    walkingPoints.Add(pointPosition); // Добавляем точку в список
                }
            }
        }
        OnPontsCreated?.Invoke();

        //EnemyPool.Instance.SetPoolSizeBasedOnPoints(walkingPoints.Count);      
    }

    // Метод для проверки, есть ли NavMesh в данной позиции
    bool IsNavMeshAtPosition(Vector3 position)
    {
        NavMeshHit hit;
        // Проверяем, есть ли NavMesh в указанной позиции с максимальной дистанцией
        if (NavMesh.SamplePosition(position, out hit, navMeshSampleDistance, NavMesh.AllAreas))
        {
            return true;  // NavMesh найден
        }
        return false;  // NavMesh отсутствует
    }

    // Метод для проверки пересечения точки с объектами с тегом "Env"
    bool IsPointIntersectingWithEnvironment(Vector3 pointPosition)
    {
        // Используем OverlapSphere для проверки объектов с тегом "Env" в указанной точке
        Collider[] hitColliders = Physics.OverlapSphere(pointPosition, pointCheckRadius);
        foreach (Collider collider in hitColliders)
        {
            if (collider.CompareTag("Env"))
            {
                return true;  // Точка пересекается с объектом с тегом "Env"
            }
        }
        return false;  // Нет пересечений
    }

    // Метод для проверки, что точка находится на поверхности (не внутри объекта)
    bool IsPointOnSurface(Vector3 pointPosition)
    {
        RaycastHit hit;
        Vector3 rayOrigin = pointPosition + Vector3.up * raycastHeight;

        // Запускаем Raycast сверху вниз, чтобы убедиться, что точка не внутри объекта
        if (Physics.Raycast(rayOrigin, Vector3.down, out hit, raycastHeight + 1.0f))
        {
            if (hit.point.y <= pointPosition.y + 0.1f && hit.point.y >= pointPosition.y - 0.1f)
            {
                return true;  // Точка на поверхности
            }
        }
        return false;  // Точка внутри объекта
    }

    // Метод для очистки ранее сгенерированных точек
    public void ClearWalkingPoints()
    {
        for (int i = walkingPointsParent.transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(walkingPointsParent.transform.GetChild(i).gameObject);
        }

        walkingPoints.Clear();
    }
}
