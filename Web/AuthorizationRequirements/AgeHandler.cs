using Microsoft.AspNetCore.Authorization;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Web.AuthorizationRequirements
{
    public class AgeHandler : AuthorizationHandler<AgeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            AgeRequirement requirement)
        {
            if (context.User.HasClaim(c => c.Type == ClaimTypes.DateOfBirth))
            {
                var year = 0;
                if (int.TryParse(context.User.FindFirst(c => c.Type == ClaimTypes.DateOfBirth).Value, out year))
                {
                    if ((DateTime.Now.Year - year) >= requirement.Age)
                    {
                        context.Succeed(requirement);
                    }
                }
            }

            return Task.CompletedTask;
        }
    }
}
