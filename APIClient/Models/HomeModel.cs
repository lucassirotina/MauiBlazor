//using ApiClient.Database;

namespace ApiClient.Models;

public class HomeModel
{
    public string Username { get; set; }
    public string? Password { get; set; }
    public int UserId { get; set; }
    public HomeModel(string username, int userId)
    {
        Username = username;
        UserId = userId;
    }
}

//public class ProjectViewModel
//{

//    public List<ProjectModel> Projects { get; set; }
//    public ProjectViewModel(List<ProjectModel> projects)
//    {
//        Projects = projects;
//    }
//}
