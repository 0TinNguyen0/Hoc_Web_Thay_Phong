using SV20T1080053.DomainModels;

namespace SV20T1080053.Web.Models
{
    public class PaginationSearchShipper : PaginationSearchBaseResult
    {
        public IList<Shipper> Data { get; set; }
    }
}
