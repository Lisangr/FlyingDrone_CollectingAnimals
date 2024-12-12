using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WorldGenerator))]
public class WorldMasterGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Отображаем стандартные поля из скрипта
        DrawDefaultInspector();

        // Добавляем отступ
        GUILayout.Space(10);

        // Создаем красную кнопку "Создать мир"
        GUIStyle redButtonStyle = new GUIStyle(GUI.skin.button);
        redButtonStyle.normal.textColor = Color.red;
        redButtonStyle.fontSize = 14;
        redButtonStyle.fontStyle = FontStyle.Bold;

        WorldGenerator worldMasterGenerator = (WorldGenerator)target;

        // Кнопка для создания мира
        if (GUILayout.Button("Создать мир", redButtonStyle, GUILayout.Height(40)))
        {
            worldMasterGenerator.GenerateFullWorld();
        }

        // Кнопка для очистки мира
        if (GUILayout.Button("Очистить мир", GUILayout.Height(40)))
        {
            worldMasterGenerator.ClearFullWorld();
        }
    }
}
