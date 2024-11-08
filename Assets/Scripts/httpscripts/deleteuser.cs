using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Data;
using UnityEngine.SceneManagement; // Необхідно для завантаження сцени

public class DeleteUserButton : MonoBehaviour
{
    // Зв'язок з CallerService
    private CallerService _callerService;

    // Поле для введення логіну користувача, якого потрібно видалити
    public TMP_InputField usernameInputField;

    // Поле для виведення повідомлень
    public TextMeshProUGUI messageText; // Зміна на TextMeshProUGUI

    void Start()
    {
        _callerService = new CallerService(); // Ініціалізація сервісу
    }

    // Метод, який викликається при натисканні на кнопку
    public void OnDeleteUserButtonClick()
    {
        string username = usernameInputField.text; // Отримання логіну користувача з TMP_InputField

        if (!string.IsNullOrEmpty(username))
        {
            // Виклик методу видалення
            Response response = _callerService.Delete(username);

            if (response.IsSuccess)
            {
                messageText.text = $"User '{username}' successfully deleted!";
                StartCoroutine(LoadHomeSceneAfterDelay(1f)); // Запуск корутини
            }
            else
            {
                messageText.text = $"Failed to delete user '{username}'. Error: {response.Message}";
            }
        }
        else
        {
            messageText.text = "Please enter a valid username.";
        }
    }

    // Корутина для завантаження сцени "Home" через задану затримку
    private IEnumerator LoadHomeSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Чекати задану кількість секунд
        SceneManager.LoadScene("Home"); // Завантажити сцену "Home"
    }
}
