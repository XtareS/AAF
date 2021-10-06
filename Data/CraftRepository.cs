using AAF.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AAF.Data
{
    public class CraftRepository : GenericRepository<Craft>, ICraftRepository
    {

        private readonly DataContext context;

        public CraftRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public IQueryable GetAllWithUser()
        {
            return this.context.Crafts.Include(p => p.User);
        }
    }
}