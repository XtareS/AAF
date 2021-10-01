using AAF.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AAF.Data
{
    public class CraftRepository : GenericRepository<Craft>, ICraftRepository
    {
        public CraftRepository(DataContext context) : base(context)
        {

        }

    }
}