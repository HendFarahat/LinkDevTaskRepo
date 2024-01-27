
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Student.Models;
using Student.Utility;
using Students.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Students.DataAccess.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _db;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        public DbInitializer(ApplicationDbContext db, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async void Initialize()
        {
  
            //
            if (!_roleManager.RoleExistsAsync(StaticDetails.Role_Admin).GetAwaiter().GetResult())
            {
                // Create admin and student roles if they don't exist
                _roleManager.CreateAsync(new IdentityRole(StaticDetails.Role_Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(StaticDetails.Role_Student)).GetAwaiter().GetResult();

                // Create admin user
                var adminUser = new IdentityUser
                {
                    UserName = "admin@gmail.com",
                    Email = "MasterAdmin@gmail.com",
                };

                var result = _userManager.CreateAsync(adminUser, "Admin@123").GetAwaiter().GetResult();

                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(adminUser, StaticDetails.Role_Admin).GetAwaiter().GetResult();
                }
               
            }

            var ClassList=_db.Classes.ToList();
            if (ClassList==null || ClassList.Count==0)
            {
           
                List<Class> ClassNames = new List<Class>() { new Class {Name= "OS" }, new Class { Name = "SD" }, new Class { Name = "Motion Graphic" } };
                _db.Classes.AddRange(ClassNames);
                _db.SaveChanges();
            }
           









        }
    }
}
