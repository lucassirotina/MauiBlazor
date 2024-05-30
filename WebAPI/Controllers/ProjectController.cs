using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Database;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectController : Controller
{
    private DataContext _dataContext;

    public ProjectController(DataContext dataContext) => _dataContext = dataContext;

    [HttpGet]
    public ActionResult<IEnumerable<Project>> GetProjects()
    {
        return _dataContext.Projects;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Project?>> GetProjectById(int id)
    {
        return await _dataContext.Projects.Where(x => x.ProjektId == id).SingleOrDefaultAsync();
    }

    [HttpPost]
    public async Task<ActionResult> CreateProject(Project project)
    {
        await _dataContext.Projects.AddAsync(project);

        await _dataContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetProjectById), new { id = project.ProjektId }, project);
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
