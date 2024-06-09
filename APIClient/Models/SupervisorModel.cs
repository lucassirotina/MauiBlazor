namespace ApiClient.Models;

public class SupervisorModel
{
    public int SupervisorId { get; set; }
    public string SupervisorName { get; set;}
    public string SupervisorFirstname { get; set;}
    public int SecondSupervisorId { get; set; }
    public string Faculty {  get; set;}
    public string Chair { get; set;}
    public string Position { get; set;}

    public SupervisorModel()
    {
        SupervisorId = 0;
        SecondSupervisorId = 0;
        SupervisorName = "";
        SupervisorFirstname = "";
        Faculty = "";
        Chair = "";
        Position = "";
    }

    public SupervisorModel(int supervisorId, int secondsupervisorId, string supervisorName, string supervisorFirstname,
                            string faculty, string chair, string position) 
    {
        SupervisorId = supervisorId;
        SecondSupervisorId = secondsupervisorId;
        SupervisorName = supervisorName;
        SupervisorFirstname = supervisorFirstname;
        Faculty = faculty;
        Chair = chair;
        Position = position;
    }
}
