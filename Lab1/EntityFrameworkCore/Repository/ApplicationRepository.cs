using Domain.Entities.ApplicationUser;
using EntityFrameworkCore.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkCore.Repository
{
    public class ApplicationRepository : IApplicationUserRepository
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly DbContextApp _dbContextApp;
        public ApplicationRepository(UserManager<IdentityUser> userManager,
                                     DbContextApp dbContextApp)
        {
            _userManager = userManager;
            _dbContextApp = dbContextApp;
        }

        public IQueryable<ApplicationUser> GetList()
        {
            return _dbContextApp.ApplicationUser;
        }

        public bool Update(ApplicationUser input, string role)
        {
            var user = _dbContextApp.ApplicationUser.Update(input);
            var roleId = (_dbContextApp.Roles.Where(x => x.Name == role).First()).Id;
            var userRole = _dbContextApp.UserRoles.First(x => x.UserId == input.Id);
            _dbContextApp.UserRoles.Remove(userRole);
            _dbContextApp.SaveChanges();

            _dbContextApp.UserRoles.Add(new IdentityUserRole<string> { RoleId = roleId, UserId = input.Id});
            _dbContextApp.SaveChanges();

            return true;
        }
    }
}
