using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WMKancelariapp.Data;
using WMKancelariapp.Models;

namespace WMKancelariapp.Repository
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        private readonly DbSet<T> _entities;
        private readonly ApplicationDbContext _context;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        public async Task<List<T>> GetAll(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _entities;
            var result = new List<T>();
            if (!includes.Any())
            {
                return await query.ToListAsync();
            }
            foreach (var include in includes)
            {
                result.AddRange(await query.Include(include).ToListAsync());
            }
            result = result.DistinctBy(x => x.Id).ToList();
            return result.ToList();
        }

        public async Task Insert(T entity)
        {
            if (entity != null)
            {
                _entities.Add(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> Delete(T entity)
        {
            _entities.Remove(entity);
            var deleted = await _context.SaveChangesAsync();
            return deleted > 0;
        }

        public async Task Update(T entity)
        {
            _entities.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<T> GetById(string id)
        {
            return await _entities.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<T> GetById(string id, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _entities;
            List<T> result = new();
            foreach (var include in includes)
            {
                result.AddRange(await query.Include(include).ToListAsync());
            }
            result = result.DistinctBy(x => x.Id).ToList();
            return result.FirstOrDefault(x => x.Id == id);
        }
    }
}
