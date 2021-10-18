using System.ComponentModel.DataAnnotations;

namespace HorseClub.Models
{
    public class YourHorses
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

        [Display(Name = "UserId")]
        public int UserId { get; set; }
    }
}
