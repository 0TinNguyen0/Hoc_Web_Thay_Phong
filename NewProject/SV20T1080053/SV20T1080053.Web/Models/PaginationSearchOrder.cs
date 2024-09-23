using SV20T1080053.DomainModels;
using SV20T1080053.Web.Models;

namespace SV20T1080053.Web.Models
{
    public class PaginationSearchOrder : PaginationSearchBaseResult
    {
        public List<Order> Data { get; set; }
    }
}