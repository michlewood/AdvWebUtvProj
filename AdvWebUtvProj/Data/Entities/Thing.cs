using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdvWebUtvProj.Data.Entities
{
    public class Thing
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string TypeOfThing { get; set; }

        [Required]
        [StringLength(200)]
        public string NameOfThing { get; set; }


        [Required]
        public int PriceOfThing { get; set; }


    }
}