using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using Web.Model;
using Microsoft.AspNetCore.Identity.Data;
using Web.Model;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

public class ApiClient
{
    private readonly HttpClient _httpClient;
    private JsonDocument _jsonDocument;
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
            return new User($"{ex.Message}");
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

    public async Task<List<Web.Model.Task>> GetTaskAsync(User userApi)
    {
        var response = await _httpClient.GetAsync($"http://localhost:5000/api/Auth/GetUserTask?id={userApi.Id}");
        response.EnsureSuccessStatusCode();
        var taskContent = await response.Content.ReadAsStringAsync();
        _jsonDocument = JsonDocument.Parse(taskContent);
        _token = _jsonDocument.RootElement.GetProperty("token");
        var task = JsonSerializer.Deserialize<List<Web.Model.Task>>(_token!);

        return task!.ToList();
    }

    public async Task<User> RegisterUser(string login, string password, string fullName, string email)
    {
        var registrationData = new User
        {
            Id = "string",
            Login = login,
            Password = password,
            FullName = fullName,
            Email = email
        };

        var content = new StringContent(JsonSerializer.Serialize(registrationData), Encoding.UTF8, "application/json");

        var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:5000/api/Auth/register")
        {
            Content = content
        };

        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        return new User { Login = login, Password = password, FullName = fullName, Email = email };
    }

    public async Task<Status> PostStatus(string name)
    {
        var Status = new Status
        {
            Id = 0,
            Name = name
        };

        var content = new StringContent(JsonSerializer.Serialize(Status), Encoding.UTF8, "application/json");

        var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:5000/api/Auth/PostStatus")
        {
            Content = content
        };
        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        return new Status();
    }
    public async Task<Priority> PostPriority(string name)
    {
        var priority = new Priority
        {
            Id = 0,
            Name = name
        };

        var content = new StringContent(JsonSerializer.Serialize(priority), Encoding.UTF8, "application/json");

        var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:5000/api/Auth/PostPriority")
        {
            Content = content
        };
        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        return new Priority();
    }

    public async Task<Web.Model.Task> PostTask(Web.Model.Task task, Web.Model.User user)
    {
        var userandTask = new UserAndTask()
        {
            Task = task,
            User = user
        };
        var content = new StringContent(JsonSerializer.Serialize(userandTask), Encoding.UTF8, "application/json");

        var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:5000/api/Auth/PostTask")
        {
            Content = content
        };
        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        return new Web.Model.Task();
    }

    public async Task<Web.Model.Task> PutTask(Web.Model.Task task, Web.Model.User user)
    {
        var userandTask = new UserAndTask()
        {
            Task = task,
            User = user
        };
        var content = new StringContent(JsonSerializer.Serialize(userandTask), Encoding.UTF8, "application/json");
        var request = new HttpRequestMessage(HttpMethod.Put, "http://localhost:5000/api/Auth/PutTask")
        {
            Content = content
        };
        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        return new Web.Model.Task();
    }

    public async Task<Web.Model.Task> DeleteTask(Web.Model.Task task,Web.Model.User user)
    {
        var userandTask = new UserAndTask()
        {
            Task = task,
            User = user
        };
        var content = new StringContent(JsonSerializer.Serialize(userandTask), Encoding.UTF8, "application/json");
        var request = new HttpRequestMessage(HttpMethod.Delete, "http://localhost:5000/api/Auth/DeleteTask")
        {
            Content = content
        };
        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        return new Web.Model.Task();
    }
}