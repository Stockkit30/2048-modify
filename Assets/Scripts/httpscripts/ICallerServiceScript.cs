#nullable enable

using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using UnityEngine;
using Data;
using System.Linq;
using System.Threading.Tasks;

public interface ICallerService
{
    public GameUser[] GetAllUsers();
    public GameUser GetUser(string username);
    public Response CreateUser(GameUser user);
    public Response Verify(GameUser user);
    public Response Delete(string username);

    public Task<Response> UpdateScore(string username, int score);
}



public class CallerService : ICallerService
{
    private HttpClient _client = new HttpClient();

    public Response CreateUser(GameUser user)
    {
        try
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7204/CreateUser");
            string json = JsonConvert.SerializeObject(user);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = _client.SendAsync(request).GetAwaiter().GetResult();
            string responseContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            Response result = JsonConvert.DeserializeObject<Response>(responseContent);
            return result;
        }
        catch (Exception ex)
        {
            return new Response { IsSuccess = false, Message = ex.Message };
        }
    }

    public Response Verify(GameUser user)
    {
        try
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7204/Verify");
            string json = JsonConvert.SerializeObject(user);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = _client.SendAsync(request).GetAwaiter().GetResult();
            string responseContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            Response result = JsonConvert.DeserializeObject<Response>(responseContent);
            return result;
        }
        catch (Exception ex)
        {
            return new Response { IsSuccess = false, Message = ex.Message };
        }
    }

    public Response Delete(string username)
    {
        try
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"https://localhost:7204/Delete/{username}");
            var response = _client.SendAsync(request).GetAwaiter().GetResult();
            string responseContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            Response res = JsonConvert.DeserializeObject<Response>(responseContent);
            return res;
        }
        catch (Exception ex)
        {
            return new Response { IsSuccess = false, Message = ex.Message };
        }
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
    public GameUser[] GetAllUsers()
    {
        try
        {
            string response = _client.GetStringAsync("https://localhost:7204/GetAllUsers").GetAwaiter().GetResult();
            Response res = JsonConvert.DeserializeObject<Response>(response);
            return res.Content.ToArray() ?? Array.Empty<GameUser>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return Array.Empty<GameUser>();
        }
    }

    public GameUser GetUser(string username)
    {
        try
        {
            string response = _client.GetStringAsync($"https://localhost:7204/GetUserByUserName/{username}").GetAwaiter().GetResult();
            Response res = JsonConvert.DeserializeObject<Response>(response);
            return res.Content.FirstOrDefault() ?? new GameUser();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return new GameUser();
        }
    }
}

public static class ListExtensions
{
    public static GameUser[] ToArray(this List<GameUser>? list)
    {
        GameUser[] users = new GameUser[list == null ? 0 : list.Count];

        for (int i = 0; i < list?.Count; i++)
        {
            users[i] = list[i];
        }

        return users;
    }
}
