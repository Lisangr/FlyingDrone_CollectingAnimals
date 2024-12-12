using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[ExecuteInEditMode]
public class BorderPlacer : MonoBehaviour
{
    [System.Serializable]
    public class BorderObject
    {
        public GameObject prefab;  // Префаб объекта
        [Range(0f, 1f)]
        public float spawnChance;  // Шанс появления (0 = не появляется, 1 = 100% появляется)
    }

    public BorderObject[] borderObjects;  // Массив объектов для границ
    private WorldGridGenerator worldGenerator;  // Ссылка на WorldGenerator
    private GameObject[,] grid;  // Сетка, сгенерированная WorldGenerator
    private GameObject bordersParent;  // Родительский объект для границ

    [ContextMenu("Place Borders")]
    public void PlaceBorders()
    {
        worldGenerator = GetComponent<WorldGridGenerator>();

        if (worldGenerator == null)
        {
            Debug.LogError("WorldGenerator script not found on the same object.");
            return;
        }

        if (borderObjects == null || borderObjects.Length == 0)
        {
            Debug.LogError("Border objects array is empty.");
            return;
        }

        // Получаем сетку из WorldGenerator
        grid = worldGenerator.GetGrid();

        if (grid == null)
        {
            Debug.LogError("Grid is not generated yet.");
            return;
        }

        RemoveOldBorders();
        CreateBordersParent();

        int gridWidth = worldGenerator.gridWidth;
        int gridHeight = worldGenerator.gridHeight;
        float tileSize = worldGenerator.tileSize;

        // Размещение объектов по краям карты
        PlaceBordersOnEdge(true, gridWidth, 0, tileSize); // Верхняя граница
        PlaceBordersOnEdge(true, gridWidth, gridHeight - 1, tileSize); // Нижняя граница
        PlaceBordersOnEdge(false, gridHeight, 0, tileSize); // Левая граница
        PlaceBordersOnEdge(false, gridHeight, gridWidth - 1, tileSize); // Правая граница
    }

    // Метод для создания родительского объекта для границ
    private void CreateBordersParent()
    {
        // Удаляем старый объект, если он существует
        if (bordersParent != null)
        {
            DestroyImmediate(bordersParent);
        }

        // Создаем новый объект "Borders"
        bordersParent = new GameObject("Borders");
        bordersParent.transform.SetParent(transform);  // Устанавливаем его дочерним элементом этого объекта
    }

    // Метод для размещения объектов последовательно вдоль одной из осей
    private void PlaceBordersOnEdge(bool isXAxis, int gridSize, int fixedCoord, float tileSize)
    {
        float currentPosition = 0f;

        for (int i = 0; i < gridSize;)
        {
            GameObject prefab = GetRandomBorderPrefab();
            if (prefab != null)
            {
                // Получаем размер префаба
                Renderer renderer = prefab.GetComponent<Renderer>();
                if (renderer != null)
                {
                    Vector3 prefabSize = renderer.bounds.size;

                    // Вычисляем позицию в зависимости от оси
                    Vector3 position;
                    if (isXAxis) // Верхняя или нижняя граница
                    {
                        position = new Vector3(currentPosition, 0, fixedCoord * tileSize);
                        currentPosition += prefabSize.x;  // Обновляем текущую позицию по X
                    }
                    else // Левая или правая граница
                    {
                        position = new Vector3(fixedCoord * tileSize, 0, currentPosition);
                        currentPosition += prefabSize.z;  // Обновляем текущую позицию по Z
                    }

                    // Инстанцируем префаб на рассчитанных координатах
                    Instantiate(prefab, position, Quaternion.identity, bordersParent.transform);

                    // Увеличиваем индекс сетки на количество тайлов, которые занимает префаб
                    i += Mathf.CeilToInt(isXAxis ? prefabSize.x / tileSize : prefabSize.z / tileSize);
                }
            }
        }
    }

    // Метод для получения случайного префаба с учетом шансов
    private GameObject GetRandomBorderPrefab()
    {
        float randomValue = Random.value;

        foreach (var borderObject in borderObjects)
        {
            if (randomValue <= borderObject.spawnChance)
            {
                return borderObject.prefab;
            }
        }

        return null; // Если ни один префаб не выбран, возвращаем null
    }

    // Метод для удаления старых границ
    public void RemoveOldBorders()
    {
        if (bordersParent != null)
        {
            DestroyImmediate(bordersParent);
        }
    }
}
