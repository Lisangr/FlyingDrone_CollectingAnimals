using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WorldGenerator))]
public class WorldMasterGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // ���������� ����������� ���� �� �������
        DrawDefaultInspector();

        // ��������� ������
        GUILayout.Space(10);

        // ������� ������� ������ "������� ���"
        GUIStyle redButtonStyle = new GUIStyle(GUI.skin.button);
        redButtonStyle.normal.textColor = Color.red;
        redButtonStyle.fontSize = 14;
        redButtonStyle.fontStyle = FontStyle.Bold;

        WorldGenerator worldMasterGenerator = (WorldGenerator)target;

        // ������ ��� �������� ����
        if (GUILayout.Button("������� ���", redButtonStyle, GUILayout.Height(40)))
        {
            worldMasterGenerator.GenerateFullWorld();
        }

        // ������ ��� ������� ����
        if (GUILayout.Button("�������� ���", GUILayout.Height(40)))
        {
            worldMasterGenerator.ClearFullWorld();
        }
    }
}
