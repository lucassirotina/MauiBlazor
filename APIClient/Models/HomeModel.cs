namespace ApiClient.Models;

public class HomeViewModel
{
    public List<UserModel> Users { get; set; }

    public HomeViewModel(List<UserModel> users)
    {
        Users = users;
    }
}

public class ProjectViewModel
{
    public List<ProjectModel> Projects { get; set; }

    public ProjectViewModel(List<ProjectModel> projects)
    {
        Projects = projects;
    }
}

public class UserViewModel
{
    public List<SupervisorModel> Supervisors { get; set; }
    public List<UserModel> Users { get; set; }

    public UserViewModel(List<SupervisorModel> supervisors, List<UserModel> users)
    {
        Supervisors = supervisors;
        Users = users;
    }
}
