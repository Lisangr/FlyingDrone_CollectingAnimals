using UnityEngine;
using UnityEngine.AI;  // Пространство имен для NavMeshSurface

public class WorldGridGenerator : MonoBehaviour
{
    [System.Serializable]
    public class GroundObject
    {
        public GameObject prefab;  // Префаб пола
        [Range(0f, 1f)]
        public float spawnChance;  // Шанс появления (0 = не появляется, 1 = 100% появляется)
    }

    public GroundObject groundObject;  // Объект для пола
    public int gridWidth = 100;        // Ширина сетки
    public int gridHeight = 100;       // Высота сетки
    public float tileSize = 1.0f;      // Размер одного тайла

    private GameObject groundParent;   // Родительский объект для тайлов пола
    private GameObject[,] grid;        // Массив для хранения пустых объектов
    //private NavMeshSurface navMeshSurface; // NavMeshSurface для запекания

    [ContextMenu("Generate World")]
    public void GenerateWorld()
    {
        ClearWorld(); // Очищаем предыдущие объекты, если они есть
        CreateGroundParent();

        // Создаем большой объект пола
        CreateGroundTile();

        // Создаем псевдосетку с пустыми GameObject
        CreatePseudoGrid();
    }

    private void CreateGroundTile()
    {
        if (groundObject != null && groundObject.prefab != null)
        {
            // Позиция смещается на 0.5 от размеров сетки
            Vector3 groundPosition = new Vector3(gridWidth * tileSize / 2, 0, gridHeight * tileSize / 2);

            // Создаем объект пола
            GameObject groundTile = Instantiate(groundObject.prefab, groundPosition, Quaternion.identity, groundParent.transform);

            // Получаем размеры исходного объекта (bounding box)
            Renderer renderer = groundTile.GetComponent<Renderer>();
            if (renderer != null)
            {
                Vector3 prefabSize = renderer.bounds.size;

                // Рассчитываем коэффициенты масштабирования по осям X и Z
                float scaleX = (gridWidth * tileSize) / prefabSize.x;
                float scaleZ = (gridHeight * tileSize) / prefabSize.z;

                // Применяем масштаб с учетом того, что по оси Y масштаб может быть небольшим (например, толщина пола)
                groundTile.transform.localScale = new Vector3(scaleX, 1, scaleZ);
            }
            else
            {
                Debug.LogError("GroundObject prefab does not have a Renderer component.");
            }

            groundTile.name = $"GroundFlor";
            groundTile.tag = $"Ground";

            // Добавляем компонент NavMeshSurface к объекту пола
           // navMeshSurface = groundTile.AddComponent<NavMeshSurface>();
        }
        else
        {
            Debug.LogError("GroundObject prefab is not assigned.");
        }
    }

    private void CreatePseudoGrid()
    {
        grid = new GameObject[gridWidth, gridHeight];

        for (int x = 0; x < gridWidth; x++)
        {
            for (int z = 0; z < gridHeight; z++)
            {
                // Рассчитываем позицию для текущего тайла
                Vector3 tilePosition = new Vector3(x * tileSize, 0, z * tileSize);

                // Создаем пустой объект для представления тайла
                grid[x, z] = new GameObject($"Tile_{x}_{z}");
                grid[x, z].transform.position = tilePosition;
                grid[x, z].transform.SetParent(groundParent.transform);
            }
        }
    }

    public void ClearWorld()
    {
        if (groundParent != null)
        {
            DestroyImmediate(groundParent);
        }
    }

    private void CreateGroundParent()
    {
        // Создаем новый объект "Ground"
        groundParent = new GameObject("Ground");
        groundParent.transform.SetParent(transform);
    }

    public GameObject[,] GetGrid()
    {
        return grid;
    }
}














/*using UnityEngine;
using UnityEngine.AI;  // Пространство имен для NavMeshSurface

public class WorldGridGenerator : MonoBehaviour
{
    [System.Serializable]
    public class GroundObject
    {
        public GameObject prefab;  // Префаб пола
        [Range(0f, 1f)]
        public float spawnChance;  // Шанс появления (0 = не появляется, 1 = 100% появляется)
    }

    public GroundObject groundObject;  // Объект для пола
    public int gridWidth = 100;        // Ширина сетки
    public int gridHeight = 100;       // Высота сетки
    public float tileSize = 1.0f;      // Размер одного тайла

    private GameObject groundParent;   // Родительский объект для тайлов пола
    private GameObject[,] grid;        // Массив для хранения пустых объектов
    private NavMeshSurface navMeshSurface; // NavMeshSurface для запекания


    [ContextMenu("Generate World")]
    public void GenerateWorld()
    {
        ClearWorld(); // Очищаем предыдущие объекты, если они есть
        CreateGroundParent();

        // Создаем большой объект пола
        CreateGroundTile();

        // Создаем псевдосетку с пустыми GameObject
        CreatePseudoGrid();
    }

    private void CreateGroundTile()
    {
        if (groundObject != null && groundObject.prefab != null)
        {
            // Позиция смещается на 0.5 от размеров сетки
            Vector3 groundPosition = new Vector3(gridWidth * tileSize / 2, 0, gridHeight * tileSize / 2);

            // Создаем объект пола с увеличенным масштабом по осям x и z
            GameObject groundTile = Instantiate(groundObject.prefab, groundPosition, Quaternion.identity, groundParent.transform);
            groundTile.transform.localScale = new Vector3(gridWidth * tileSize, 0.1f, gridHeight * tileSize);
            groundTile.name = $"GroundFlor";
            groundTile.tag = $"Ground";

            // Добавляем компонент NavMeshSurface к объекту пола
            navMeshSurface = groundTile.AddComponent<NavMeshSurface>();
        }
        else
        {
            Debug.LogError("GroundObject prefab is not assigned.");
        }
    }

    private void CreatePseudoGrid()
    {
        grid = new GameObject[gridWidth, gridHeight];

        for (int x = 0; x < gridWidth; x++)
        {
            for (int z = 0; z < gridHeight; z++)
            {
                // Рассчитываем позицию для текущего тайла
                Vector3 tilePosition = new Vector3(x * tileSize, 0, z * tileSize);

                // Создаем пустой объект для представления тайла
                grid[x, z] = new GameObject($"Tile_{x}_{z}");
                grid[x, z].transform.position = tilePosition;
                grid[x, z].transform.SetParent(groundParent.transform);
            }
        }
    }

    public void ClearWorld()
    {
        if (groundParent != null)
        {
            DestroyImmediate(groundParent);
        }
    }

    private void CreateGroundParent()
    {
        // Создаем новый объект "Ground"
        groundParent = new GameObject("Ground");
        groundParent.transform.SetParent(transform);
    }

    public GameObject[,] GetGrid()
    {
        return grid;
    }
}
*/






/*
using UnityEngine;

public class WorldGridGenerator : MonoBehaviour
{
    [System.Serializable]
    public class GroundObject
    {
        public GameObject prefab;  // Префаб пола
        [Range(0f, 1f)]
        public float spawnChance;  // Шанс появления (0 = не появляется, 1 = 100% появляется)
    }

    public GroundObject[] groundObjects;  // Массив вариантов для префабов пола
    public int gridWidth = 10;      // Ширина сетки
    public int gridHeight = 10;     // Высота сетки
    public float tileSize = 1.0f;   // Размер одного тайла

    private GameObject groundParent;  // Родительский объект для тайлов пола
    private GameObject[,] grid;       // Массив для хранения сгенерированных объектов

    // Добавляем в контекстное меню пункт для генерации сетки
    [ContextMenu("Generate World")]
    public void GenerateWorld()
    {
        ClearWorld(); // Очищаем предыдущие объекты, если они есть
        CreateGroundParent();

        grid = new GameObject[gridWidth, gridHeight];

        for (int x = 0; x < gridWidth; x++)
        {
            for (int z = 0; z < gridHeight; z++)
            {
                // Рассчитываем позицию для текущего тайла
                Vector3 tilePosition = new Vector3(x * tileSize, 0, z * tileSize);

                // Получаем случайный префаб для текущего тайла
                GameObject selectedPrefab = GetRandomGroundPrefab();

                // Создаем префаб на рассчитанных координатах и сохраняем его в массив
                if (selectedPrefab != null)
                {
                    grid[x, z] = Instantiate(selectedPrefab, tilePosition, Quaternion.identity, groundParent.transform);
                    grid[x, z].name = $"Tile_{x}_{z}";  // Присваиваем имя для удобства
                }
            }
        }
    }

    // Метод для очистки старых тайлов и объектов
    public void ClearWorld()
    {
        if (groundParent != null)
        {
            DestroyImmediate(groundParent);
        }
    }

    // Метод для создания родительского объекта для пола
    private void CreateGroundParent()
    {
        // Создаем новый объект "Ground"
        groundParent = new GameObject("Ground");
        groundParent.transform.SetParent(transform);  // Устанавливаем его дочерним элементом этого объекта
    }

    // Метод для выбора случайного префаба пола с учетом шансов
    private GameObject GetRandomGroundPrefab()
    {
        float randomValue = Random.value;

        foreach (var groundObject in groundObjects)
        {
            if (randomValue <= groundObject.spawnChance)
            {
                return groundObject.prefab;
            }
        }

        return null;  // Если ни один префаб не выбран, возвращаем null
    }

    // Метод для передачи размеров и сетки для других скриптов
    public GameObject[,] GetGrid()
    {
        return grid;
    }
}
*/