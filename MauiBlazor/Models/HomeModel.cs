//using WebAPI.Database;

using System.ComponentModel.DataAnnotations;

namespace MauiBlazor.Models;

public class HomeModel
{
    //public string Username { get; set; }

    [Required]
    public string? Password { get; set; }

    [Required]
    public int UserId { get; set; }
}

//public class ProjectViewModel
//{

//    public List<ProjectModel> Projects { get; set; }
//    public ProjectViewModel(List<ProjectModel> projects)
//    {
//        Projects = projects;
//    }
//}
