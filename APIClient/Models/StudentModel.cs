namespace WebMVC.Models
{
    public class StudentModel
    {
        public int StudentId { get; set; }  
        public string StudentName { get; set;}
        public string StudentFirstname { get; set;}
        public string Faculty {  get; set;}
        public StudentModel()
        {
            StudentId = 0;
            StudentName = "";
            StudentFirstname = "";
            Faculty = "";
        }
    }
}
