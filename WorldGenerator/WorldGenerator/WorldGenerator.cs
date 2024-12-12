using UnityEngine;
using UnityEngine.AI;

public class WorldGenerator : MonoBehaviour
{
    // Ссылки на скрипты для генерации (будут автоматически добавлены)
    private WorldGridGenerator gridGenerator;
    private BorderPlacer borderPlacer;
    private GrassPlacer grassPlacer;
    private TreesPlacer treesPlacer;
   // private NavMeshSurface navMeshSurface;  // Ссылка на NavMeshSurface для запекания навмеша

    // Флаги для контроля генерации отдельных элементов
    public bool generateGrid = true;
    public bool placeBorders = true;
    public bool placeGrass = true;
    public bool placeTrees = true;
    public bool bakeNavMesh = true;  // Флаг для контроля запекания навмеша
    public bool autoGenerateOnSceneLoad = true;  // Флаг для автоматической генерации при загрузке сцены

    public delegate void WorldCreated();
    public static event WorldCreated OnWorldCreated;
    // Метод вызывается при загрузке сцены
    private void Start()
    {
        // Если включена автогенерация, запускаем генерацию при загрузке сцены
        if (autoGenerateOnSceneLoad)
        {
            GenerateFullWorld();
        }
    }

    // Метод для генерации всего мира
    public void GenerateFullWorld()
    {
        // Убедимся, что компоненты настроены
        EnsureComponents();

        // Генерация мира
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
        Debug.Log("Я ВЗЯЛ КОМПОНЕНТ И ХОЧУ ЕГО ЗАПЕЧЬ");

        // Запекаем навмеш, если флаг установлен
        if (bakeNavMesh && navMeshSurface != null)
        {
            navMeshSurface.BuildNavMesh();
            Debug.Log("NavMesh has been baked!");
            OnWorldCreated?.Invoke();
        }
        else
        {
            Debug.Log("Я НЕ ЗАПЕК ПОЧЕМУ-ТО");
        }*/
    }

    // Удаление всех старых объектов перед новой генерацией
    public void ClearFullWorld()
    {
        // Убедимся, что компоненты настроены
        EnsureComponents();

        // Очистка мира
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

    // Метод для проверки и добавления компонентов
    private void EnsureComponents()
    {
        // Добавляем WorldGridGenerator, если он отсутствует
        if (gridGenerator == null)
        {
            gridGenerator = gameObject.GetComponent<WorldGridGenerator>();
            if (gridGenerator == null)
            {
                gridGenerator = gameObject.AddComponent<WorldGridGenerator>();
            }
        }

        // Добавляем BorderPlacer, если он отсутствует
        if (borderPlacer == null)
        {
            borderPlacer = gameObject.GetComponent<BorderPlacer>();
            if (borderPlacer == null)
            {
                borderPlacer = gameObject.AddComponent<BorderPlacer>();
            }
        }

        // Добавляем GrassPlacer, если он отсутствует
        if (grassPlacer == null)
        {
            grassPlacer = gameObject.GetComponent<GrassPlacer>();
            if (grassPlacer == null)
            {
                grassPlacer = gameObject.AddComponent<GrassPlacer>();
            }
        }

        // Добавляем TreesPlacer, если он отсутствует
        if (treesPlacer == null)
        {
            treesPlacer = gameObject.GetComponent<TreesPlacer>();
            if (treesPlacer == null)
            {
                treesPlacer = gameObject.AddComponent<TreesPlacer>();
            }
        }
        /*
        // Добавляем NavMeshSurface, если он отсутствует
        if (navMeshSurface == null)
        {
            navMeshSurface = gameObject.GetComponent<NavMeshSurface>();
            if (navMeshSurface == null)
            {
                navMeshSurface = gameObject.AddComponent<NavMeshSurface>();
            }
        }*/
    }

    // Метод для установки значений по умолчанию и инициализации ссылок
    private void Reset()
    {
        // Автоматически находим и добавляем компоненты, если они отсутствуют
        EnsureComponents();
    }
}





/*
using UnityEngine;
using UnityEngine.AI;

public class WorldGenerator : MonoBehaviour
{
    // Ссылки на скрипты для генерации (будут автоматически добавлены)
    private WorldGridGenerator gridGenerator;
    private BorderPlacer borderPlacer;
    private GrassPlacer grassPlacer;
    private TreesPlacer treesPlacer;
    private NavMeshSurface navMeshSurface;  // Ссылка на NavMeshSurface для запекания навмеша

    // Флаги для контроля генерации отдельных элементов
    public bool generateGrid = true;
    public bool placeBorders = true;
    public bool placeGrass = true;
    public bool placeTrees = true;
    public bool bakeNavMesh = true;  // Флаг для контроля запекания навмеша

    // Метод для генерации всего мира
    public void GenerateFullWorld()
    {
        // Убедимся, что компоненты настроены
        EnsureComponents();

        // Генерация мира
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

        // Запекаем навмеш, если флаг установлен
        if (bakeNavMesh && navMeshSurface != null)
        {
            navMeshSurface.BuildNavMesh();
            Debug.Log("NavMesh has been baked!");
        }
    }

    // Удаление всех старых объектов перед новой генерацией
    public void ClearFullWorld()
    {
        // Убедимся, что компоненты настроены
        EnsureComponents();

        // Очистка мира
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

    // Метод для проверки и добавления компонентов
    private void EnsureComponents()
    {
        // Добавляем WorldGridGenerator, если он отсутствует
        if (gridGenerator == null)
        {
            gridGenerator = gameObject.GetComponent<WorldGridGenerator>();
            if (gridGenerator == null)
            {
                gridGenerator = gameObject.AddComponent<WorldGridGenerator>();
            }
        }

        // Добавляем BorderPlacer, если он отсутствует
        if (borderPlacer == null)
        {
            borderPlacer = gameObject.GetComponent<BorderPlacer>();
            if (borderPlacer == null)
            {
                borderPlacer = gameObject.AddComponent<BorderPlacer>();
            }
        }

        // Добавляем GrassPlacer, если он отсутствует
        if (grassPlacer == null)
        {
            grassPlacer = gameObject.GetComponent<GrassPlacer>();
            if (grassPlacer == null)
            {
                grassPlacer = gameObject.AddComponent<GrassPlacer>();
            }
        }

        // Добавляем TreesPlacer, если он отсутствует
        if (treesPlacer == null)
        {
            treesPlacer = gameObject.GetComponent<TreesPlacer>();
            if (treesPlacer == null)
            {
                treesPlacer = gameObject.AddComponent<TreesPlacer>();
            }
        }

        // Добавляем NavMeshSurface, если он отсутствует
        if (navMeshSurface == null)
        {
            navMeshSurface = gameObject.GetComponent<NavMeshSurface>();
            if (navMeshSurface == null)
            {
                navMeshSurface = gameObject.AddComponent<NavMeshSurface>();
            }
        }
    }

    // Метод для установки значений по умолчанию и инициализации ссылок
    private void Reset()
    {
        // Автоматически находим и добавляем компоненты, если они отсутствуют
        EnsureComponents();
    }
}
*/