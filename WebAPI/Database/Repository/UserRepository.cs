using ApiClient.Models.ApiModels;

namespace WebAPI.Database.Repository;

public interface IUserRepository
{
    public User GetUserById(int id);
    public User GetUserByEmail(string Email);
    public void CreateUser(User user);
    public void DeleteUser(User user);
    public IDictionary<string, string> LoadUserFromDB();
    public List<User> GetAllUser();
    public User GetUserByName(string userName);
    public bool UserExists(string userName);
    public bool EmailExists(string email);
}

public class UserRepository : IUserRepository
{
    private DataContext context;

    public UserRepository(DataContext context)
    {
        this.context = context;
    }

    public User GetUserById(int id)
    {
        return context.Users.Find(id);
    }

    public User GetUserByName(string userName)
    {
        return context.Users.FirstOrDefault(x => x.UserName == userName);
    }
    
    public User GetUserByEmail(string email)
    {
        return context.Users.FirstOrDefault(x => x.Email == email);
    }

    public bool UserExists(string userName)
    {
        return context.Users.Any(x => x.UserName == userName);
    }
    
    public bool EmailExists(string email)
    {
        return context.Users.Any(x => x.Email == email);
    }

    public void CreateUser(User user)
    {
        context.Users.Add(user);
    }

    public void DeleteUser(User user)
    {
        context.Users.Remove(user);
    }

    public List<User> GetAllUser()
    {
        return context.Users.ToList();
    }

    public IDictionary<string, string> LoadUserFromDB()
    {
        IDictionary<string, string> User = new Dictionary<string, string>();
        foreach (User user in context.Users)
        {
            User[user.UserName] = user.Password;
        }
        return User;
    }
}
