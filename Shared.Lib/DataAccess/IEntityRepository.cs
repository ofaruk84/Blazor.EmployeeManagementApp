using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Lib.DataAccess
{
    public interface IEntityRepository<T>
    {

        Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter);

        Task<T?> GetAsync(Expression<Func<T, bool>>? filter);

        Task AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);

    }
}
