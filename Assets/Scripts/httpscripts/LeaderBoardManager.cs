using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Data;

public class LeaderboardManager : MonoBehaviour
{
    public GameObject content; // Container for elements in ScrollView
    public GameObject textPrefab; // Prefab for user row
    private CallerService callerService;

    void Start()
    {
        callerService = new CallerService(); // Initialize the service for server requests
    }

    // Method to update the leaderboard
    public void UpdateLeaderboard()
    {
        try
        {
            // ������� ��������� ������
            foreach (Transform child in content.transform)
            {
                Destroy(child.gameObject);
            }

            // ��������� ��� ������������
            GameUser[] users = callerService.GetAllUsers();

            // ������� �������, ���� ������ ������������ null ��� �������
            if (users == null || users.Length == 0)
            {
                Debug.LogWarning("No users found or GetAllUsers returned null.");
                return;
            }

            // ���������� �� BestScore
            Array.Sort(users, (x, y) => y.BestScore.CompareTo(x.BestScore));

            // ��������� ������ ������ ��� ��� ������������
            string leaderboardText = "";

            // ��������� ����� ������� ����������� �� ������
            foreach (var user in users)
            {
                leaderboardText += $"{user.Login}: {user.BestScore}\n";
            }

            // ��������� ������ ��'���� � ������� ��� ���������
            GameObject newUserEntry = Instantiate(textPrefab, content.transform);
            var textComponent = newUserEntry.GetComponent<TextMeshProUGUI>();

            if (textComponent == null)
            {
                Debug.LogError("Text component not found on the prefab.");
                return;
            }

            // ������������ ������ � ���������
            textComponent.text = leaderboardText;
        }
        catch (Exception ex)
        {
            Debug.LogError("An error occurred in UpdateLeaderboard: " + ex.Message);
        }
    }
}
