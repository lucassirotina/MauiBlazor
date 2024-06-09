using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiClient.Models.ApiModels;

public class Supervisor
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [ForeignKey("User")]
    public int EmployeeNr { get; set; }
    public string Position { get; set; }
    public string Chair { get; set; }
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string Faculty { get; set; }
    public string OfficeNr { get; set; }
    public ICollection<Project>? Projects { get; set; }
    
    public Supervisor(int employeeNr, string position, string chair, string lastName, string firstName,
                      string faculty, string officeNr)
    {
        EmployeeNr = employeeNr;
        Position = position;
        Chair = chair;
        LastName = lastName;
        FirstName = firstName;
        Faculty = faculty;
        OfficeNr = officeNr;
    }
}
