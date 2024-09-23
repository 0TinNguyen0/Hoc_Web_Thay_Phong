using SV20T1080053.DomainModels;

namespace SV20T1080053.Web.Models
{
    public class PaginationSearchEmployee : PaginationSearchBaseResult
    {
        public IList<Employee> Data { get; set; }
    }
}
