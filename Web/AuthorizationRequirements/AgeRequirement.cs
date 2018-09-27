using Microsoft.AspNetCore.Authorization;

namespace Web.AuthorizationRequirements
{
    public class AgeRequirement : IAuthorizationRequirement
    {
        protected internal int Age { get; set; }

        public AgeRequirement(int age)
        {
            Age = age;
        }
    }
}
