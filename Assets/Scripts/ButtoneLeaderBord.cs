using UnityEngine;

public class ButtoneLeaderBord : MonoBehaviour
{
    public GameObject scrollView; // Призначаємо об'єкт ScrollView через інспектор
    public GameObject openButton; // Кнопка для відкриття лідерборда
    public GameObject closeButton; // Кнопка для закриття лідерборда
    public LeaderboardManager leaderboardManager; // Скрипт LeaderboardManager

    void Start()
    {
        // Спочатку робимо ScrollView неактивним
        scrollView.SetActive(false);

        // Додаємо обробники подій для кнопок
        openButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(OpenLeaderboard);
        closeButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(CloseLeaderboard);
    }

    // Функція для відкриття лідерборда
    public void OpenLeaderboard()
    {
        scrollView.SetActive(true); // Активуємо ScrollView
        leaderboardManager.UpdateLeaderboard(); // Оновлюємо лідерборд
    }

    // Функція для закриття лідерборда
    public void CloseLeaderboard()
    {
        scrollView.SetActive(false); // Деактивуємо ScrollView
    }
}
