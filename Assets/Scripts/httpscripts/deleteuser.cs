using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Data;
using UnityEngine.SceneManagement; // ��������� ��� ������������ �����

public class DeleteUserButton : MonoBehaviour
{
    // ��'���� � CallerService
    private CallerService _callerService;

    // ���� ��� �������� ����� �����������, ����� ������� ��������
    public TMP_InputField usernameInputField;

    // ���� ��� ��������� ����������
    public TextMeshProUGUI messageText; // ���� �� TextMeshProUGUI

    void Start()
    {
        _callerService = new CallerService(); // ����������� ������
    }

    // �����, ���� ����������� ��� ��������� �� ������
    public void OnDeleteUserButtonClick()
    {
        string username = usernameInputField.text; // ��������� ����� ����������� � TMP_InputField

        if (!string.IsNullOrEmpty(username))
        {
            // ������ ������ ���������
            Response response = _callerService.Delete(username);

            if (response.IsSuccess)
            {
                messageText.text = $"User '{username}' successfully deleted!";
                StartCoroutine(LoadHomeSceneAfterDelay(1f)); // ������ ��������
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

    // �������� ��� ������������ ����� "Home" ����� ������ ��������
    private IEnumerator LoadHomeSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // ������ ������ ������� ������
        SceneManager.LoadScene("Home"); // ����������� ����� "Home"
    }
}
