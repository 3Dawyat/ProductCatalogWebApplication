namespace ProductCatalogWebApplication.Middlewares
{
    public class DenyRunMyAppInFrame
    {
        private readonly RequestDelegate _next;
        public DenyRunMyAppInFrame(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            context.Response.Headers.Add("X-Frame-Options", "Deny");
            await _next(context);
        }
    }
}
