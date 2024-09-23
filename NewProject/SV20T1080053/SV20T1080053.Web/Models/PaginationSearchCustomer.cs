using SV20T1080053.DomainModels;
using SV20T1080053.Web.Models;

namespace SV20T1080053.Web.Models
{
    public class PaginationSearchCustomer : PaginationSearchBaseResult
    {
        public IList<Customer> Data { get; set; }
    }
}
