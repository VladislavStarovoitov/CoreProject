using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Web.Data
{
    public class RolesInitializer
    {
        public static async Task Initialize(RoleManager<IdentityRole> rolesManager)
        {
            if (!await rolesManager.RoleExistsAsync("Admin"))
            {
                await rolesManager.CreateAsync(new IdentityRole("Admin"));
            }

            if (!await rolesManager.RoleExistsAsync("User"))
            {
                await rolesManager.CreateAsync(new IdentityRole("User"));
            }

            if (!await rolesManager.RoleExistsAsync("God"))
            {
                await rolesManager.CreateAsync(new IdentityRole("God"));
            }
        }
    }
}
