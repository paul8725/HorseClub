using System.ComponentModel.DataAnnotations;

namespace HorseClub.Models
{
    public class Breeds
    {
        public int Id { get; set; }

        [Display(Name = "Breed")]
        public string Breed { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }
        
        [Display(Name = "Category")]
        public string Category { get; set; }
        
        [Display(Name = "Price")]
        public string Price { get; set; }
    }
}
