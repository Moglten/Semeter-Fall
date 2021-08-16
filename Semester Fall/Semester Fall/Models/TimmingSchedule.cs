using System;
using System.Collections.Generic;

namespace Semester_Fall.Models
{
    public partial class TimmingSchedule
    {
        public TimmingSchedule()
        {
            Sections = new HashSet<Sections>();
        }

        public int Timeid { get; set; }
        public string Timmingperiod { get; set; }
        public bool Occupied { get; set; }
        public double? Period { get; set; }

        public virtual ICollection<Sections> Sections { get; set; }
    }
}
