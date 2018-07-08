using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Constraints
{
    public class UrlConstraint : IRouteConstraint
    {
        private string _url;

        public UrlConstraint(string url)
        {
            _url = url;
        }

        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            return !httpContext.Request.Path.Value.EndsWith(_url);
        }
    }
}
