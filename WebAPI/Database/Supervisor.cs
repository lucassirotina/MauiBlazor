using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Database
{
    public class Supervisor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [ForeignKey("User")]
        public int MitarbeiterNr { get; set; }
        public string Position { get; set; }
        public string Lehrstuhl {  get; set; }
        public string Nachname { get; set; }
        public string Vorname { get; set; }
        public int Fakultaet { get; set; }
        public string Raumnummer { get; set; }
        public ICollection<Project> Projekte { get; set; }
        public Supervisor() { }
        public Supervisor(int mitarbeiterNr, string position, string lehrstuhl, string nachname, string vorname, int fakultaet, string raumnummer)
        {
            Lehrstuhl = lehrstuhl;
            Position = position;
            MitarbeiterNr = mitarbeiterNr;
            Vorname = vorname;
            Nachname = nachname;
            Fakultaet = fakultaet;
            Raumnummer = raumnummer;
        }
    }
}
