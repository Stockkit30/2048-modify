//using UnityEngine;
//using TMPro;
//using UnityEngine.SceneManagement;
//using UnityEngine.UI;
//using System.Collections;
//using Newtonsoft.Json;
//using System.Net.Http;
//using System.Text;
//using System.Threading.Tasks;
//using UnityEditor.PackageManager;
//public class RegistrationManager : MonoBehaviour
//{
//    public TMP_InputField loginInputRegister;
//    public TMP_InputField passwordInputRegister;
//    public GameObject registrationSuccessPanel;
//    public GameObject errorPanel; // Панель для помилок
//    public Button saveButton;
//    public Button closeErrorButton; // Кнопка для закриття панелі помилок
//    private HttpClient _client;

//    private int minLoginLength = 3;
//    private int maxLoginLength = 15;
//    private int minPasswordLength = 8;
//    private int maxPasswordLength = 15;

//    private string savedLogin = "";
//    private string savedPassword = "";

//    void Start()
//    {
//        //_client.GetStringAsync("")
//        registrationSuccessPanel.SetActive(false);
//        errorPanel.SetActive(false); // Вимикаємо панель помилок на старті

//        saveButton.onClick.AddListener(SaveUserData);
//        closeErrorButton.onClick.AddListener(CloseErrorPanel); // Прив'язуємо метод до кнопки закриття помилок
//    }

//    public void SaveUserData()
//    {
//        string login = loginInputRegister.text;
//        string password = passwordInputRegister.text;

//        // Перевіряємо умови для логіна та пароля
//        if (login.Length >= minLoginLength && login.Length <= maxLoginLength &&
//            password.Length >= minPasswordLength && password.Length <= maxPasswordLength)
//        {
//            savedLogin = login;
//            savedPassword = password;

//            PlayerPrefs.SetString("UserLogin", savedLogin);
//            PlayerPrefs.SetString("UserPassword", savedPassword);
//            PlayerPrefs.SetInt($"{login}_hiscore", 0); // Ініціалізуємо hiscore для нового користувача

//            PlayerPrefs.Save();

//            registrationSuccessPanel.SetActive(true);
//            Debug.Log("Дані збережені та панель активована!");

//            StartCoroutine(LoadSceneAfterDelay("Home", 1));
//        }
//        else
//        {
//            // Якщо логін або пароль не відповідають вимогам, активуємо панель помилок
//            errorPanel.SetActive(true);
//            Debug.Log("Логін або пароль не відповідають вимогам!");
//        }
//    }

//    // Метод для закриття панелі помилок і перезавантаження сцени
//    public void CloseErrorPanel()
//    {
       
//        errorPanel.SetActive(false); // Вимикаємо панель помилок
//        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Перезавантажуємо сцену
//    }

//    IEnumerator LoadSceneAfterDelay(string sceneName, float delay)
//    {
//        yield return new WaitForSeconds(delay);
//        SceneManager.LoadScene(sceneName);
//    }
//}
       