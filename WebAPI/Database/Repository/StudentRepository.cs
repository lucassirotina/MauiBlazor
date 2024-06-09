using ApiClient.Models.ApiModels;

namespace WebAPI.Database.Repository;

public interface IStudentRepository
{
    public Student? GetUserById(int? id);
    public void CreateStudent(Student student);
    public void DeleteUser(Student user);
    public List<Student> GetAllUser();
    public Student? GetUserByName(int? matrikelnummer);
    public bool UserExists(int matrikelnummer);
}
public class StudentRepository : IStudentRepository
{
    private readonly DataContext context;

    public StudentRepository(DataContext context)
    {
        this.context = context;
    }
    public Student? GetUserById(int? id)
    {
        return context.Students.Find(id);
    }

    public Student? GetUserByName(int? matrikelnummer)
    {
        return context.Students.FirstOrDefault(x => x.StudentId == matrikelnummer);
    }

    public bool UserExists(int matrikelnummer)
    {
        return context.Students.Any(x => x.StudentId == matrikelnummer);
    }

    public void CreateStudent(Student user)
    {
        context.Students.Add(user);
    }

    public void DeleteUser(Student user)
    {
        context.Students.Remove(user);
    }

    public List<Student> GetAllUser()
    {
        return context.Students.ToList();
    }
}
