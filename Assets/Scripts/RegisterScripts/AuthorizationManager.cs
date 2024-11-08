using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AuthorizationManager : MonoBehaviour
{
    public TMP_InputField loginInputLogin; // Поле для введення логіну
    public TMP_InputField passwordInputLogin; // Поле для введення пароля
    public Button entryButton; // Оголошення кнопки Entry
    public GameObject errorPanel; // Панель для відображення помилки
    public Button closeButton; // Кнопка для закриття панелі помилки

    void Start()
    {
        // Прив'язуємо методи до кнопок
        entryButton.onClick.AddListener(LoginUser);
        closeButton.onClick.AddListener(CloseErrorPanel);

        // Спочатку приховуємо панель помилки
        errorPanel.SetActive(false);
    }

    public void LoginUser()
    {
        string login = loginInputLogin.text; // Отримуємо введений логін
        string password = passwordInputLogin.text; // Отримуємо введений пароль

        // Отримуємо збережені дані з PlayerPrefs
        string storedLogin = PlayerPrefs.GetString("UserLogin");
        string storedPassword = PlayerPrefs.GetString("UserPassword");

        // Перевіряємо введені дані з збереженими
        if (login == storedLogin && password == storedPassword)
        {
            Debug.Log("Успішний вхід!");
            PlayerPrefs.SetString("CurrentUser", login); // Зберігаємо поточного користувача
            SceneManager.LoadScene("Game"); // Переходь до сцени гри
        }
        else
        {
            Debug.Log("Невірний логін або пароль!");
            errorPanel.SetActive(true); // Активуємо панель помилки
        }
    }

    // Метод для закриття панелі помилки і перезавантаження сцени
    public void CloseErrorPanel()
    {
        errorPanel.SetActive(false); // Деактивуємо панель
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Перезавантажуємо сцену
    }
}
