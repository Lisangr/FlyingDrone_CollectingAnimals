using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[ExecuteInEditMode]
public class BorderPlacer : MonoBehaviour
{
    [System.Serializable]
    public class BorderObject
    {
        public GameObject prefab;  // ������ �������
        [Range(0f, 1f)]
        public float spawnChance;  // ���� ��������� (0 = �� ����������, 1 = 100% ����������)
    }

    public BorderObject[] borderObjects;  // ������ �������� ��� ������
    private WorldGridGenerator worldGenerator;  // ������ �� WorldGenerator
    private GameObject[,] grid;  // �����, ��������������� WorldGenerator
    private GameObject bordersParent;  // ������������ ������ ��� ������

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

        // �������� ����� �� WorldGenerator
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

        // ���������� �������� �� ����� �����
        PlaceBordersOnEdge(true, gridWidth, 0, tileSize); // ������� �������
        PlaceBordersOnEdge(true, gridWidth, gridHeight - 1, tileSize); // ������ �������
        PlaceBordersOnEdge(false, gridHeight, 0, tileSize); // ����� �������
        PlaceBordersOnEdge(false, gridHeight, gridWidth - 1, tileSize); // ������ �������
    }

    // ����� ��� �������� ������������� ������� ��� ������
    private void CreateBordersParent()
    {
        // ������� ������ ������, ���� �� ����������
        if (bordersParent != null)
        {
            DestroyImmediate(bordersParent);
        }

        // ������� ����� ������ "Borders"
        bordersParent = new GameObject("Borders");
        bordersParent.transform.SetParent(transform);  // ������������� ��� �������� ��������� ����� �������
    }

    // ����� ��� ���������� �������� ��������������� ����� ����� �� ����
    private void PlaceBordersOnEdge(bool isXAxis, int gridSize, int fixedCoord, float tileSize)
    {
        float currentPosition = 0f;

        for (int i = 0; i < gridSize;)
        {
            GameObject prefab = GetRandomBorderPrefab();
            if (prefab != null)
            {
                // �������� ������ �������
                Renderer renderer = prefab.GetComponent<Renderer>();
                if (renderer != null)
                {
                    Vector3 prefabSize = renderer.bounds.size;

                    // ��������� ������� � ����������� �� ���
                    Vector3 position;
                    if (isXAxis) // ������� ��� ������ �������
                    {
                        position = new Vector3(currentPosition, 0, fixedCoord * tileSize);
                        currentPosition += prefabSize.x;  // ��������� ������� ������� �� X
                    }
                    else // ����� ��� ������ �������
                    {
                        position = new Vector3(fixedCoord * tileSize, 0, currentPosition);
                        currentPosition += prefabSize.z;  // ��������� ������� ������� �� Z
                    }

                    // ������������ ������ �� ������������ �����������
                    Instantiate(prefab, position, Quaternion.identity, bordersParent.transform);

                    // ����������� ������ ����� �� ���������� ������, ������� �������� ������
                    i += Mathf.CeilToInt(isXAxis ? prefabSize.x / tileSize : prefabSize.z / tileSize);
                }
            }
        }
    }

    // ����� ��� ��������� ���������� ������� � ������ ������
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

        return null; // ���� �� ���� ������ �� ������, ���������� null
    }

    // ����� ��� �������� ������ ������
    public void RemoveOldBorders()
    {
        if (bordersParent != null)
        {
            DestroyImmediate(bordersParent);
        }
    }
}
