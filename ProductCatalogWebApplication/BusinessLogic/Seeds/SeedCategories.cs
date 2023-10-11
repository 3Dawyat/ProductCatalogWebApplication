namespace ProductCatalogWebApplication.BusinessLogic.Seeds
{
    public static class SeedCategories
    {
        public static async Task SeedAsync(IDataBaseServices<Category> _category)
        {
            var categories = await _category.GetAllAsync();
            if (!categories.Any())
            {
                var newCategories = new List<Category>{
                new Category{Name="Dairy"},
                new Category{Name="Cream"},
                new Category{Name="Cheese"},
                new Category{Name="Egg"},
                new Category{Name="Ghee"}};

                await _category.AddRangeAsync(newCategories);
            }
        }
    }
}
