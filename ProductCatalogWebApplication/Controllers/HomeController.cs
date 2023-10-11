



namespace ProductCatalogWebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDataBaseServices<Category> _category;
        private readonly IMapper _mapper;

        public HomeController(IDataBaseServices<Category> category, IMapper mapper)
        {
            _category = category;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {

            var data = await _category.GetAllAsync();
            var viewModel = _mapper.Map<IEnumerable<CategoryViewModel>>(data);
            return View(viewModel);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int statusCode = 500)
        {
            return View(
                new ErrorViewModel
                {
                    ErrorCode = statusCode,
                    ErrorDescription = ReasonPhrases.GetReasonPhrase(statusCode)
                });
        }
    }
}