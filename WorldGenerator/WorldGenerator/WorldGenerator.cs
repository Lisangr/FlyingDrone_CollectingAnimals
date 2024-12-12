using UnityEngine;
using UnityEngine.AI;

public class WorldGenerator : MonoBehaviour
{
    // ������ �� ������� ��� ��������� (����� ������������� ���������)
    private WorldGridGenerator gridGenerator;
    private BorderPlacer borderPlacer;
    private GrassPlacer grassPlacer;
    private TreesPlacer treesPlacer;
   // private NavMeshSurface navMeshSurface;  // ������ �� NavMeshSurface ��� ��������� �������

    // ����� ��� �������� ��������� ��������� ���������
    public bool generateGrid = true;
    public bool placeBorders = true;
    public bool placeGrass = true;
    public bool placeTrees = true;
    public bool bakeNavMesh = true;  // ���� ��� �������� ��������� �������
    public bool autoGenerateOnSceneLoad = true;  // ���� ��� �������������� ��������� ��� �������� �����

    public delegate void WorldCreated();
    public static event WorldCreated OnWorldCreated;
    // ����� ���������� ��� �������� �����
    private void Start()
    {
        // ���� �������� �������������, ��������� ��������� ��� �������� �����
        if (autoGenerateOnSceneLoad)
        {
            GenerateFullWorld();
        }
    }

    // ����� ��� ��������� ����� ����
    public void GenerateFullWorld()
    {
        // ��������, ��� ���������� ���������
        EnsureComponents();

        // ��������� ����
        if (generateGrid && gridGenerator != null)
        {
            gridGenerator.GenerateWorld();
        }

        if (placeBorders && borderPlacer != null)
        {
            borderPlacer.PlaceBorders();
        }

        if (placeGrass && grassPlacer != null)
        {
            grassPlacer.PlaceGrass();
        }

        if (placeTrees && treesPlacer != null)
        {
            treesPlacer.PlaceTrees();
        }

        /*navMeshSurface = GameObject.Find("GroundFlor").GetComponent<NavMeshSurface>();
        Debug.Log("� ���� ��������� � ���� ��� ������");

        // �������� ������, ���� ���� ����������
        if (bakeNavMesh && navMeshSurface != null)
        {
            navMeshSurface.BuildNavMesh();
            Debug.Log("NavMesh has been baked!");
            OnWorldCreated?.Invoke();
        }
        else
        {
            Debug.Log("� �� ����� ������-��");
        }*/
    }

    // �������� ���� ������ �������� ����� ����� ����������
    public void ClearFullWorld()
    {
        // ��������, ��� ���������� ���������
        EnsureComponents();

        // ������� ����
        if (gridGenerator != null)
        {
            gridGenerator.ClearWorld();
        }

        if (borderPlacer != null)
        {
            borderPlacer.RemoveOldBorders();
        }

        if (grassPlacer != null)
        {
            grassPlacer.RemoveOldGrass();
        }

        if (treesPlacer != null)
        {
            treesPlacer.RemoveOldTrees();
        }
    }

    // ����� ��� �������� � ���������� �����������
    private void EnsureComponents()
    {
        // ��������� WorldGridGenerator, ���� �� �����������
        if (gridGenerator == null)
        {
            gridGenerator = gameObject.GetComponent<WorldGridGenerator>();
            if (gridGenerator == null)
            {
                gridGenerator = gameObject.AddComponent<WorldGridGenerator>();
            }
        }

        // ��������� BorderPlacer, ���� �� �����������
        if (borderPlacer == null)
        {
            borderPlacer = gameObject.GetComponent<BorderPlacer>();
            if (borderPlacer == null)
            {
                borderPlacer = gameObject.AddComponent<BorderPlacer>();
            }
        }

        // ��������� GrassPlacer, ���� �� �����������
        if (grassPlacer == null)
        {
            grassPlacer = gameObject.GetComponent<GrassPlacer>();
            if (grassPlacer == null)
            {
                grassPlacer = gameObject.AddComponent<GrassPlacer>();
            }
        }

        // ��������� TreesPlacer, ���� �� �����������
        if (treesPlacer == null)
        {
            treesPlacer = gameObject.GetComponent<TreesPlacer>();
            if (treesPlacer == null)
            {
                treesPlacer = gameObject.AddComponent<TreesPlacer>();
            }
        }
        /*
        // ��������� NavMeshSurface, ���� �� �����������
        if (navMeshSurface == null)
        {
            navMeshSurface = gameObject.GetComponent<NavMeshSurface>();
            if (navMeshSurface == null)
            {
                navMeshSurface = gameObject.AddComponent<NavMeshSurface>();
            }
        }*/
    }

    // ����� ��� ��������� �������� �� ��������� � ������������� ������
    private void Reset()
    {
        // ������������� ������� � ��������� ����������, ���� ��� �����������
        EnsureComponents();
    }
}





/*
using UnityEngine;
using UnityEngine.AI;

public class WorldGenerator : MonoBehaviour
{
    // ������ �� ������� ��� ��������� (����� ������������� ���������)
    private WorldGridGenerator gridGenerator;
    private BorderPlacer borderPlacer;
    private GrassPlacer grassPlacer;
    private TreesPlacer treesPlacer;
    private NavMeshSurface navMeshSurface;  // ������ �� NavMeshSurface ��� ��������� �������

    // ����� ��� �������� ��������� ��������� ���������
    public bool generateGrid = true;
    public bool placeBorders = true;
    public bool placeGrass = true;
    public bool placeTrees = true;
    public bool bakeNavMesh = true;  // ���� ��� �������� ��������� �������

    // ����� ��� ��������� ����� ����
    public void GenerateFullWorld()
    {
        // ��������, ��� ���������� ���������
        EnsureComponents();

        // ��������� ����
        if (generateGrid && gridGenerator != null)
        {
            gridGenerator.GenerateWorld();
        }

        if (placeBorders && borderPlacer != null)
        {
            borderPlacer.PlaceBorders();
        }

        if (placeGrass && grassPlacer != null)
        {
            grassPlacer.PlaceGrass();
        }

        if (placeTrees && treesPlacer != null)
        {
            treesPlacer.PlaceTrees();
        }

        // �������� ������, ���� ���� ����������
        if (bakeNavMesh && navMeshSurface != null)
        {
            navMeshSurface.BuildNavMesh();
            Debug.Log("NavMesh has been baked!");
        }
    }

    // �������� ���� ������ �������� ����� ����� ����������
    public void ClearFullWorld()
    {
        // ��������, ��� ���������� ���������
        EnsureComponents();

        // ������� ����
        if (gridGenerator != null)
        {
            gridGenerator.ClearWorld();
        }

        if (borderPlacer != null)
        {
            borderPlacer.RemoveOldBorders();
        }

        if (grassPlacer != null)
        {
            grassPlacer.RemoveOldGrass();
        }

        if (treesPlacer != null)
        {
            treesPlacer.RemoveOldTrees();
        }
    }

    // ����� ��� �������� � ���������� �����������
    private void EnsureComponents()
    {
        // ��������� WorldGridGenerator, ���� �� �����������
        if (gridGenerator == null)
        {
            gridGenerator = gameObject.GetComponent<WorldGridGenerator>();
            if (gridGenerator == null)
            {
                gridGenerator = gameObject.AddComponent<WorldGridGenerator>();
            }
        }

        // ��������� BorderPlacer, ���� �� �����������
        if (borderPlacer == null)
        {
            borderPlacer = gameObject.GetComponent<BorderPlacer>();
            if (borderPlacer == null)
            {
                borderPlacer = gameObject.AddComponent<BorderPlacer>();
            }
        }

        // ��������� GrassPlacer, ���� �� �����������
        if (grassPlacer == null)
        {
            grassPlacer = gameObject.GetComponent<GrassPlacer>();
            if (grassPlacer == null)
            {
                grassPlacer = gameObject.AddComponent<GrassPlacer>();
            }
        }

        // ��������� TreesPlacer, ���� �� �����������
        if (treesPlacer == null)
        {
            treesPlacer = gameObject.GetComponent<TreesPlacer>();
            if (treesPlacer == null)
            {
                treesPlacer = gameObject.AddComponent<TreesPlacer>();
            }
        }

        // ��������� NavMeshSurface, ���� �� �����������
        if (navMeshSurface == null)
        {
            navMeshSurface = gameObject.GetComponent<NavMeshSurface>();
            if (navMeshSurface == null)
            {
                navMeshSurface = gameObject.AddComponent<NavMeshSurface>();
            }
        }
    }

    // ����� ��� ��������� �������� �� ��������� � ������������� ������
    private void Reset()
    {
        // ������������� ������� � ��������� ����������, ���� ��� �����������
        EnsureComponents();
    }
}
*/