namespace ProductCatalogWebApplication.BusinessLogic.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            #region Product
            CreateMap<Product, ProductViewModel>()
           .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy!.FullName))
           .ForMember(dest => dest.LastUpdatedBy, opt => opt.MapFrom(src => src.LastUpdatedBy!.FullName))
           .ForMember(dest => dest.Key, opt => opt.MapFrom(src => src.Id.ToString()))
           .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category!.Name));

            CreateMap<ProductFormViewModel, Product>().ReverseMap();
            #endregion
            #region Category
            CreateMap<Category, SelectListItem>()
                          .ForMember(dist => dist.Value, opt => opt.MapFrom(src => src.Id))
                          .ForMember(dist => dist.Text, opt => opt.MapFrom(src => src.Name));

            CreateMap<Category, CategoryViewModel>();
            #endregion



        }
    }
}
