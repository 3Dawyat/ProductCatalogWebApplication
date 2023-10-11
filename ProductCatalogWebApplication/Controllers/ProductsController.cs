namespace ProductCatalogWebApplication.Controllers
{
    [Authorize(Roles = AppRoles.Admin)]
    public class ProductsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IDataBaseServices<Product> _product;
        private readonly IDataBaseServices<Category> _category;
        private readonly IDataProtection _dataProtection;
        public ProductsController(IDataProtection dataProtection, IDataBaseServices<Product> product, IMapper mapper, IDataBaseServices<Category> category)
        {
            _dataProtection = dataProtection;
            _product = product;
            _mapper = mapper;
            _category = category;
        }
        public async Task<IActionResult> Index()
        {
            var data = await _product.GetAllAsync(a => a.Category!, a => a.CreatedBy!, a => a.LastUpdatedBy!);
            var viewModel = _mapper.Map<IEnumerable<ProductViewModel>>(data);
            foreach (var product in viewModel)
                product.Key = _dataProtection.Encode(int.Parse(product.Key!));
            return View(viewModel);
        }
        [HttpGet, ValidateAjaxOnly]
        public async Task<IActionResult> Create()
        {
            return PartialView("_ProductForm", await PopulateViewModel());
        }
        [AllowAnonymous]
        public async Task<IActionResult> Details(string key)
        {
            var productId = _dataProtection.Decode(key);
            var product = await _product.GetObjectByAsync(s => s.Id == productId, includes: a => a.Category!);
            if (product is null)
                return NotFound();

            var viewModel = _mapper.Map<ProductViewModel>(product);
            viewModel.Key = key;
            return View(viewModel);
        }
        [HttpPost, ValidateAntiForgeryToken, ValidateModelState]
        public async Task<IActionResult> Create(ProductFormViewModel model)
        {
            var product = _mapper.Map<Product>(model);
            product.CreatedById = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await _product.AddAsync(product);

            product = await _product.GetObjectByAsync(a => a.Id == product.Id, includes: a => a.Category!);

            var viewModel = _mapper.Map<ProductViewModel>(product);
            viewModel.CreatedBy = User.FindFirstValue(ClaimTypes.GivenName)!;
            viewModel.Key = _dataProtection.Encode(product!.Id);

            return PartialView("_ProductRow", viewModel);
        }
        [HttpGet, ValidateAjaxOnly]
        public async Task<IActionResult> Edit(string key)
        {
            var productId = _dataProtection.Decode(key);
            var product = await _product.FindAsync(productId);
            if (product is null)
                return NotFound();

            var viewModel = _mapper.Map<ProductFormViewModel>(product);
            viewModel.Key = key;

            return PartialView("_ProductForm", await PopulateViewModel(viewModel));
        }
        [HttpPost, ValidateAntiForgeryToken, ValidateModelState]
        public async Task<IActionResult> Edit(ProductFormViewModel model)
        {
            var productId = _dataProtection.Decode(model.Key!);

            var product = await _product.FindAsync(productId);
            if (product is null)
                return NotFound();

            product = _mapper.Map(model, product);
            product.LastUpdatedOn = DateTime.UtcNow.ToLocalTime();
            product.LastUpdatedById = User.FindFirstValue(ClaimTypes.NameIdentifier);

            _product.Edit(product);

            if (!await _product.SaveChanges())
                return BadRequest(Errors.SavingData);

            product = await _product.GetObjectByAsync(a => a.Id == product.Id, false, c => c.Category!, u => u.CreatedBy!);
            var viewModel = _mapper.Map<ProductViewModel>(product);
            viewModel.LastUpdatedBy = User.FindFirstValue(ClaimTypes.GivenName);
            return PartialView("_ProductRow", viewModel);


        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleStatus(string key)
        {
            var productId = _dataProtection.Decode(key);

            var product = await _product.FindAsync(productId);
            if (product is null)
                return NotFound();

            product.LastUpdatedOn = DateTime.UtcNow.ToLocalTime();
            product.IsDeleted = !product.IsDeleted;
            product.LastUpdatedById = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _product.Edit(product);

            if (!await _product.SaveChanges())
                return BadRequest(Errors.SavingData);

            var viewModel = _mapper.Map<ProductViewModel>(product);

            return Ok(new
            {
                LastUpdatedOn = viewModel.LastUpdatedOn!.Value.ToString(),
                LastUpdatedBy = User.FindFirstValue(ClaimTypes.GivenName),
                Status = viewModel.IsDeleted,
                viewModel.Availability
            });
        }

        [HttpGet, ValidateAjaxOnly, AllowAnonymous]
        public async Task<IActionResult> GetByCategoryId(int id)
        {
            IEnumerable<Product> products;
            if (id == 0)
                products = await _product.GetListByAsync(a => a.StartDate.AddDays(a.Duration) > DateTime.Now && a.StartDate <= DateTime.Now && !a.IsDeleted);
            else
                products = await _product.GetListByAsync(a => a.CategoryId == id && (a.StartDate.AddDays(a.Duration) > DateTime.Now && a.StartDate <= DateTime.Now && !a.IsDeleted));
            
            var viewModel = _mapper.Map<IEnumerable<ProductViewModel>>(products);
            foreach (var product in viewModel)
                product.Key = _dataProtection.Encode(int.Parse(product.Key!));

            return Ok(viewModel);
        }
        private async Task<ProductFormViewModel> PopulateViewModel(ProductFormViewModel? model = null)
        {
            ProductFormViewModel viewModel = model is null ? new ProductFormViewModel() : model;
            var categories = await _category.GetAllAsync();
            viewModel.Categories = _mapper.Map<IEnumerable<SelectListItem>>(categories);
            return viewModel;
        }
        public async Task<IActionResult> AllowProduct(ProductFormViewModel model)
        {
            var productId = !string.IsNullOrEmpty(model.Key) ? _dataProtection.Decode(model.Key!) : 0;
            var product = await _product.GetObjectByAsync(a => a.Name == model.Name);
            var viewModel = _mapper.Map<ProductViewModel>(product);
            var isAllowd = product is null || product.Id.Equals(productId) || !product.CategoryId.Equals(model.CategoryId);
            return Json(isAllowd);
        }
    }
}
