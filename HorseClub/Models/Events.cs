using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HorseClub.Models
{
    public class Events
    {

        public int Id { get; set; }


        [Display(Name = "Name")]
        public string Name { get; set; }


        [Display(Name = "EventGroup")]
        public string EventGroup { get; set; }


        [Display(Name = "Time")]
        public string Time { get; set; }
        

        [Display(Name = "Location")]
        public string Location { get; set; }


        [Display(Name = "Message")]
        public string Message { get; set; }

    }
}
