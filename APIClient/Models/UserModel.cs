namespace ApiClient.Models;

public class UserModel
{
    public string Username { get; set; }
    public string Password { get; set; }
    public int UserId { get; set; }
    public string Role {  get; set; }

    public UserModel() {
        UserId = 0;
        Username = "";
        Password = "";
        Role = "";
    }

    public UserModel(string username, string password, int userId, string role)
    {
        Username = username;
        Password = password;
        UserId = userId;
        Role = role;
    }
}
