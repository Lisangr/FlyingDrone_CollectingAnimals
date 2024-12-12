using UnityEngine;
using UnityEditor;

public class DeleteEverySecondChild : MonoBehaviour
{
    [ContextMenu("Delete Every Second Child")]
    private void DeleteEverySecondChildObjects()
    {
        // Проверяем количество детей
        int childCount = transform.childCount;

        // Проходимся по детям с конца, чтобы удаление не нарушило индексирование
        for (int i = childCount - 1; i >= 0; i--)
        {
            // Удаляем каждый второй объект
            if (i % 2 == 1) // Удаляем объекты с нечетными индексами
            {
                DestroyImmediate(transform.GetChild(i).gameObject);
            }
        }
    }
}
