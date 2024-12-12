using UnityEngine;

[CreateAssetMenu(fileName = "NewAnimalData", menuName = "Animal/AnimalData")]
public class AnimalData : ScriptableObject
{
    // ��������� ���� ��� �����, ������ � �������
    public int experience = 0; // ���� ���������
    public int gold = 0; // ���������� ������
    public int energy = 5; // ������� ���������    
}
