using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SacramentMeetingPlanner.Models
{
    public class Speakers
    {
        public int SpeakersID { get; set; }

        [Required]
        [StringLength(50)]
        public string Speaker1 { get; set; }

        [Required]
        [StringLength(50)]
        public string Speaker2 { get; set; }

        [Required]
        [StringLength(50)]
        public string Speaker3 { get; set; }

    }
}
