namespace SV20T1080053.Web.Models
{
    public class PersonDAL
    {
        public List<Person> List()
        {
            List<Person> list = new List<Person>(); //ten bien: camelCase, fistName --- PascalCase

            list.Add(new Person()
            {
                PersonId = 1,
                Name =  "Nguyễn Quốc Trung",
                Address = "27 Ngự Bình",
                Email = "trunga92loctri@gmail.com"
            });

            list.Add(new Person()
            {
                PersonId = 2,
                Name = "Trần Thái",
                Address = "34 Nguyễn Huệ",
                Email = "tranthai@gmail.com"
            });

            list.Add(new Person()
            {
                PersonId = 3,
                Name = "Mai Loan",
                Address = "56 Trần Phú",
                Email = "mailoan@gmail.com"
            });
            return list;
        }
    }
}
