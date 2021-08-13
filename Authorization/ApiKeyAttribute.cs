using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Authorization
{
    public class ApiKeyAttribute : Attribute, IAsyncActionFilter
    {
        private const string ApikeyName = "ApiKey";
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if(!context.HttpContext.Request.Headers.TryGetValue(ApikeyName , out var returnedApi))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var config = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();

            var ApiKey = config.GetValue<string>(key:"ApiKey");

            if (!ApiKey.Equals(returnedApi))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            await next();
        }
    }
}
