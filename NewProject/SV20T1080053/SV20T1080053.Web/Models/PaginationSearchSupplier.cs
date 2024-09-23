using SV20T1080053.DomainModels;

namespace SV20T1080053.Web.Models
{
    public class PaginationSearchSupplier : PaginationSearchBaseResult
    {
        public IList<Supplier> Data { get; set; }
    }
}
