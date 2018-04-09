using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SacramentMeetingPlanner.Models
{
    public class Prayers
    {

        public int PrayersID { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Opening Prayers")]
        public string OpeningPrayers { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Closing Prayers")]
        public string ClosingPrayers { get; set; }
    }
}
