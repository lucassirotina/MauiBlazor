using ApiClient.Models.ApiModels;

namespace WebAPI.Database.Repository;

public interface IApplicationRepository
{
    public Application GetApplicationById(int id);
    public void CreateApplication(Application application);
    public void DeleteApplication(Application application);
    public List<Application> GetAllApplications();
}

public class ApplicationRepository : IApplicationRepository
{
    private DataContext context;

    public ApplicationRepository(DataContext context)
    {
        this.context = context;
    }

    public Application GetApplicationById(int id)
    {
        return context.Applications.Find(id);
    }

    public void CreateApplication(Application application)
    {
        context.Applications.Add(application);
    }

    public void DeleteApplication(Application application)
    {
        context.Applications.Remove(application);
    }

    public List<Application> GetAllApplications()
    {
        return context.Applications.ToList();
    }

}