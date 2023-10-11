namespace ProductCatalogWebApplication.ExtensionMethods
{
    public static class ConfigureSeedsData
    {
        public async static Task<WebApplication> SeedsData(this WebApplication app)
        {
            var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
            using var scope = scopeFactory.CreateScope();

            var _roleManger = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var _userManger = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var _category = scope.ServiceProvider.GetRequiredService<IDataBaseServices<Category>>();

            await SeedRoles.SeedAsync(_roleManger);
            await SeedUsers.SeedAsync(_userManger);
            await SeedCategories.SeedAsync(_category);

            return app;
        }
    }
}
