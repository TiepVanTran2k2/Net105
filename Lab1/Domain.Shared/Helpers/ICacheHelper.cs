using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Shared.Helpers
{
    public interface ICacheHelper
    {
        TEntity GetAsync<TEntity>(string key) where TEntity : class;
        bool CreateAsync<TEntity>(TEntity entity, string key) where TEntity : class;
    }
}
