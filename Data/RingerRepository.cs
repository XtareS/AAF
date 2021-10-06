using AAF.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AAF.Data
{
    public class RingerRepository : GenericRepository<Ringer>, IRingerRepository
    {
        private readonly DataContext context;

        public RingerRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public IQueryable GetAllWithUser()
        {
            return this.context.Ringers.Include(p => p.User);
        }
    }
}
