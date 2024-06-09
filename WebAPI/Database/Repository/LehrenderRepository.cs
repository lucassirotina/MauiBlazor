using ApiClient.Models.ApiModels;

namespace WebAPI.Database.Repository;

public interface ILehrenderRepository
{
    public Supervisor? GetUserById(int? id);
    public Supervisor? GetSupervisorByName(string? name);
    public void CreateSupervisor(Supervisor supervisor);
    public void DeleteUser(User user);
    public IDictionary<string, string> LoadUserFromDB();
    public List<Supervisor> GetAllUser();
    public User? GetUserByName(string? userName);
    public bool UserExists(string userName);
}
public class LehrenderRepository : ILehrenderRepository
{
    private DataContext context;

    public LehrenderRepository(DataContext context)
    {
        this.context = context;
    }

    public Supervisor? GetUserById(int? id)
    {
        return context.Supervisors.Find(id);
    }

    public Supervisor? GetSupervisorByName(string? name)
    {
        return context.Supervisors.FirstOrDefault(x => x.LastName == name);
    }

    public User? GetUserByName(string? userName)
    {
        return context.Users.FirstOrDefault(x => x.UserName == userName);
    }

    public bool UserExists(string userName)
    {
        return context.Users.Any(x => x.UserName == userName);
    }

    public void CreateSupervisor(Supervisor user)
    {
        context.Supervisors.Add(user);
    }

    public void DeleteUser(User user)
    {
        context.Users.Remove(user);
    }

    public List<Supervisor> GetAllUser()
    {
        return context.Supervisors.ToList();
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
