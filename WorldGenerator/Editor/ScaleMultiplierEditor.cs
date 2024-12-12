using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

#if UNITY_EDITOR
[CustomEditor(typeof(ScaleMultiplier))]
public class ScaleMultiplierEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Базовый GUI для компонента
        DrawDefaultInspector();

        // Получаем ссылку на скрипт
        ScaleMultiplier scaleMultiplier = (ScaleMultiplier)target;

        // Добавляем кнопки в редактор
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Увеличить масштаб", EditorStyles.boldLabel);

        if (GUILayout.Button("Увеличить в 2 раза"))
        {
            scaleMultiplier.MultiplyScale(2);
        }

        if (GUILayout.Button("Увеличить в 3 раза"))
        {
            scaleMultiplier.MultiplyScale(3);
        }

        if (GUILayout.Button("Увеличить в 4 раза"))
        {
            scaleMultiplier.MultiplyScale(4);
        }
    }
}
#endif
