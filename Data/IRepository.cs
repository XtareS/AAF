using AAF.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AAF.Data
{
    public interface IRepository
    {
        void AddCraft(Craft craft);

        void AddRinger(Ringer ringer);

        void AddTextei(Textei textei);

        bool CraftExists(int id);

        Craft GetCraft(int id);

        IEnumerable<Craft> GetCrafts();

        Ringer GetRinger(int id);

        IEnumerable<Ringer> GetRingers();

        Textei GetTextei(int id);

        IEnumerable<Textei> GetTexteis();

        void RemoveCraft(Craft craft);

        void RemoveRinger(Ringer ringer);

        void RemoveTextei(Textei textei);

        bool RingerExists(int id);

        Task<bool> SaveAllAsync();

        bool TexteiExists(int id);

        void UpdateCraft(Craft craft);

        void UpdateRinger(Ringer ringer);

        void UpdateTextei(Textei textei);
    }
}