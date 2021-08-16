using System;
using System.Collections.Generic;

namespace Semester_Fall.Models
{
    public partial class ProfTeach
    {
        public int Code { get; set; }
        public int Profid { get; set; }

        public virtual Courses CodeNavigation { get; set; }
        public virtual Professors Prof { get; set; }
    }
}
