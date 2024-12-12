using UnityEngine;
using UnityEngine.AI;  // ������������ ���� ��� NavMeshSurface

public class WorldGridGenerator : MonoBehaviour
{
    [System.Serializable]
    public class GroundObject
    {
        public GameObject prefab;  // ������ ����
        [Range(0f, 1f)]
        public float spawnChance;  // ���� ��������� (0 = �� ����������, 1 = 100% ����������)
    }

    public GroundObject groundObject;  // ������ ��� ����
    public int gridWidth = 100;        // ������ �����
    public int gridHeight = 100;       // ������ �����
    public float tileSize = 1.0f;      // ������ ������ �����

    private GameObject groundParent;   // ������������ ������ ��� ������ ����
    private GameObject[,] grid;        // ������ ��� �������� ������ ��������
    //private NavMeshSurface navMeshSurface; // NavMeshSurface ��� ���������

    [ContextMenu("Generate World")]
    public void GenerateWorld()
    {
        ClearWorld(); // ������� ���������� �������, ���� ��� ����
        CreateGroundParent();

        // ������� ������� ������ ����
        CreateGroundTile();

        // ������� ����������� � ������� GameObject
        CreatePseudoGrid();
    }

    private void CreateGroundTile()
    {
        if (groundObject != null && groundObject.prefab != null)
        {
            // ������� ��������� �� 0.5 �� �������� �����
            Vector3 groundPosition = new Vector3(gridWidth * tileSize / 2, 0, gridHeight * tileSize / 2);

            // ������� ������ ����
            GameObject groundTile = Instantiate(groundObject.prefab, groundPosition, Quaternion.identity, groundParent.transform);

            // �������� ������� ��������� ������� (bounding box)
            Renderer renderer = groundTile.GetComponent<Renderer>();
            if (renderer != null)
            {
                Vector3 prefabSize = renderer.bounds.size;

                // ������������ ������������ ��������������� �� ���� X � Z
                float scaleX = (gridWidth * tileSize) / prefabSize.x;
                float scaleZ = (gridHeight * tileSize) / prefabSize.z;

                // ��������� ������� � ������ ����, ��� �� ��� Y ������� ����� ���� ��������� (��������, ������� ����)
                groundTile.transform.localScale = new Vector3(scaleX, 1, scaleZ);
            }
            else
            {
                Debug.LogError("GroundObject prefab does not have a Renderer component.");
            }

            groundTile.name = $"GroundFlor";
            groundTile.tag = $"Ground";

            // ��������� ��������� NavMeshSurface � ������� ����
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
                // ������������ ������� ��� �������� �����
                Vector3 tilePosition = new Vector3(x * tileSize, 0, z * tileSize);

                // ������� ������ ������ ��� ������������� �����
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
        // ������� ����� ������ "Ground"
        groundParent = new GameObject("Ground");
        groundParent.transform.SetParent(transform);
    }

    public GameObject[,] GetGrid()
    {
        return grid;
    }
}














/*using UnityEngine;
using UnityEngine.AI;  // ������������ ���� ��� NavMeshSurface

public class WorldGridGenerator : MonoBehaviour
{
    [System.Serializable]
    public class GroundObject
    {
        public GameObject prefab;  // ������ ����
        [Range(0f, 1f)]
        public float spawnChance;  // ���� ��������� (0 = �� ����������, 1 = 100% ����������)
    }

    public GroundObject groundObject;  // ������ ��� ����
    public int gridWidth = 100;        // ������ �����
    public int gridHeight = 100;       // ������ �����
    public float tileSize = 1.0f;      // ������ ������ �����

    private GameObject groundParent;   // ������������ ������ ��� ������ ����
    private GameObject[,] grid;        // ������ ��� �������� ������ ��������
    private NavMeshSurface navMeshSurface; // NavMeshSurface ��� ���������


    [ContextMenu("Generate World")]
    public void GenerateWorld()
    {
        ClearWorld(); // ������� ���������� �������, ���� ��� ����
        CreateGroundParent();

        // ������� ������� ������ ����
        CreateGroundTile();

        // ������� ����������� � ������� GameObject
        CreatePseudoGrid();
    }

    private void CreateGroundTile()
    {
        if (groundObject != null && groundObject.prefab != null)
        {
            // ������� ��������� �� 0.5 �� �������� �����
            Vector3 groundPosition = new Vector3(gridWidth * tileSize / 2, 0, gridHeight * tileSize / 2);

            // ������� ������ ���� � ����������� ��������� �� ���� x � z
            GameObject groundTile = Instantiate(groundObject.prefab, groundPosition, Quaternion.identity, groundParent.transform);
            groundTile.transform.localScale = new Vector3(gridWidth * tileSize, 0.1f, gridHeight * tileSize);
            groundTile.name = $"GroundFlor";
            groundTile.tag = $"Ground";

            // ��������� ��������� NavMeshSurface � ������� ����
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
                // ������������ ������� ��� �������� �����
                Vector3 tilePosition = new Vector3(x * tileSize, 0, z * tileSize);

                // ������� ������ ������ ��� ������������� �����
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
        // ������� ����� ������ "Ground"
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
        public GameObject prefab;  // ������ ����
        [Range(0f, 1f)]
        public float spawnChance;  // ���� ��������� (0 = �� ����������, 1 = 100% ����������)
    }

    public GroundObject[] groundObjects;  // ������ ��������� ��� �������� ����
    public int gridWidth = 10;      // ������ �����
    public int gridHeight = 10;     // ������ �����
    public float tileSize = 1.0f;   // ������ ������ �����

    private GameObject groundParent;  // ������������ ������ ��� ������ ����
    private GameObject[,] grid;       // ������ ��� �������� ��������������� ��������

    // ��������� � ����������� ���� ����� ��� ��������� �����
    [ContextMenu("Generate World")]
    public void GenerateWorld()
    {
        ClearWorld(); // ������� ���������� �������, ���� ��� ����
        CreateGroundParent();

        grid = new GameObject[gridWidth, gridHeight];

        for (int x = 0; x < gridWidth; x++)
        {
            for (int z = 0; z < gridHeight; z++)
            {
                // ������������ ������� ��� �������� �����
                Vector3 tilePosition = new Vector3(x * tileSize, 0, z * tileSize);

                // �������� ��������� ������ ��� �������� �����
                GameObject selectedPrefab = GetRandomGroundPrefab();

                // ������� ������ �� ������������ ����������� � ��������� ��� � ������
                if (selectedPrefab != null)
                {
                    grid[x, z] = Instantiate(selectedPrefab, tilePosition, Quaternion.identity, groundParent.transform);
                    grid[x, z].name = $"Tile_{x}_{z}";  // ����������� ��� ��� ��������
                }
            }
        }
    }

    // ����� ��� ������� ������ ������ � ��������
    public void ClearWorld()
    {
        if (groundParent != null)
        {
            DestroyImmediate(groundParent);
        }
    }

    // ����� ��� �������� ������������� ������� ��� ����
    private void CreateGroundParent()
    {
        // ������� ����� ������ "Ground"
        groundParent = new GameObject("Ground");
        groundParent.transform.SetParent(transform);  // ������������� ��� �������� ��������� ����� �������
    }

    // ����� ��� ������ ���������� ������� ���� � ������ ������
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

        return null;  // ���� �� ���� ������ �� ������, ���������� null
    }

    // ����� ��� �������� �������� � ����� ��� ������ ��������
    public GameObject[,] GetGrid()
    {
        return grid;
    }
}
*/