using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WalkingPointsGenerator))]
public class WalkingPointsGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Отображаем стандартные поля из скрипта
        DrawDefaultInspector();

        // Добавляем отступ
        GUILayout.Space(10);

        // Создаем стиль для синей кнопки
        GUIStyle blueButtonStyle = new GUIStyle(GUI.skin.button);
        blueButtonStyle.normal.textColor = Color.blue;
        blueButtonStyle.fontSize = 14;
        blueButtonStyle.fontStyle = FontStyle.Bold;

        WalkingPointsGenerator generator = (WalkingPointsGenerator)target;

        // Кнопка для генерации точек
        if (GUILayout.Button("Сгенерировать точки", blueButtonStyle, GUILayout.Height(40)))
        {
            generator.GenerateGridPoints();  // Вызываем метод генерации точек
        }

        // Кнопка для очистки сгенерированных точек
        if (GUILayout.Button("Очистить точки", GUILayout.Height(40)))
        {
            generator.ClearWalkingPoints();  // Вызываем метод для очистки точек
        }
    }
}
