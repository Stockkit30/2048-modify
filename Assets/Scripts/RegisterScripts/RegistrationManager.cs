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
//    public GameObject errorPanel; // ������ ��� �������
//    public Button saveButton;
//    public Button closeErrorButton; // ������ ��� �������� ����� �������
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
//        errorPanel.SetActive(false); // �������� ������ ������� �� �����

//        saveButton.onClick.AddListener(SaveUserData);
//        closeErrorButton.onClick.AddListener(CloseErrorPanel); // ����'����� ����� �� ������ �������� �������
//    }

//    public void SaveUserData()
//    {
//        string login = loginInputRegister.text;
//        string password = passwordInputRegister.text;

//        // ���������� ����� ��� ����� �� ������
//        if (login.Length >= minLoginLength && login.Length <= maxLoginLength &&
//            password.Length >= minPasswordLength && password.Length <= maxPasswordLength)
//        {
//            savedLogin = login;
//            savedPassword = password;

//            PlayerPrefs.SetString("UserLogin", savedLogin);
//            PlayerPrefs.SetString("UserPassword", savedPassword);
//            PlayerPrefs.SetInt($"{login}_hiscore", 0); // ���������� hiscore ��� ������ �����������

//            PlayerPrefs.Save();

//            registrationSuccessPanel.SetActive(true);
//            Debug.Log("��� �������� �� ������ ����������!");

//            StartCoroutine(LoadSceneAfterDelay("Home", 1));
//        }
//        else
//        {
//            // ���� ���� ��� ������ �� ���������� �������, �������� ������ �������
//            errorPanel.SetActive(true);
//            Debug.Log("���� ��� ������ �� ���������� �������!");
//        }
//    }

//    // ����� ��� �������� ����� ������� � ���������������� �����
//    public void CloseErrorPanel()
//    {
       
//        errorPanel.SetActive(false); // �������� ������ �������
//        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // ��������������� �����
//    }

//    IEnumerator LoadSceneAfterDelay(string sceneName, float delay)
//    {
//        yield return new WaitForSeconds(delay);
//        SceneManager.LoadScene(sceneName);
//    }
//}
       