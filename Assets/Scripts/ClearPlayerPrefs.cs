using UnityEngine;

public class ClearPlayerPrefs : MonoBehaviour
{
    private void Start()
    {
        PlayerPrefs.DeleteAll(); // ��������� �� �������� ���
        PlayerPrefs.Save();      // �������� ����, ��� ������������, �� ��������� ������
        Debug.Log("PlayerPrefs �������.");
    }
}
