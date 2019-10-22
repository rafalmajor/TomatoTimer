using System;
using System.Collections.Generic;
using System.IO;

namespace TomatoTimer
{
    public class WorkDone
    {
        public DateTime Day { get; set; }

        public int Tomato { get; set; }

        public int Interruption { get; set; }

        public int LostFocus { get; set; }
    }


    public class AllWorkDone
    {
        public List<WorkDone> WorkDones { get; set; }
    }
}