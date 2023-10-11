using UoN.ExpressiveAnnotations.NetCore.Attributes;

namespace ProductCatalogWebApplication.Models.FormViewModel
{
    public class ProductFormViewModel
    {

        public string? Key { get; set; }
        [MaxLength(100, ErrorMessage = Errors.MaxLength)]
        [Remote("AllowProduct", null!, AdditionalFields = "Key,CategoryId", ErrorMessage = Errors.Duplicated)]
        public string Name { get; set; } = null!;

        [Display(Name = "Publishing Date")]
        [AssertThat("StartDate > Today()", ErrorMessage = Errors.NotAllowOldDate)]
        public DateTime StartDate { get; set; } = DateTime.Now;

        [Range(1, int.MaxValue, ErrorMessage = Errors.MustGreaterThan)]
        public int Duration { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = Errors.MustGreaterThan)]
        public decimal Price { get; set; }

        [Display(Name = "Category")]
        [Remote("AllowProduct", null!, AdditionalFields = "Key,Name", ErrorMessage = Errors.Duplicated)]

        public int CategoryId { get; set; }
        public IEnumerable<SelectListItem>? Categories { get; set; }
    }
}
