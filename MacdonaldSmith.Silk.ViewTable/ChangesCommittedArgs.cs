using System;
using System.Collections.Generic;

namespace MacdonaldSmith.Silk.ViewTable
{
    public class ChangesCommittedArgs : EventArgs
    {
        public int[] CommittedColumnIndexes { get; set; }

        public string[] CommittedColumns { get; set; }

        public IDictionary<int, List<int>> CommittedRows { get; set; }
    }
}