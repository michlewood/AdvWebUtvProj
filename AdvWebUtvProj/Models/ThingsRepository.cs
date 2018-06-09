using AdvWebUtvProj.Data;
using AdvWebUtvProj.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvWebUtvProj.Models
{
    public class ThingsRepository : IThingsRepository
    {
        public ApplicationDbContext context;

        public ThingsRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Add(Thing thing)
        {
            context.Add(thing);
            context.SaveChanges();
        }

        public void ClearAll()
        {
            foreach (var thing in GetAll())
            {
                context.Remove(thing);
            }
            context.SaveChanges();
        }

        public int Count()
        {
            return context.Things.ToList().Count;
        }

        public IEnumerable<Thing> GetAll()
        {
            return context.Things;
        }

        public Thing GetById(int id)
        {
            return context.Things.Single(t => t.Id == id);
        }

        public bool ThingExists(int id)
        {
            return context.Things.ToList().Exists(t => t.Id == id);
        }

        public void Remove(int id)
        {
            context.Things.Remove(GetById(id));
            context.SaveChanges();
        }

        public void SeedRepo()
        {
            ClearAll();

            context.Things.Add(new Thing
            {
                //Id = 1,
                NameOfThing = "thing1",
                TypeOfThing = "thingType1",
                PriceOfThing = 10
            });
            context.Things.Add(new Thing
            {
                //Id = 2,
                NameOfThing = "thing2",
                TypeOfThing = "thingType2",
                PriceOfThing = 50
            });


            context.SaveChanges();
        }

        public void Update(UpdateThingModel thing)
        {
            var thingToUpdate = GetById(thing.Id);
            thingToUpdate.NameOfThing = thing.NameOfThing;
            thingToUpdate.TypeOfThing = thing.TypeOfThing;
            thingToUpdate.PriceOfThing = thing.PriceOfThing;
            context.SaveChanges();
        }
    }
}
