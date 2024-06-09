using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiClient.Models.ApiModels;

public class Application
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ApplicationId { get; set; }
    [ForeignKey("Student")]
    public int StudentId { get; set; }
    [ForeignKey("Supervisor")]
    public int SupervisorId { get; set; }
    [ForeignKey("Project")]
    public int ProjectId { get; set; }
    public bool? Accept { get; set; }
    public DateTime ApplicationDate { get; set; }


    public Application() { }
    
    public Application(int studentId, int supervisorId, int projectId)
    {
        StudentId = studentId;
        SupervisorId = supervisorId;
        ProjectId = projectId;
        Accept = null;
        ApplicationDate = DateTime.Now;
    }
}
