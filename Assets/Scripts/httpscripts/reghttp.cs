#nullable enable

using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System;
using Data;

public class reghttp : MonoBehaviour
{
    public TMP_InputField loginInputRegister;
    public TMP_InputField passwordInputRegister;
    public GameObject registrationSuccessPanel;
    public GameObject errorPanel; // Панель для помилок
    public Button saveButton;
    public Button closeErrorButton; // Кнопка для закриття панелі помилок
    private int minLoginLength = 3;
    private int maxLoginLength = 15;
    private int minPasswordLength = 8;
    private int maxPasswordLength = 15;
    private string savedLogin = "";
    private string savedPassword = "";

    private HttpClient _client;
    private HttpRequestMessage _requestMessage;

    void Start()
    {
        registrationSuccessPanel.SetActive(false);
        errorPanel.SetActive(false); // Вимикаємо панель помилок на старті
        saveButton.onClick.AddListener(SaveUserData);
        closeErrorButton.onClick.AddListener(CloseErrorPanel); // Прив'язуємо метод до кнопки закриття помилок

        _client = new HttpClient();
        _requestMessage = new HttpRequestMessage();
    }

    async void SaveUserData()
    {
        string login = loginInputRegister.text;
        string password = passwordInputRegister.text;

        // Перевіряємо умови для логіна та пароля
        if (login.Length >= minLoginLength && login.Length <= maxLoginLength &&
            password.Length >= minPasswordLength && password.Length <= maxPasswordLength)
        {
            savedLogin = login;
            savedPassword = password;

            var user = new GameUser
            {
                Id = Guid.NewGuid(), // Використовуємо Guid
                Login = login,
                Password = password,
                BestScore = 0
            };

            Response response = await CreateUser(user); // Асинхронний виклик CreateUser

            if (response.IsSuccess)
            {
                PlayerPrefs.SetString("UserLogin", savedLogin);
                PlayerPrefs.SetString("UserPassword", savedPassword);
                PlayerPrefs.SetInt($"{login}_hiscore", 0); // Ініціалізуємо hiscore для нового користувача
                PlayerPrefs.Save();
                registrationSuccessPanel.SetActive(true);
                Debug.Log("Дані збережені та панель активована!");
                StartCoroutine(LoadSceneAfterDelay("Home", 1));
            }
            else
            {
                errorPanel.SetActive(true);
                Debug.Log($"Помилка під час реєстрації: {response.Message}");
            }
        }
        else
        {
            errorPanel.SetActive(true);
            Debug.Log("Логін або пароль не відповідають вимогам!");
        }
    }

    public async Task<Response> CreateUser(GameUser user)
    {
        string json = JsonConvert.SerializeObject(user);
        _requestMessage.Content = new StringContent(json, Encoding.UTF8, "application/json");
        _requestMessage.Method = HttpMethod.Post;
        _requestMessage.RequestUri = new Uri("https://localhost:7204/CreateUser");

        var response = await _client.SendAsync(_requestMessage); // Використовуємо SendAsync
        string responseContent = await response.Content.ReadAsStringAsync(); // Очікуємо відповідь
        Response result = JsonConvert.DeserializeObject<Response>(responseContent);

        return result;
    }

    // Метод для закриття панелі помилок і перезавантаження сцени
    public void CloseErrorPanel()
    {
        errorPanel.SetActive(false); // Вимикаємо панель помилок
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Перезавантажуємо сцену
    }

    IEnumerator LoadSceneAfterDelay(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}

