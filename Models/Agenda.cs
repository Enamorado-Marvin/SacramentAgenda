using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SacramentAgenda.Models
{
    public class Agenda
    {        
        public int ID { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Meeting Date")]
        public DateTime SacramentMeetingDate { get; set; }
        [Required]
        [StringLength(100)]
        [Display(Name = "Presiding Leader")]
        public string PresidingLeader { get; set; }
        [Required]
        [StringLength(100)]
        [Display(Name = "Conducting Leader")]
        public string ConductingLeader { get; set; }
        [StringLength(200)]
        [Required]
        [Display(Name = "Opening Song")]
        public string OpeningSong { get; set; }
        [StringLength(100)]
        [Required]
        [Display(Name = "Opening Prayer")]
        public string Invocation { get; set; }
        [Display(Name = "Special Number (Optional)")]
        public string SpecialNumber { get; set; }
        
        [StringLength(200)]
        [Required]
        [Display(Name = "Closing Song")]
        public string ClosingSong { get; set; }
        
        [StringLength(100)]
        [Required]
        [Display(Name = "Closing Prayer")]
        public string Benediction { get; set; }
        public ICollection<Assignment> Assignments { get; set; }        
    }
}
