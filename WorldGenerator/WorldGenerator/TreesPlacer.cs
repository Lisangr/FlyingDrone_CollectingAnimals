using UnityEngine;

[ExecuteInEditMode]
public class TreesPlacer : MonoBehaviour
{
    [System.Serializable]
    public class TreeObject
    {
        public GameObject prefab;  // Префаб дерева
        [Range(0f, 1f)]
        public float spawnChance;  // Шанс появления (0 = не появляется, 1 = 100% появляется)

        [Range(0.5f, 2f)]
        public float minScale = 0.8f;  // Минимальный масштаб дерева

        [Range(0.5f, 2f)]
        public float maxScale = 1.2f;  // Максимальный масштаб дерева
    }

    public TreeObject[] treeObjects;  // Массив префабов деревьев
    private WorldGridGenerator worldGenerator;  // Ссылка на WorldGenerator
    private GameObject[,] grid;  // Сетка, сгенерированная WorldGenerator
    private GameObject treesParent;  // Родительский объект для деревьев

    [Range(0f, 1f)]
    public float treeDensity = 0.3f;  // Плотность деревьев

    [Range(0f, 1f)]
    public float skipChance = 0.5f;  // Вероятность пропуска тайла

    [ContextMenu("Place Trees")]
    public void PlaceTrees()
    {
        worldGenerator = GetComponent<WorldGridGenerator>();

        if (worldGenerator == null)
        {
            Debug.LogError("WorldGenerator script not found on the same object.");
            return;
        }

        if (treeObjects == null || treeObjects.Length == 0)
        {
            Debug.LogError("Tree objects array is empty.");
            return;
        }

        grid = worldGenerator.GetGrid();

        if (grid == null)
        {
            Debug.LogError("Grid is not generated yet.");
            return;
        }

        RemoveOldTrees();
        CreateTreesParent();

        int gridWidth = worldGenerator.gridWidth;
        int gridHeight = worldGenerator.gridHeight;
        float tileSize = worldGenerator.tileSize;

        for (int x = 0; x < gridWidth; x++)
        {
            for (int z = 0; z < gridHeight; z++)
            {
                if (Random.value <= treeDensity && Random.value > skipChance)
                {
                    foreach (var treeObject in treeObjects)
                    {
                        if (Random.value <= treeObject.spawnChance)
                        {
                            PlaceTreeAt(grid[x, z].transform.position, treeObject);
                        }
                    }
                }
            }
        }
    }

    private void CreateTreesParent()
    {
        treesParent = GameObject.Find("Trees");

        if (treesParent == null)
        {
            treesParent = new GameObject("Trees");
            treesParent.transform.SetParent(transform);
        }
    }

    private void PlaceTreeAt(Vector3 position, TreeObject treeObject)
    {
        if (treeObject.prefab != null)
        {
            Renderer renderer = treeObject.prefab.GetComponent<Renderer>();
            Vector3 prefabSize = renderer != null ? renderer.bounds.size : new Vector3(1, 1, 1);

            Vector3 adjustedPosition = position + new Vector3(prefabSize.x / 2f, 0, prefabSize.z / 2f);

            // Случайное вращение по оси Y
            Quaternion randomRotation = Quaternion.Euler(0, Random.Range(0f, 360f), 0);

            // Случайный масштаб дерева
            float randomScale = Random.Range(treeObject.minScale, treeObject.maxScale);
            Vector3 scaleVector = new Vector3(randomScale, randomScale, randomScale);

            GameObject treeInstance = Instantiate(treeObject.prefab, adjustedPosition, randomRotation);
            treeInstance.transform.localScale = scaleVector;  // Применяем случайный масштаб
            treeInstance.transform.SetParent(treesParent.transform);
        }
    }

    public void RemoveOldTrees()
    {
        treesParent = GameObject.Find("Trees");

        if (treesParent != null)
        {
            DestroyImmediate(treesParent);
        }
    }
}
