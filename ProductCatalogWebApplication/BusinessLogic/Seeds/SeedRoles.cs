namespace ProductCatalogWebApplication.BusinessLogic.Seeds
{
    public class SeedRoles
    {
        public static async Task SeedAsync(RoleManager<IdentityRole> _roleManager)
        {
            if (!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new IdentityRole(AppRoles.Admin));
                await _roleManager.CreateAsync(new IdentityRole(AppRoles.User));
            }
        }
    }
}
