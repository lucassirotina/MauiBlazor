using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Database
{
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [ForeignKey("User")]
        public int Matrikelnummer {  get; set; }
        public string Studiengang { get; set; }
        public string StudienAbschluss { get; set; }
        public int Semester { get; set; }
        public string Nachname { get; set; }
        public string Vorname { get; set; }
        public bool Bewerbungsstatus { get; set; }
        [ForeignKey("Projekt")]
        public int? ProjektId { get; set; }
        public int Fakultaet { get; set; }
        public Student() {}
        public Student(int matrikelnummer, string studiengang, string studienAbschluss, int semester, string nachname, string vorname, int fakultaet)
        {
            Matrikelnummer = matrikelnummer;
            Studiengang = studiengang;
            StudienAbschluss = studienAbschluss;
            Semester = semester;
            Nachname = nachname;
            Vorname = vorname;
            Bewerbungsstatus = false;
            ProjektId = null;
            Fakultaet = fakultaet;
        }
    }
}
