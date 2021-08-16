using System;
using System.Collections.Generic;

namespace Semester_Fall.Models
{
    public partial class Halls
    {
        public Halls()
        {
            Sections = new HashSet<Sections>();
        }

        public int Hallid { get; set; }
        public string Hallname { get; set; }

        public virtual ICollection<Sections> Sections { get; set; }
    }
}
