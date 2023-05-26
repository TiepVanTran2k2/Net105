using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.ApplicationUser
{
    public interface IApplicationUserRepository
    {
        Task<bool> UpdateAsync(IdentityUser input);
    }
}
