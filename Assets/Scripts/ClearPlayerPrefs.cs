using UnityEngine;

public class ClearPlayerPrefs : MonoBehaviour
{
    private void Start()
    {
        PlayerPrefs.DeleteAll(); // Видаляємо всі збережені дані
        PlayerPrefs.Save();      // Зберігаємо зміни, щоб переконатися, що видалення успішне
        Debug.Log("PlayerPrefs очищено.");
    }
}
