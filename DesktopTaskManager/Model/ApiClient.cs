using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Windows;
using WebTaskManager.Model;

namespace DesktopTaskManager.Model
{
    public class ApiClient
    {
        private readonly HttpClient _httpClient;
        private JsonDocument _jsonDocument = null!;
        private JsonElement _token;
        public ApiClient(string apiKey, string apiSecret)
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
        }

        public async Task<User> AuthorizationUser(string login, string password)
        {
            var loginData = new
            {
                id = "string",
                login = login,
                password = password,
                fullName = "string",
                email = "string"
            };

            var content = new StringContent(JsonSerializer.Serialize(loginData), Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:5000/api/Auth/login")
            {
                Content = content
            };

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json"); // Используем Content.Headers.ContentType
            try
            {
                var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var user = await response.Content.ReadAsStringAsync();

                _jsonDocument = JsonDocument.Parse(user);
                var token = _jsonDocument.RootElement.GetProperty("token").GetString();
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
                var userId = jsonToken!.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
                var fullname = jsonToken!.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/fullname")?.Value;

                return new User(userId!, login, password);
            }
            catch (HttpRequestException ex)
            {
                return new User ($"{ex.Message}");
            }
        }

        public async Task<List<Status>> GetStatusAsync()
        {
            var response = await _httpClient.GetAsync("http://localhost:5000/api/Auth/GetStatus");
            response.EnsureSuccessStatusCode();
            var statusContent = await response.Content.ReadAsStringAsync();
            _jsonDocument = JsonDocument.Parse(statusContent);
            _token = _jsonDocument.RootElement.GetProperty("token");
            var statuses = JsonSerializer.Deserialize<List<Status>>(_token.ToString());

            return statuses!.ToList();
        }

        public async Task<List<Priority>> GetPrioritiesAsync()
        {
            var response = await _httpClient.GetAsync("http://localhost:5000/api/Auth/GetPriority");
            response.EnsureSuccessStatusCode();
            var priorityContent = await response.Content.ReadAsStringAsync();
            _jsonDocument = JsonDocument.Parse(priorityContent);
            _token = _jsonDocument.RootElement.GetProperty("token");
            var priority = JsonSerializer.Deserialize<List<Priority>>(_token!);

            return priority!.ToList();
        }

        public async Task<List<WebTaskManager.Model.Task>> GetTaskAsync(User userApi, List<Status> statuses, List<Priority> priority)
        {
            var response = await _httpClient.GetAsync($"http://localhost:5000/api/Auth/GetUserTask?id={userApi.Id}");
            response.EnsureSuccessStatusCode();
            var taskContent = await response.Content.ReadAsStringAsync();
            _jsonDocument = JsonDocument.Parse(taskContent);
            _token = _jsonDocument.RootElement.GetProperty("token");
            var tasks = JsonSerializer.Deserialize<List<WebTaskManager.Model.Task>>(_token!);
            // заполнить свойства Status и Priority данными из API
            foreach (var task in tasks!)
            {
                task.Status = statuses.FirstOrDefault(s => s.Id == task.StatusId)!;
                task.Priority = priority.FirstOrDefault(p => p.Id == task.PriorityId)!;
            }

            return tasks.ToList();
        }
    }
}
