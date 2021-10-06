using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AAF.Data.Entities
{
    public class TexteiRepository : GenericRepository<Textei>,ITexteiRepository
    {
        private readonly DataContext context;

        public TexteiRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public IQueryable GetAllWithUser()
        {
            return this.context.Texteis.Include(p => p.User);
        }
    }
}
