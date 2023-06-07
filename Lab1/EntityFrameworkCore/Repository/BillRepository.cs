using Domain.Entities.Bill;
using EntityFrameworkCore.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkCore.Repository
{
    public class BillRepository : RepositoryBase<Bill>, IBillRepository
    {
        public BillRepository(DbContextApp db) : base(db)
        {
        }
    }
    public class BillDetailRepository : RepositoryBase<DetailBill>, IBillDetailRepository
    {
        public BillDetailRepository(DbContextApp db) : base(db)
        {
        }
    }
}
