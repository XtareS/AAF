using AAF.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AAF.Data
{
    public class Repository : IRepository
    {
        private readonly DataContext context;


        public Repository(DataContext context)
        {
            this.context = context;
        }




        // Refe à base de dados de Texteis

        public IEnumerable<Textei> GetTexteis()
        {
            return this.context.Texteis.OrderBy(p => p.Name);
        }

        public Textei GetTextei(int id)
        {
            return this.context.Texteis.Find(id);
        }

        public void AddTextei(Textei textei)
        {
            this.context.Texteis.Add(textei);
        }

        public void UpdateTextei(Textei textei)
        {
            this.context.Texteis.Update(textei);
        }

        public void RemoveTextei(Textei textei)
        {
            this.context.Texteis.Remove(textei);
        }

        public bool TexteiExists(int id)
        {
            return this.context.Texteis.Any(p => p.Id == id);
        }



        // Ref à base de dados de Artesanato

        public IEnumerable<Craft> GetCrafts()
        {
            return this.context.Crafts.OrderBy(p => p.Name);
        }

        public Craft GetCraft(int id)
        {
            return this.context.Crafts.Find(id);
        }

        public void AddCraft(Craft craft)
        {
            this.context.Crafts.Add(craft);
        }

        public void UpdateCraft(Craft craft)
        {
            this.context.Crafts.Update(craft);
        }

        public void RemoveCraft(Craft craft)
        {
            this.context.Crafts.Remove(craft);
        }

        public bool CraftExists(int id)
        {
            return this.context.Crafts.Any(p => p.Id == id);
        }



        // Ref à base de Dados de acessórios

        public IEnumerable<Ringer> GetRingers()
        {
            return this.context.Ringers.OrderBy(p => p.Name);
        }

        public Ringer GetRinger(int id)
        {
            return this.context.Ringers.Find(id);
        }

        public void AddRinger(Ringer ringer)
        {
            this.context.Ringers.Add(ringer);
        }

        public void UpdateRinger(Ringer ringer)
        {
            this.context.Ringers.Update(ringer);
        }

        public void RemoveRinger(Ringer ringer)
        {
            this.context.Ringers.Remove(ringer);
        }
        public bool RingerExists(int id)
        {
            return this.context.Ringers.Any(p => p.Id == id);
        }


        //generalic Codeling -- para salvar tudo 

        public async Task<bool> SaveAllAsync()
        {
            return await this.context.SaveChangesAsync() > 0;
        }



    }


}
