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
        public async Task<bool> UpdateAsync(IdentityUser input)
        {
            var result = await _userManager.UpdateAsync(input);
            if (result.Succeeded)
            {
                await _dbContextApp.SaveChangesAsync();
            }
            return true;
        }
    }
}
