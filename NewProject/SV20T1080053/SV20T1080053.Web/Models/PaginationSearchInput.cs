namespace SV20T1080053.Web.Models
{
    /// <summary>
    /// Thông tin đầu vào
    /// </summary>
    public class PaginationSearchInput
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public string SearchValue { get; set; } = "";

        public int CategoryID { get; set; } 
        public int SupplierID { get; set; } 
    }
}