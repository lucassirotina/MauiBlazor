using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiClient.Models.ApiModels;

public class Project
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ProjectId { get; set; }
    public string ProjectName { get; set; }
    public string ProjectDescription { get; set; }
    public string? ProjectRequirements { get; set; }
    public int SecondSupervisorId { get; set; }
    public bool ProjectStatus { get; set; }
    public DateTime? ProjectBeginn { get; set; }
    public int? ProjectDuration { get; set; }
    public string? ProjectLocation { get; set; }
    public string? AdditionalInfo { get; set; }
    public string? PossibleProjects { get; set; }
    public string? Semester { get; set; }
    public string Degree { get; set; }
    public string Language { get; set; }
    public DateTime? ProjectEnd { get; set; }

    [ForeignKey("Supervisor")]
    public int SupervisorId { get; set; }

    [ForeignKey("Student")]
    public int StudentId { get; set; }
    public float? Grade { get; set; }
    public string ProjectType { get; set; }
    public string Faculty { get; set; }

    public Project()
    {
    }

    public Project(string projectName, string projectDescription, DateTime? projectBeginn, int? projectDuration,
                       string? additionalInfo, string? possibleProjects, string degree, string language, string? requirements,
                       string? projectLocation, int supervisorId, int secondSupervisorId, string projectType, string? semester, string faculty)

    {
        ProjectName = projectName;
        ProjectDescription = projectDescription;
        ProjectStatus = false;
        ProjectBeginn = projectBeginn;
        ProjectDuration = projectDuration;
        AdditionalInfo = additionalInfo;
        PossibleProjects = possibleProjects;
        Degree = degree;
        Language = language;
        ProjectRequirements = requirements;
        ProjectLocation = projectLocation;
        SupervisorId = supervisorId;
        SecondSupervisorId = secondSupervisorId;
        ProjectType = projectType;
        Semester = semester;
        Faculty = faculty;
    }
}
