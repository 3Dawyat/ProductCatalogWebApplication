namespace ProductCatalogWebApplication.BusinessLogic.Seeds
{
    public static class SeedUsers
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> _userManager)
        {
            var adminEmail = "admin@ProductCatalog.com";
            var userEmail = "user@ProductCatalog.com";

            var existingUsers = await _userManager.Users.ToListAsync();

            if (!existingUsers.Any(u => u.Email!.Equals(adminEmail)))
            {
                var newAdmin = new ApplicationUser
                {
                    UserName = "admin",
                    Email = adminEmail,
                    FullName = "Admin",
                    PhoneNumber = "01095358487",
                    LockoutEnabled = false,
                    EmailConfirmed = true
                };

                await _userManager.CreateAsync(newAdmin, "P@ssword123");
                await _userManager.AddToRolesAsync(newAdmin, new[] { AppRoles.Admin, AppRoles.User });
            }
            if (!existingUsers.Any(u => u.Email!.Equals(userEmail)))
            {
                var newUser = new ApplicationUser
                {
                    UserName = "user",
                    Email = userEmail,
                    FullName = "User",
                    PhoneNumber = "01095358487",
                    LockoutEnabled = false,
                    EmailConfirmed = true
                };
                await _userManager.CreateAsync(newUser, "P@ssword123");
                await _userManager.AddToRoleAsync(newUser, AppRoles.User);
            }
        }

    }
}
