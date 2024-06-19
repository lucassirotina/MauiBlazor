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

public class RegistrationModel
{
    public int Matrikelnummer { get; set; }

    [Required]
    public string? Password { get; set; }

    [Required]
    public string? PasswordRepeat { get; set; }

    public string? Studiengang { get; set; }

    public string? Studienabschluss { get; set; }

    public int Semester { get; set; }

    [Required]
    public string? Nachname { get; set; }

    [Required]
    public string? Vorname { get; set; }

    [Required]
    public string? UserType { get; set; }

    [Required]
    public string? Email { get; set; }

    [Required]
    public string? EmailRepeat { get; set; }

    public string? Lehrstuhl { get; set; }

    public int MitarbeiterNr { get; set; }

    public string? Position { get; set; }

    [Required]
    public string? Fakultaet { get; set; }

    public string? Raumnummer { get; set; }

    public RegistrationModel()
    {
    }

    public RegistrationModel(int matrikelnummer, string password, string passwordRepeat, string studiengang, string studienabschluss, int semester, string nachname, string vorname, string userType, string email, string emailRepeat, string lehrstuhl, int mitarbeiterNr, string position, string fakultaet, string raumnummer)
    {
        Matrikelnummer = matrikelnummer;
        Password = password;
        PasswordRepeat = passwordRepeat;
        Studiengang = studiengang;
        Studienabschluss = studienabschluss;
        Semester = semester;
        Nachname = nachname;
        Vorname = vorname;
        UserType = userType;
        Email = email;
        EmailRepeat = emailRepeat;
        Lehrstuhl = lehrstuhl;
        MitarbeiterNr = mitarbeiterNr;
        Position = position;
        Fakultaet = fakultaet;
        Raumnummer = raumnummer;
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
