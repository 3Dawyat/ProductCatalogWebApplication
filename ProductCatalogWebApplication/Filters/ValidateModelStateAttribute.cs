

namespace ProductCatalogWebApplication.Filters
{
    public class ValidateModelStateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var error in context.ModelState.Values)
                {
                    foreach (var field in error.Errors)
                    {
                        sb.AppendLine(field.ErrorMessage);
                    }
                }
                var result = new ObjectResult(sb.ToString())
                {
                    StatusCode = 400
                };
                context.Result = result;
            }
            base.OnActionExecuting(context);
        }
    }
}
