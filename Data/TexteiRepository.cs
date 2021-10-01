using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AAF.Data.Entities
{
    public class TexteiRepository : GenericRepository<Textei>,ITexteiRepository
    {
        public TexteiRepository(DataContext context) : base(context)
        {

        }

    }
}
