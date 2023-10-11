namespace ProductCatalogWebApplication.Models.ViewModel
{
    public class ProductViewModel
    {
        public string? Key { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public bool IsDeleted { get; set; }
        public string CategoryName { get; set; } = null!;
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedOn { get; set; }
        public string? LastUpdatedBy { get; set; } = null!;
        public DateTime? LastUpdatedOn { get; set; }
        public DateTime StartDate { get; set; }
        public int Duration { get; set; }
        public bool Availability => (StartDate.AddDays(Duration) > DateTime.Now && StartDate <=DateTime.Now&& !IsDeleted) ? true : false;
    }
}
