using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace SacramentAgenda.Models
{
    public class Speaker
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SpeakerID { get; set; }
        public string SpeakerName { get; set; }
        public string Subject { get; set; }

        public ICollection<Assignment> Assignments { get; set; }
    }
}
