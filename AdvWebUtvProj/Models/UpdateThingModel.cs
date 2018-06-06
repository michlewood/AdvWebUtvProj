using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvWebUtvProj.Models
{
    public class UpdateThingModel
    {
        public int Id { get; set; }

        public string TypeOfThing { get; set; }
        
        public string NameOfThing { get; set; }
                
        public int PriceOfThing { get; set; }
    }
}
