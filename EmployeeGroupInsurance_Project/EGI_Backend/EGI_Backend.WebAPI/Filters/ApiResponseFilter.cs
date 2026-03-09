using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using EGI_Backend.WebAPI.Models;

namespace EGI_Backend.WebAPI.Filters
{
    public class ApiResponseFilter : IAsyncResultFilter
    {
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (context.Result is ObjectResult objectResult && objectResult.Value is not null)
            {
                // If it's already an ApiResponse, or an ErrorResponse (handled by Exception Middleware), don't wrap it again
                if (objectResult.Value is ErrorResponse || objectResult.Value.GetType().IsGenericType && objectResult.Value.GetType().GetGenericTypeDefinition() == typeof(ApiResponse<>))
                {
                    await next();
                    return;
                }

                // Wrap the successful result
                var resultType = objectResult.Value.GetType();
                var wrapperType = typeof(ApiResponse<>).MakeGenericType(resultType);

                var wrappedResult = Activator.CreateInstance(wrapperType, true, "Operation successful", objectResult.Value, null);

                objectResult.Value = wrappedResult;
            }
            else if (context.Result is OkResult || context.Result is EmptyResult)
            {
                context.Result = new ObjectResult(new ApiResponse<object>(true, "Operation successful", null, null));
            }

            await next();
        }
    }
}
