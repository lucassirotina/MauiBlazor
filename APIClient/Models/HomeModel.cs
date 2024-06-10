//using WebAPI.Database;

using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace ApiClient.Models;

public class HomeModel
{
    //public string Username { get; set; }

    [Required]
    public string? Password { get; set; }

    [Required]
    public int UserId { get; set; }

    public HomeModel()
    {
    }

    public HomeModel(int userId, string password)
    {
        UserId = userId;
        Password = password;
    }   

    // PBKDF2 Hash-Algorithmus
    public static string Hash(string password, string salt)
    {
        using (var algorithm = new Rfc2898DeriveBytes(password, Convert.FromBase64String(salt), 500000))
        {
            var hash = algorithm.GetBytes(256 / 8);
            return Convert.ToBase64String(hash);
        }
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
