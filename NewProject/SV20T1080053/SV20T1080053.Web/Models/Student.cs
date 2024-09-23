namespace SV20T1080053.Web.Models
{
    public class Student
    {

        public String StudentId { get; set; }

        public String StudentName { get; set;}
    }

    public class StudentDAL
    {
        public List<Student> List()
        {
            List<Student> students = new List<Student>();

            students.Add(new Student()
            {
                StudentId = "20T1080053",
                StudentName = "Nguyễn Quốc Trung"
            });
            students.Add(new Student()
            {
                StudentId = "20T1080056",
                StudentName = "Hữu Trí"
            });
            students.Add(new Student()
            {
                StudentId = "20T1080051",
                StudentName = "Duy Quyền"
            });

            return students;
        }
    }
}
