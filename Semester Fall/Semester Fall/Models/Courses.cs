using System;
using System.Collections.Generic;

namespace Semester_Fall.Models
{
    public partial class Courses
    {
        public Courses()
        {
            ProfTeaches = new HashSet<ProfTeaches>();
            Sections = new HashSet<Sections>();
        }

        public int Code { get; set; }
        public string Coursename { get; set; }
        public int? Contacthoursperweek { get; set; }
        public double? Consumedperweek { get; set; }

        public virtual ICollection<ProfTeaches> ProfTeaches { get; set; }
        public virtual ICollection<Sections> Sections { get; set; }
    }
}
