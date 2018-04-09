using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SacramentMeetingPlanner.Models
{
    public class Hymns
    {
        public int HymnsID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Opening Hymn")]
        public string OpeningHymn { get; set; }
        
        [Display(Name = "")]
        public int OpeningHyNumber { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Sacrament Hymn")]
        public string SacramentHymn { get; set; }
        
        [Display(Name = "")]
        public int SacrHyNumber { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Closing Hymn")]
        public string ClosingHymn { get; set; }
   
        [Display(Name = "")]
        public int ClosingNumber { get; set; }

    }
}
