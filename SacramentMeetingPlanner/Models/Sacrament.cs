using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SacramentMeetingPlanner.Models
{
    public class Sacrament
    {

        public int ID               { get; set; }

        [Required]
        [StringLength(50)]
        public string Conducting    { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Hymn")]
        public string HymnName      { get; set; }

        [Column("HymnNumber")]
        [Display(Name= "Number")]
        public int HymnNumber       { get; set; }

        [Required]
        [StringLength(50)]
        public string Prayers       { get; set; }

        [Required]
        [StringLength(50)]
        public string Speakers      { get; set; }

        [Required]
        [StringLength(50)]
        public string Subjects      { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date")]
        public DateTime MeetingDate { get; set; }
    }
}
