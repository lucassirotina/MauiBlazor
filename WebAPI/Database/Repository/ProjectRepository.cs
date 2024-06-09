using ApiClient.Models.ApiModels;

namespace WebAPI.Database.Repository;

public interface IProjectRepository
{
    public Project? GetProjectById(int? id);
    public void CreateProject(Project project);
    public void DeleteProject(Project project);
    public List<Project> GetAllProjects();
    public List<Project> GetProjectsBySupervisorId(int supervisorId);
    public void UpdateProject(Project project);
    public void SetProjectStatus(int projectId);
    public bool ProjectNameExists(string projectName);
}

public class ProjectRepository : IProjectRepository
{
    private readonly DataContext context;

    public ProjectRepository(DataContext context)
    {
        this.context = context;
    }

    public Project? GetProjectById(int? id)
    {
        return context.Projects.Find(id);
    }

    public void CreateProject(Project project)
    {
        context.Projects.Add(project);
    }

    public void DeleteProject(Project project)
    {
        context.Projects.Remove(project);
    }

    public void UpdateProject(Project project)
    {
        context.Projects.Update(project);
    }

    public void SetProjectStatus(int projectId)
    {
        Project? project = GetProjectById(projectId);
        project!.ProjectStatus = true;
        context.SaveChanges();
    }

    public List<Project> GetAllProjects()
    {
        return context.Projects.ToList();
    }

    public List<Project> GetProjectsBySupervisorId(int supervisorId)
    {
        return context.Projects.Where(x => x.SupervisorId == supervisorId).ToList();
    }

    public bool ProjectNameExists(string projectName)
    {
        return context.Projects.Any(x => x.ProjectName == projectName);
    }
}
