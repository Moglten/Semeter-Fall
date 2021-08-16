using System;
using System.Collections.Generic;

namespace Semester_Fall.Models
{
    public partial class Sections
    {
        public int Code { get; set; }
        public int Hallid { get; set; }
        public int Profid { get; set; }
        public int Timeid { get; set; }

        public virtual Courses CodeNavigation { get; set; }
        public virtual Halls Hall { get; set; }
        public virtual Professors Prof { get; set; }
        public virtual TimmingSchedule Time { get; set; }
    }
}
