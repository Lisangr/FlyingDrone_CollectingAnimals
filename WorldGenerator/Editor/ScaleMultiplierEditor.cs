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
        // ������� GUI ��� ����������
        DrawDefaultInspector();

        // �������� ������ �� ������
        ScaleMultiplier scaleMultiplier = (ScaleMultiplier)target;

        // ��������� ������ � ��������
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("��������� �������", EditorStyles.boldLabel);

        if (GUILayout.Button("��������� � 2 ����"))
        {
            scaleMultiplier.MultiplyScale(2);
        }

        if (GUILayout.Button("��������� � 3 ����"))
        {
            scaleMultiplier.MultiplyScale(3);
        }

        if (GUILayout.Button("��������� � 4 ����"))
        {
            scaleMultiplier.MultiplyScale(4);
        }
    }
}
#endif
