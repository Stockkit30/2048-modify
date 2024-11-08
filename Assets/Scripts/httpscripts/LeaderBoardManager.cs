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
            // Очистка попередніх записів
            foreach (Transform child in content.transform)
            {
                Destroy(child.gameObject);
            }

            // Отримання всіх користувачів
            GameUser[] users = callerService.GetAllUsers();

            // Обробка випадку, коли список користувачів null або порожній
            if (users == null || users.Length == 0)
            {
                Debug.LogWarning("No users found or GetAllUsers returned null.");
                return;
            }

            // Сортування за BestScore
            Array.Sort(users, (x, y) => y.BestScore.CompareTo(x.BestScore));

            // Створення нового тексту для всіх користувачів
            string leaderboardText = "";

            // Додавання даних кожного користувача до тексту
            foreach (var user in users)
            {
                leaderboardText += $"{user.Login}: {user.BestScore}\n";
            }

            // Створення одного об'єкта з текстом для лідерборду
            GameObject newUserEntry = Instantiate(textPrefab, content.transform);
            var textComponent = newUserEntry.GetComponent<TextMeshProUGUI>();

            if (textComponent == null)
            {
                Debug.LogError("Text component not found on the prefab.");
                return;
            }

            // Встановлення тексту в компонент
            textComponent.text = leaderboardText;
        }
        catch (Exception ex)
        {
            Debug.LogError("An error occurred in UpdateLeaderboard: " + ex.Message);
        }
    }
}
