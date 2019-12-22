using System;
using System.ComponentModel.DataAnnotations;

namespace SacramentAgenda.Models.UnitViewModels
{
    public class AssignmentDateGroup
    {
        [DataType(DataType.Date)]
        public DateTime? AssignmentDate { get; set; }

        public int AgendaCount { get; set; }
    }
}
