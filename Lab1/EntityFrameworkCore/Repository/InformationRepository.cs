using Domain.Entities.Information;
using Domain.Repository;
using EntityFrameworkCore.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkCore.Repository
{
    public class InformationRepository : RepositoryBase<Information>, IInformationRepository
    {
        public InformationRepository(DbContextApp db) : base(db)
        {
        }
    }
}
