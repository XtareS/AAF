using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace AAF.Data.Entities
{
    public class Entities<T> : IEntities<T> where T : class, IEntitiesId
    {

        private readonly DataContext context;

        public int Id => throw new NotImplementedException();

        public Entities(DataContext context)
        {
            this.context = context;
        }

        public IQueryable<T> GetAll()
        {
            return this.context.Set<T>().AsNoTracking();
        }


        public async Task<T> GetByAsync(int id)
        {
            return await this.context.Set<T>()
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);
        }


        public async Task CreateAsync(T entity)
        {
            await this.context.Set<T>().AddAsync(entity);
            await SaveAllAsync();
        }

   

        public Task<T> UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }


        public Task<T> DeleteAsync(T entity)
        {
            throw new NotImplementedException();
        }


        public Task<bool> ExistsAsync(int id)
        {
            throw new NotImplementedException();
        }

        private Task SaveAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
