using Domain.Entities.Lab4;
using EntityFrameworkCore.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkCore.Repository
{
    public class Lab4Repository : RepositoryBase<lab4>, ILab4Repository
    {
        public Lab4Repository(DbContextApp db) : base(db)
        {
        }
    }
}
