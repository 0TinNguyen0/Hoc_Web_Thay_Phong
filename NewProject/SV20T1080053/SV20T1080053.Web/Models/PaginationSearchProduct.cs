using SV20T1080053.DomainModels;
using SV20T1080053.Web.Models;

namespace SV20T1080053.Web.Models
{
    public class PaginationSearchProduct : PaginationSearchBaseResult
    {
        public IList<Product> Data { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int SupplierId { get; set; }
    }
}