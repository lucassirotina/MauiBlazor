using ApiClient.Models;
using ApiClient.Models.ApiModels;
using System.Net.Http.Json;

namespace ApiClient;

public class ApiClientService
{
    private readonly HttpClient _httpClient;

    public ApiClientService(ApiClientOptions apiClientOptions)
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(apiClientOptions.BaseAddress!);
    }

    #region Project
    public async Task<List<Project>?> GetAllProjects()
    {
        List<Project>? projects = await _httpClient.GetFromJsonAsync<List<Project>?>("/api/Project");
        return projects;
    }

    public async Task<Project?> GetProjectById(int id)
    {
        return await _httpClient.GetFromJsonAsync<Project?>($"/api/Project/{id}");
    }

    public async Task SaveProject(Project project)
    {
        await _httpClient.PostAsJsonAsync("/api/Project", project);
    }

    public async Task UpdateProject(Project project)
    {
        await _httpClient.PutAsJsonAsync("/api/Project", project);
    }

    public async Task DeleteProject(int id)
    {
        await _httpClient.DeleteAsync($"/api/Project/{id}");
    }
    #endregion

    #region User
    public async Task<User?> GetUserById(int id)
    {
        return await _httpClient.GetFromJsonAsync<User?>($"/api/User/{id}");
    }

    public async Task<List<ProjectModel>?> Login(int userId, string password)
    {
        var response = await _httpClient.PostAsJsonAsync("/api/Home/Login", new HomeModel( userId, password ));
        if (response.IsSuccessStatusCode)
        {
            var projects = await response.Content.ReadFromJsonAsync<List<ProjectModel>>();
            return projects;
        }
        
        return new List<ProjectModel>();
    }
    #endregion
}
