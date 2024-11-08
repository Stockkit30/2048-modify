using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Data;

public class AuthController : MonoBehaviour
{
    public TMP_InputField loginInputLogin; // Поле для введення логіну
    public TMP_InputField passwordInputLogin; // Поле для введення пароля
    public Button entryButton; // Оголошення кнопки Entry
    public GameObject errorPanel; // Панель для відображення помилки
    public Button closeButton; // Кнопка для закриття панелі помилки

    private HttpClient _client;

    void Start()
    {
        // Ініціалізація HttpClient для надсилання запитів
        _client = new HttpClient();

        // Прив'язуємо методи до кнопок
        entryButton.onClick.AddListener(() => LoginUser());
        closeButton.onClick.AddListener(CloseErrorPanel);

        // Спочатку приховуємо панель помилки
        errorPanel.SetActive(false);
    }

    public async void LoginUser()
    {
        string login = loginInputLogin.text; // Отримуємо введений логін
        string password = passwordInputLogin.text; // Отримуємо введений пароль

        // Створюємо об'єкт для автентифікації
        var loginData = new
        {
            Login = login,
            Password = password
        };

        // Конвертуємо об'єкт у JSON
        string json = JsonConvert.SerializeObject(loginData);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        try
        {
            // Надсилаємо запит на сервер для перевірки логіна та пароля
            HttpResponseMessage response = await _client.PostAsync("https://localhost:7204/Verify", content);

            if (response.IsSuccessStatusCode)
            {
                // Десеріалізуємо відповідь від сервера
                string responseBody = await response.Content.ReadAsStringAsync();
                Response serverResponse = JsonConvert.DeserializeObject<Response>(responseBody);

                // Перевіряємо статус автентифікації
                if (serverResponse.IsSuccess)
                {
                    Debug.Log("Успішний вхід!");
                    PlayerPrefs.SetString("UserLogin", login); // Зберігаємо поточного користувача
                    SceneManager.LoadScene("Game"); // Переходимо до сцени гри
                }
                else
                {
                    // Якщо автентифікація не успішна, показуємо панель помилки
                    Debug.Log("Невірний логін або пароль: " + serverResponse.Message);
                    errorPanel.SetActive(true);
                }
            }
            else
            {
                // Якщо відповідь від сервера містить помилку
                Debug.Log("Помилка запиту до сервера.");
                errorPanel.SetActive(true);
            }
        }
        catch (HttpRequestException e)
        {
            // Виводимо помилку в консоль, якщо сервер не доступний
            Debug.LogError($"Помилка підключення до сервера: {e.Message}");
            errorPanel.SetActive(true); // Відображаємо панель помилки
        }
    }

    // Метод для закриття панелі помилки і перезавантаження сцени
    public void CloseErrorPanel()
    {
        errorPanel.SetActive(false); // Деактивуємо панель
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Перезавантажуємо сцену
    }
}
