

namespace SCERP.Domain.Common
{
    public class PaginationQuery
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 12;
        public string? SortBy { get; set; }
        public bool SortDescending { get; set; }
    }

    public class ProductQuery : PaginationQuery
    {
        public string? SearchTerm { get; set; }
        public int? CategoryId { get; set; }
        public string? Brand { get; set; }
        public string? VehicleModel { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public bool? InStockOnly { get; set; }
    }

  
}
