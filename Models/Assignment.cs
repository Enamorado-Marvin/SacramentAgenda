using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SacramentAgenda.Models
{
    public class Assignment
    {
        public int AssignmentID { get; set; }
        public int AgendaID { get; set; }
        public int SpeakerID { get; set; }

        public Agenda Agenda { get; set; }
        public Speaker Speaker { get; set; }
    }
}
