using System;
using System.Collections.Generic;
using System.Text;

namespace EJ.Entities.Models
{
    public class AbsenceNotification : EntityBase
    {
        public int AbsenceId { get; set; }
        public virtual Absence Absence { get; set; }

        public DateTime SendDate { get; set; }
    }
}
