using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace products.Domain.Shared;
public class ValidationFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.ModelState.IsValid)
        {
            var errorsInModelState = context.ModelState.
                                        Where(x => x.Value.Errors.Count > 0)
                                            .ToDictionary(x => x.Key, x => x.Value.Errors.Select(x => x.ErrorMessage))
                                                .ToArray();

            var errorResponse = new ErrorResponse();
            foreach (var error in errorsInModelState)
            {
                foreach (var subError in error.Value)
                {
                    var errorModel = new ErrorModel
                    {
                        FieldName = error.Key,
                        Message = subError
                    };
                    errorResponse.Errors.Add(errorModel);
                }
            }
            context.Result = new BadRequestObjectResult(errorResponse);
            return;
        }
    }
}