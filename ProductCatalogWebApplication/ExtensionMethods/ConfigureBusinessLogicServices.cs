namespace ProductCatalogWebApplication.ExtensionMethods
{
    public static class ConfigureBusinessLogicServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            services.AddScoped(typeof(IDataBaseServices<>), typeof(ClsDataBaseServices<>));
            services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, ApplicationUserClaimsPrincipalFactory>();

            services.AddSingleton<IHashids>(_ => new Hashids(minHashLength: 12, alphabet: "ABCDEFGHIJKLMNOPQRSTUVWXYZ"));
            services.AddSingleton<IDataProtection, ClsDataProtection>();

            services.AddExpressiveAnnotations();
            services.AddAutoMapper(typeof(MappingProfile));
            return services;
        }
    }
}
