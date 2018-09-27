using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Text.RegularExpressions;

namespace Web.Filters
{
    public class IEFilterAttribute : Attribute, IResourceFilter
    {
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            string userAgent = context.HttpContext.Request.Headers["User-Agent"].ToString();
            if (userAgent.IndexOf("MSIE", StringComparison.InvariantCultureIgnoreCase) >= 0)
            {
                context.Result = new ContentResult { Content = "Sorry, but your browser is IE(:" };
            }
        }
    }
}
