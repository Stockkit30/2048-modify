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
    public TMP_InputField loginInputLogin; // ���� ��� �������� �����
    public TMP_InputField passwordInputLogin; // ���� ��� �������� ������
    public Button entryButton; // ���������� ������ Entry
    public GameObject errorPanel; // ������ ��� ����������� �������
    public Button closeButton; // ������ ��� �������� ����� �������

    private HttpClient _client;

    void Start()
    {
        // ����������� HttpClient ��� ���������� ������
        _client = new HttpClient();

        // ����'����� ������ �� ������
        entryButton.onClick.AddListener(() => LoginUser());
        closeButton.onClick.AddListener(CloseErrorPanel);

        // �������� ��������� ������ �������
        errorPanel.SetActive(false);
    }

    public async void LoginUser()
    {
        string login = loginInputLogin.text; // �������� �������� ����
        string password = passwordInputLogin.text; // �������� �������� ������

        // ��������� ��'��� ��� ��������������
        var loginData = new
        {
            Login = login,
            Password = password
        };

        // ���������� ��'��� � JSON
        string json = JsonConvert.SerializeObject(loginData);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        try
        {
            // ��������� ����� �� ������ ��� �������� ����� �� ������
            HttpResponseMessage response = await _client.PostAsync("https://localhost:7204/Verify", content);

            if (response.IsSuccessStatusCode)
            {
                // ����������� ������� �� �������
                string responseBody = await response.Content.ReadAsStringAsync();
                Response serverResponse = JsonConvert.DeserializeObject<Response>(responseBody);

                // ���������� ������ ��������������
                if (serverResponse.IsSuccess)
                {
                    Debug.Log("������� ����!");
                    PlayerPrefs.SetString("UserLogin", login); // �������� ��������� �����������
                    SceneManager.LoadScene("Game"); // ���������� �� ����� ���
                }
                else
                {
                    // ���� �������������� �� ������, �������� ������ �������
                    Debug.Log("������� ���� ��� ������: " + serverResponse.Message);
                    errorPanel.SetActive(true);
                }
            }
            else
            {
                // ���� ������� �� ������� ������ �������
                Debug.Log("������� ������ �� �������.");
                errorPanel.SetActive(true);
            }
        }
        catch (HttpRequestException e)
        {
            // �������� ������� � �������, ���� ������ �� ���������
            Debug.LogError($"������� ���������� �� �������: {e.Message}");
            errorPanel.SetActive(true); // ³��������� ������ �������
        }
    }

    // ����� ��� �������� ����� ������� � ���������������� �����
    public void CloseErrorPanel()
    {
        errorPanel.SetActive(false); // ���������� ������
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // ��������������� �����
    }
}
