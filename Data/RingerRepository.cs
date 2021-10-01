using AAF.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AAF.Data
{
    public class RingerRepository : GenericRepository<Ringer>, IRingerRepository
    {
        public RingerRepository(DataContext context) : base(context)
        {

        }

    }
}
