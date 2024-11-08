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
    public GameObject errorPanel; // ������ ��� �������
    public Button saveButton;
    public Button closeErrorButton; // ������ ��� �������� ����� �������
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
        errorPanel.SetActive(false); // �������� ������ ������� �� �����
        saveButton.onClick.AddListener(SaveUserData);
        closeErrorButton.onClick.AddListener(CloseErrorPanel); // ����'����� ����� �� ������ �������� �������

        _client = new HttpClient();
        _requestMessage = new HttpRequestMessage();
    }

    async void SaveUserData()
    {
        string login = loginInputRegister.text;
        string password = passwordInputRegister.text;

        // ���������� ����� ��� ����� �� ������
        if (login.Length >= minLoginLength && login.Length <= maxLoginLength &&
            password.Length >= minPasswordLength && password.Length <= maxPasswordLength)
        {
            savedLogin = login;
            savedPassword = password;

            var user = new GameUser
            {
                Id = Guid.NewGuid(), // ������������� Guid
                Login = login,
                Password = password,
                BestScore = 0
            };

            Response response = await CreateUser(user); // ����������� ������ CreateUser

            if (response.IsSuccess)
            {
                PlayerPrefs.SetString("UserLogin", savedLogin);
                PlayerPrefs.SetString("UserPassword", savedPassword);
                PlayerPrefs.SetInt($"{login}_hiscore", 0); // ���������� hiscore ��� ������ �����������
                PlayerPrefs.Save();
                registrationSuccessPanel.SetActive(true);
                Debug.Log("��� �������� �� ������ ����������!");
                StartCoroutine(LoadSceneAfterDelay("Home", 1));
            }
            else
            {
                errorPanel.SetActive(true);
                Debug.Log($"������� �� ��� ���������: {response.Message}");
            }
        }
        else
        {
            errorPanel.SetActive(true);
            Debug.Log("���� ��� ������ �� ���������� �������!");
        }
    }

    public async Task<Response> CreateUser(GameUser user)
    {
        string json = JsonConvert.SerializeObject(user);
        _requestMessage.Content = new StringContent(json, Encoding.UTF8, "application/json");
        _requestMessage.Method = HttpMethod.Post;
        _requestMessage.RequestUri = new Uri("https://localhost:7204/CreateUser");

        var response = await _client.SendAsync(_requestMessage); // ������������� SendAsync
        string responseContent = await response.Content.ReadAsStringAsync(); // ������� �������
        Response result = JsonConvert.DeserializeObject<Response>(responseContent);

        return result;
    }

    // ����� ��� �������� ����� ������� � ���������������� �����
    public void CloseErrorPanel()
    {
        errorPanel.SetActive(false); // �������� ������ �������
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // ��������������� �����
    }

    IEnumerator LoadSceneAfterDelay(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}

