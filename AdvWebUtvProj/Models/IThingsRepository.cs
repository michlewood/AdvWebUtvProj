using AdvWebUtvProj.Data;
using AdvWebUtvProj.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvWebUtvProj.Models
{
    public interface IThingsRepository
    {
        void SeedRepo();
        void ClearAll();
        IEnumerable<Thing> GetAll();
        Thing GetById(int id);
        void Add(Thing thing);
        void Update(UpdateThingModel thing);
        void Remove(int id);
        bool ThingExists(int id);
        int Count();
    }
}
