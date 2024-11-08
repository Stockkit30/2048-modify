using UnityEngine;

public class ButtoneLeaderBord : MonoBehaviour
{
    public GameObject scrollView; // ���������� ��'��� ScrollView ����� ���������
    public GameObject openButton; // ������ ��� �������� ���������
    public GameObject closeButton; // ������ ��� �������� ���������
    public LeaderboardManager leaderboardManager; // ������ LeaderboardManager

    void Start()
    {
        // �������� ������ ScrollView ����������
        scrollView.SetActive(false);

        // ������ ��������� ���� ��� ������
        openButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(OpenLeaderboard);
        closeButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(CloseLeaderboard);
    }

    // ������� ��� �������� ���������
    public void OpenLeaderboard()
    {
        scrollView.SetActive(true); // �������� ScrollView
        leaderboardManager.UpdateLeaderboard(); // ��������� ��������
    }

    // ������� ��� �������� ���������
    public void CloseLeaderboard()
    {
        scrollView.SetActive(false); // ���������� ScrollView
    }
}
