using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppExam.Core.Entities.Commons;

namespace WebAppExam.DAL.Repositories.Interfaces
{
    public interface IRepository<T> where T : BaseAuditableEntity, new()
    {
        Task<IQueryable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> DeleteAsync(int id);
        Task<T> RecoverAsync(int id);
        Task RemoveAsync(int id);
        Task<int> SaveChangesAsync();
    }
}
