namespace ApiClient.Models;

public class ProjectModel
{
    public int ProjectId { get; set; }
    public string ProjectName { get; set; }
    public string ProjectDescription { get; set; }
    public string SupervisorName { get; set; } 
    public string Degree { get; set; }
    public string Semester {  get; set; }
    public string Language { get; set; }
    public string Type { get; set; }
    public string SecondSupervisor { get; set; }
    public string Requirements { get; set; }
    public DateTime StartDate { get; set; }
    public string Duration { get; set; }
    public string Location { get; set; }
    public string Possibleprojects {  get; set; }
    public string AdditionalInfo { get; set; }
    public string Faculty { get; set; }
    public int EmployeeNr { get; set; }

    public ProjectModel()
    {
        ProjectName = "";
        ProjectDescription = "";
        ProjectId = 0;
    }

    public ProjectModel(int projectId,string projectName, string projectDescription, string degree, string semester, string language,
                        string type, string requirements,string possibleprojects, string faculty, string supervisor, string secondsupervisor,
                        string duration)
    {
        ProjectId = projectId;
        ProjectName = projectName;
        ProjectDescription = projectDescription;
        Degree = degree;
        Semester = semester;
        Language = language;
        Type = type;
        Requirements = requirements;
        Possibleprojects = possibleprojects;
        Faculty = faculty;
        SupervisorName = supervisor;
        SecondSupervisor = secondsupervisor;
        Duration = duration;
    }
}
