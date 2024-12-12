using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WalkingPointsGenerator))]
public class WalkingPointsGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // ���������� ����������� ���� �� �������
        DrawDefaultInspector();

        // ��������� ������
        GUILayout.Space(10);

        // ������� ����� ��� ����� ������
        GUIStyle blueButtonStyle = new GUIStyle(GUI.skin.button);
        blueButtonStyle.normal.textColor = Color.blue;
        blueButtonStyle.fontSize = 14;
        blueButtonStyle.fontStyle = FontStyle.Bold;

        WalkingPointsGenerator generator = (WalkingPointsGenerator)target;

        // ������ ��� ��������� �����
        if (GUILayout.Button("������������� �����", blueButtonStyle, GUILayout.Height(40)))
        {
            generator.GenerateGridPoints();  // �������� ����� ��������� �����
        }

        // ������ ��� ������� ��������������� �����
        if (GUILayout.Button("�������� �����", GUILayout.Height(40)))
        {
            generator.ClearWalkingPoints();  // �������� ����� ��� ������� �����
        }
    }
}
