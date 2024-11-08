using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AuthorizationManager : MonoBehaviour
{
    public TMP_InputField loginInputLogin; // ���� ��� �������� �����
    public TMP_InputField passwordInputLogin; // ���� ��� �������� ������
    public Button entryButton; // ���������� ������ Entry
    public GameObject errorPanel; // ������ ��� ����������� �������
    public Button closeButton; // ������ ��� �������� ����� �������

    void Start()
    {
        // ����'����� ������ �� ������
        entryButton.onClick.AddListener(LoginUser);
        closeButton.onClick.AddListener(CloseErrorPanel);

        // �������� ��������� ������ �������
        errorPanel.SetActive(false);
    }

    public void LoginUser()
    {
        string login = loginInputLogin.text; // �������� �������� ����
        string password = passwordInputLogin.text; // �������� �������� ������

        // �������� �������� ��� � PlayerPrefs
        string storedLogin = PlayerPrefs.GetString("UserLogin");
        string storedPassword = PlayerPrefs.GetString("UserPassword");

        // ���������� ������ ��� � �����������
        if (login == storedLogin && password == storedPassword)
        {
            Debug.Log("������� ����!");
            PlayerPrefs.SetString("CurrentUser", login); // �������� ��������� �����������
            SceneManager.LoadScene("Game"); // �������� �� ����� ���
        }
        else
        {
            Debug.Log("������� ���� ��� ������!");
            errorPanel.SetActive(true); // �������� ������ �������
        }
    }

    // ����� ��� �������� ����� ������� � ���������������� �����
    public void CloseErrorPanel()
    {
        errorPanel.SetActive(false); // ���������� ������
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // ��������������� �����
    }
}
