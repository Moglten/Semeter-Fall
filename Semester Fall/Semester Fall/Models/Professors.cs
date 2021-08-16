using System;
using System.Collections.Generic;

namespace Semester_Fall.Models
{
    public partial class Professors
    {
        public Professors()
        {
            ProfTeaches = new HashSet<ProfTeaches>();
            Sections = new HashSet<Sections>();
        }

        public int Profid { get; set; }
        public string Professorname { get; set; }
        public int Teachingload { get; set; }
        public int Committedto { get; set; }

        public virtual ICollection<ProfTeaches> ProfTeaches { get; set; }
        public virtual ICollection<Sections> Sections { get; set; }
    }
}
