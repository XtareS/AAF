using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AAF.Data.Entities
{
   public interface IEntities<T> where T : class
    {

   

        IQueryable<T> GetAll();

        Task<T> GetByAsync(int id);

        Task<T> CreateAsync(T entity);

        Task<T> UpdateAsync(T entity);

        Task<T> DeleteAsync(T entity);

        Task<bool> ExistsAsync(int id);


    }
}
