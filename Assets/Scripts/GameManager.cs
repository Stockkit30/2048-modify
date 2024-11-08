using System;
using System.Collections;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using System.Linq;
using Data;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private TileBoard board;
    [SerializeField] private CanvasGroup gameOver;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI hiscoreText;
    [SerializeField] private TMP_Text userLoginDisplay; // ³���������� ����� �������������� �����������

    private int score = 0;
    private HttpClient _client = new HttpClient();

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void Start()
    {
        // �������� ���� �� ���������� ����� � ���������� ����
        string savedLogin = PlayerPrefs.GetString("UserLogin", "");
        userLoginDisplay.text = savedLogin;

        // ����������, �� ���� ��� ������ ������������
        if (string.IsNullOrEmpty(savedLogin))
        {
            Debug.LogWarning("�� ������� ����������� ���� �����������.");
        }

        // ����������� ���� ���
        NewGame();
    }

    public void NewGame()
    {
        SetScore(0);

        // �������� ��'� �������������� �����������
        string currentUser = userLoginDisplay.text;

        // ��������, �� ��'� ����������� ���������
        if (string.IsNullOrEmpty(currentUser))
        {
            Debug.LogWarning("��'� ����������� �� ������.");
            hiscoreText.text = "0";
            return;
        }

        GameUser user = GetUser(currentUser);

        if (user != null)
        {
            int best = user.BestScore;
            hiscoreText.text = best.ToString();
        }
        else
        {
            Debug.LogWarning("����������� �� �������� ��� �� �� �������� ���������.");
            hiscoreText.text = "0";
        }

        gameOver.alpha = 0f;
        gameOver.interactable = false;

        board.ClearBoard();
        board.CreateTile();
        board.CreateTile();
        board.enabled = true;
    }

    public void GameOver()
    {
        board.enabled = false;
        gameOver.interactable = true;
        StartCoroutine(Fade(gameOver, 1f, 1f));
    }

    private IEnumerator Fade(CanvasGroup canvasGroup, float to, float delay = 0f)
    {
        yield return new WaitForSeconds(delay);

        float elapsed = 0f;
        const float duration = 0.5f;
        float from = canvasGroup.alpha;

        while (elapsed < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = to;
    }

    public void IncreaseScore(int points)
    {
        SetScore(score + points);
    }

    private async void SetScore(int newScore)
    {
        score = newScore;
        scoreText.text = score.ToString();

        await SaveHiscore();
    }

    private async Task SaveHiscore()
    {
        string currentUser = userLoginDisplay.text;

        if (string.IsNullOrEmpty(currentUser))
        {
            Debug.LogWarning("��'� ����������� �� ���������.");
            return;
        }

        int hiscore = LoadHiscore(currentUser);

        if (score > hiscore)
        {
            PlayerPrefs.SetInt($"{currentUser}_hiscore", score);
            PlayerPrefs.Save();

            var response = await UpdateScore(currentUser, score);
            if (!response.IsSuccess)
            {
                Debug.LogError($"������� ��������� hiscore �� ������: {response.Message}");
            }
            else
            {
                Debug.Log("Hiscore ������ �������� �� ������.");
            }
        }
    }

    private int LoadHiscore(string user)
    {
        return PlayerPrefs.GetInt($"{user}_hiscore", 0);
    }

    public async Task<Response> UpdateScore(string username, int score)
    {
        try
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Put, $"https://localhost:7204/UpdateUser/{username}/{score}");

            var rawResponse = await _client.SendAsync(requestMessage);
            string content = await rawResponse.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<Response>(content);
        }
        catch (Exception ex)
        {
            return new Response { IsSuccess = false, Message = ex.Message };
        }
    }

    public GameUser GetUser(string username)
    {
        try
        {
            string response = _client.GetStringAsync($"https://localhost:7204/GetUserByUserName/{username}").GetAwaiter().GetResult();
            Debug.Log($"�������� ������� �� ������� ��� ����������� {username}: {response}");

            Response res = JsonConvert.DeserializeObject<Response>(response);
            GameUser data = res?.Content?.FirstOrDefault();
            return data;
        }
        catch (Exception ex)
        {
            Debug.LogError($"������� ��������� ����� �����������: {ex.Message}");
            return null;
        }
    }
}
