using Data;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;



namespace Data
{
    public class CallerService : ICallerService
    {
        private HttpRequestMessage _requestMessage = new HttpRequestMessage();
        private HttpClient _client = new HttpClient();

        public CallerService()
        {
            _requestMessage.Headers.Add("Users", "application/json");
        }

        public Response CreateUser(GameUser user)
        {
            string json = JsonConvert.SerializeObject(user);
            _requestMessage.Content = new StringContent(json, Encoding.UTF8, "application/json");
            _requestMessage.Method = HttpMethod.Post;
            _requestMessage.RequestUri = new Uri("https://localhost:7204/CreateUser");

            
            var response = _client.SendAsync(_requestMessage).GetAwaiter().GetResult();
            string responseContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            Response result = JsonConvert.DeserializeObject<Response>(responseContent);

            return result;
        }

        public Response Verify(GameUser user)
        {
            string json = JsonConvert.SerializeObject(user);
            _requestMessage.Content = new StringContent(json, Encoding.UTF8, "application/json");
            _requestMessage.Method = HttpMethod.Post;
            _requestMessage.RequestUri = new Uri("https://localhost:7204/Verify");

            var response = _client.SendAsync(_requestMessage).GetAwaiter().GetResult();
            string responseContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            Response result = JsonConvert.DeserializeObject<Response>(responseContent);

            return result;
        }

        public Response Delete(string username)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"https://localhost:7204/Delete/{username}");
            var response = _client.SendAsync(_requestMessage).GetAwaiter().GetResult();

            string responseContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            Response res = JsonConvert.DeserializeObject<Response>(responseContent);
            return res;
        }


        public GameUser[] GetAllUsers()
        {
            string response = _client.GetStringAsync("https://localhost:7204/GetAllUsers").GetAwaiter().GetResult();
            Response res = JsonConvert.DeserializeObject<Response>(response);
            GameUser[] data = res?.Content.ToArray();
            return data;
        }

        public GameUser GetUser(string username)
        {
            string response = _client.GetStringAsync($"https://localhost:7204/GetUserByUserName/{username}").GetAwaiter().GetResult();
            Response res = JsonConvert.DeserializeObject<Response>(response);
            GameUser data = res?.Content.ToArray().First();
            return data;
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

    }



}
