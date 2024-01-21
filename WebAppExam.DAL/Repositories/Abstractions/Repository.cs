using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppExam.Core.Entities.Commons;
using WebAppExam.DAL.Context;
using WebAppExam.DAL.Repositories.Interfaces;

namespace WebAppExam.DAL.Repositories.Abstractions
{
    public class Repository<T> : IRepository<T> where T : BaseAuditableEntity, new()
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _table;

        public Repository(AppDbContext context)
        {
            _context = context;
            _table = _context.Set<T>();
        }

        public async Task<T> CreateAsync(T entity)
        {
            await _table.AddAsync(entity);

            return entity;
        }

        public async Task<T> DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);

            entity.IsDeleted = true;
            await UpdateAsync(entity);

            return entity;  
        }

        public async Task<IQueryable<T>> GetAllAsync()
        {
            IQueryable<T> query = _table;

            return query;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _table.Where(x => x.Id == id).FirstOrDefaultAsync();    
        }

        public async Task<T> RecoverAsync(int id)
        {
            var entity = await GetByIdAsync(id);

            entity.IsDeleted = false;
            await UpdateAsync(entity);

            return entity;
        }

        public async Task RemoveAsync(int id)
        {
            var entity = await GetByIdAsync(id);

            _table.Remove(entity);  
        }

        public async Task<int> SaveChangesAsync()
        {
            var result = await _context.SaveChangesAsync();

            return result;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _table.Update(entity);

            return entity;
        }
    }
}
