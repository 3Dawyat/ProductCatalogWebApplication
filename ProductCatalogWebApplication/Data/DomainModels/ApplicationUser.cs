namespace ProductCatalogWebApplication.Data.DomainModels
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(100)]
        public string FullName { get; set; } = null!;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
    }
}
