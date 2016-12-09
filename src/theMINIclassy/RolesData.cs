using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using theMINIclassy.Models;

namespace theMINIclassy
{
    public static class RolesData
    {
        public static async Task CreateRolesAndUsers(IServiceProvider services)
        {
            //ApplicationDbContext db = new ApplicationDbContext();

            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            //var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            //var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));


            // In Startup iam creating first Admin Role and creating a default Admin User    
            if (await roleManager.RoleExistsAsync("Admin") != true)
            {

                // first we create Admin role   
                var role = new Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole();
                role.Name = "Admin";
                await roleManager.CreateAsync(role);

                //Here we create a Admin super user who will maintain the website                  

                var user = new ApplicationUser { UserName = "the@mini.classy", Email = "the@mini.classy" };
                string userPWD = "Mini123classy!";

                var chkUser = UserManager.CreateAsync(user, userPWD);

                //Add default User to Role Admin   
                if (chkUser.Result.Succeeded)
                {
                    await UserManager.AddToRoleAsync(user, "Admin");

                }
            }

            // creating Creating Manager role    
            if (await roleManager.RoleExistsAsync("Manager") != true)
            {
                var role = new Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole();
                role.Name = "Manager";
                await roleManager.CreateAsync(role);

            }

            // creating Creating Employee role    
            if (await roleManager.RoleExistsAsync("Employee") != true)
            {
                var role = new Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole();
                role.Name = "Employee";
                await roleManager.CreateAsync(role);

            }
        }
    }
}
