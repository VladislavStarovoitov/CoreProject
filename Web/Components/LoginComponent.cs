using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Components
{
    public class LoginComponent : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(bool isSignedIn)
        {
            if (isSignedIn)
            {
                return Task.FromResult<IViewComponentResult>(View("_SignedIn"));
            }
            else
            {
                return Task.FromResult<IViewComponentResult>(View("_NotSignedIn"));
            }
        }
    }
}
