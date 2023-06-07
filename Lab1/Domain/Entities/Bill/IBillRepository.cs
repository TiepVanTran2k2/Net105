using Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Bill
{
    public interface IBillRepository : IRepositoryBase<Bill>
    {
    }
    public interface IBillDetailRepository : IRepositoryBase<DetailBill>
    {
    }
}
