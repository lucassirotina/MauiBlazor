using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiClient.Models.ApiModels;

public class Student
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [ForeignKey("User")]
    public int StudentId { get; set; }
    public string Course { get; set; }
    public string Degree { get; set; }
    public int Semester { get; set; }
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public bool ApplicationStatus { get; set; }

    [ForeignKey("Projekt")]
    public int? ProjectId { get; set; }
    public string Faculty { get; set; }
    
    public Student(int studentId, string course, string degree, int semester, string lastName, string firstName, string faculty)
    {
        StudentId = studentId;
        Course = course;
        Degree = degree;
        Semester = semester;
        LastName = lastName;
        FirstName = firstName;
        Faculty = faculty;
    }
}
