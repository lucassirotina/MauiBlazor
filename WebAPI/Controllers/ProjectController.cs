using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Database;
using ApiClient.Models.ApiModels;
using ApiClient.Models;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectController : Controller
{
    private readonly DataContext _dataContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ProjectController(DataContext dataContext, IHttpContextAccessor httpContextAccessor) 
    {
        _dataContext = dataContext;
        _httpContextAccessor = httpContextAccessor;
    }

    [Route("GetProjects")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Project>>?> GetProjects()
    {
        return await _dataContext.Projects.ToListAsync();
    }

    [Route("GetAllProjects")]
    [HttpGet]
    //Die Themen werden den Studierenden und Lehrenden basiert auf ihre Fakultät gezeigt 
    public ActionResult<List<ProjectModel>> GetAllProjects()
    {
        var UserId = _httpContextAccessor.HttpContext.Session.GetInt32("UserId");
        var UserRole = _httpContextAccessor.HttpContext.Session.GetString("UserRole");

        using (UnitOfWork unitOfWork = new UnitOfWork())
        {
            var user = unitOfWork.UserRepository.GetUserById(UserId.Value);
            var student = unitOfWork.StudentRepository.GetUserById(user.UserId);
            var supervisor = unitOfWork.LehrenderRepository.GetUserById(user.UserId);
            string? faculty = null;

            if (user.Role == "teacher")
            {
                faculty = supervisor!.Faculty;
            }
            if (user.Role == "student")
            {
                faculty = student!.Faculty;
            }
            var project = unitOfWork.ProjectRepository.GetAllProjects().Where(pr => pr.Faculty == faculty && pr.ProjectStatus == false);

            var projects = project.Select(pr => new ProjectModel()
            {
                ProjectId = pr.ProjectId,
                Faculty = pr.Faculty,
                ProjectName = pr.ProjectName,
                ProjectDescription = pr.ProjectDescription,
                Degree = pr.Degree,
                Language = pr.Language,
                Type = pr.ProjectType,
                Semester = pr.Semester,
                Possibleprojects = pr.PossibleProjects != null ? pr.PossibleProjects : "Keine",
                Duration = pr.ProjectDuration.ToString() + " Monate",
                AdditionalInfo = pr.AdditionalInfo != null ? pr.AdditionalInfo : "Keine zusätzliche Information",
                Requirements = pr.ProjectRequirements != null ? pr.ProjectRequirements : "Keine"
            });

            return projects.ToList();
        }
    }

    //[HttpGet]
    //public async Task<ActionResult<IEnumerable<Project>>?> GetProjects(int userId)
    //{
    //    var UserId = _httpContextAccessor.HttpContext!.Session.GetInt32("UserId");

    //    if (UserId == null)
    //    {
    //        // Weiterleiten zur Login-Seite, wenn der Benutzer nicht eingeloggt ist
    //        return RedirectToAction("Index", "Home");
    //    }
    //    using (UnitOfWork unitOfWork = new UnitOfWork())
    //    {
    //        var project = unitOfWork.ProjectRepository.GetProjectsBySupervisorId(userId);
    //        var projects = project.Select(pr => new ProjectModel()
    //        {
    //            Faculty = pr.Faculty,
    //            ProjectName = pr.ProjectName,
    //            ProjectDescription = pr.ProjectDescription,
    //            Degree = pr.Degree,
    //            Language = pr.Language,
    //            Semester = pr.Semester,
    //            Possibleprojects = pr.PossibleProjects != null ? pr.PossibleProjects : "Keine"
    //        });

    //        return View("FeedPage", new ProjectViewModel(projects.ToList()));
    //    }


    //}

    [HttpGet("{id}")]
    public async Task<ActionResult<Project?>> GetProjectById(int id)
    {
        return await _dataContext.Projects.Where(x => x.ProjectId == id).SingleOrDefaultAsync();
    }

    [HttpPost]
    public async Task<ActionResult> CreateProject(Project project)
    {
        await _dataContext.Projects.AddAsync(project);

        await _dataContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetProjectById), new { id = project.ProjectId }, project);
    }

    [HttpPut]
    public async Task<ActionResult> UpdateProject(Project project)
    {
        _dataContext.Projects.Update(project);
        await _dataContext.SaveChangesAsync();

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteProject(int id)
    {
        var project = await GetProjectById(id);

        if (project.Value == null)
            return NotFound();

        _dataContext.Remove(project.Value);
        await _dataContext.SaveChangesAsync();

        return Ok();
    }
}
