using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class HelperService : IHelperService
    {
        public List<string> GetListNameFieldAsync<TEntity>()
        {
            var result = new List<string>();
            Type fieldsType = typeof(TEntity);

            PropertyInfo[] props = fieldsType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            for (int i = 0; i < props.Length; i++)
            {
                result.Add(props[i].Name);
            }
            return result.OrderBy(x => x.Length).ToList();
        }
    }
}
