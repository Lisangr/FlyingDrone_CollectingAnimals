using UnityEngine;

[ExecuteInEditMode]
public class GrassPlacer : MonoBehaviour
{
    [System.Serializable]
    public class GrassObject
    {
        public GameObject prefab;  // Префаб травы
        [Range(0f, 1f)]
        public float spawnChance;  // Шанс появления (0 = не появляется, 1 = 100% появляется)
    }

    public GrassObject[] grassObjects;  // Массив префабов травы
    private WorldGridGenerator worldGenerator;  // Ссылка на WorldGenerator
    private GameObject[,] grid;  // Сетка, сгенерированная WorldGenerator
    private GameObject grassParent;  // Родительский объект для травы

    [Range(0f, 1f)]
    public float grassDensity = 0.3f;  // Плотность травы

    [Range(0f, 1f)]
    public float skipChance = 0.5f;  // Вероятность пропуска тайла

    [ContextMenu("Place Grass")]
    public void PlaceGrass()
    {
        worldGenerator = GetComponent<WorldGridGenerator>();

        if (worldGenerator == null)
        {
            Debug.LogError("WorldGenerator script not found on the same object.");
            return;
        }

        if (grassObjects == null || grassObjects.Length == 0)
        {
            Debug.LogError("Grass objects array is empty.");
            return;
        }

        grid = worldGenerator.GetGrid();

        if (grid == null)
        {
            Debug.LogError("Grid is not generated yet.");
            return;
        }

        RemoveOldGrass();
        CreateGrassParent();

        int gridWidth = worldGenerator.gridWidth;
        int gridHeight = worldGenerator.gridHeight;
        float tileSize = worldGenerator.tileSize;

        for (int x = 0; x < gridWidth; x++)
        {
            for (int z = 0; z < gridHeight; z++)
            {
                if (Random.value <= grassDensity && Random.value > skipChance)
                {
                    PlaceRandomGrassAt(grid[x, z].transform.position);
                }
            }
        }
    }

    private void CreateGrassParent()
    {
        grassParent = GameObject.Find("Grass");

        if (grassParent == null)
        {
            grassParent = new GameObject("Grass");
            grassParent.transform.SetParent(transform);
        }
    }

    private void PlaceRandomGrassAt(Vector3 position)
    {
        GameObject selectedPrefab = GetRandomGrassPrefab();
        if (selectedPrefab != null)
        {
            Renderer renderer = selectedPrefab.GetComponent<Renderer>();
            Vector3 prefabSize = renderer != null ? renderer.bounds.size : new Vector3(1, 1, 1);

            Vector3 adjustedPosition = position + new Vector3(prefabSize.x / 2f, 0, prefabSize.z / 2f);

            // Случайное вращение по оси Y
            Quaternion randomRotation = Quaternion.Euler(0, Random.Range(0f, 360f), 0);

            GameObject grassInstance = Instantiate(selectedPrefab, adjustedPosition, randomRotation);
            grassInstance.transform.SetParent(grassParent.transform);
        }
    }

    private GameObject GetRandomGrassPrefab()
    {
        float totalChance = 0f;
        foreach (var grassObject in grassObjects)
        {
            totalChance += grassObject.spawnChance;
        }

        float randomValue = Random.value * totalChance;
        float cumulativeChance = 0f;

        foreach (var grassObject in grassObjects)
        {
            cumulativeChance += grassObject.spawnChance;
            if (randomValue <= cumulativeChance)
            {
                return grassObject.prefab;
            }
        }

        return null;
    }

    public void RemoveOldGrass()
    {
        grassParent = GameObject.Find("Grass");

        if (grassParent != null)
        {
            DestroyImmediate(grassParent);
        }
    }
}
