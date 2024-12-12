using UnityEngine;

[CreateAssetMenu(fileName = "NewAnimalData", menuName = "Animal/AnimalData")]
public class AnimalData : ScriptableObject
{
    // Публичные поля для опыта, золота и энергии
    public int experience = 0; // Опыт животного
    public int gold = 0; // Количество золота
    public int energy = 5; // Энергия животного    
}
