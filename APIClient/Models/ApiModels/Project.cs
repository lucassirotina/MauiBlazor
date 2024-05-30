using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiClient.Models.ApiModels;

public class Project
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ProjektId { get; set; }

    public string ProjektName { get; set; }

    public string ProjektBeschreibung { get; set; }

    public bool ProjektStatus { get; set; }

    public DateTime? ProjektStart { get; set; }

    public DateTime? ProjektEnde { get; set; }

    [ForeignKey("Supervisor")]
    public int PrüferId { get; set; }

    [ForeignKey("Student")]
    public int StudentId { get; set; }

    public string? Note { get; set; }

    //public Project() { }

    //public Project(int projektId, string projektName, string projektBeschreibung, bool projektStatus, int prüferId, int studentId, string note)
    //{
    //    ProjektId = projektId;
    //    ProjektName = projektName;
    //    ProjektBeschreibung = projektBeschreibung;
    //    ProjektStatus = projektStatus;
    //    ProjektStart = null;
    //    ProjektEnde = null;
    //    PrüferId = prüferId;
    //    StudentId = studentId;
    //    Note = note;
    //}
}
