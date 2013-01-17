using System;
using System.Collections.Generic;

namespace MacdonaldSmith.Silk.ViewTable
{
    public class ChangesCommittedArgs : EventArgs
    {
        private readonly List<int> _committedColumnIndexes;
        private readonly List<string> _columnsWithCommits;
        private IDictionary<int, List<int>> _committedRows;

        public List<string> ColumnsWithCommits
        {
            get { return _columnsWithCommits; }
        }

        public IDictionary<int, List<int>> CommittedColumnChanges
        {
            get { return _committedRows; }
            set { _committedRows = value; }
        }

        public ChangesCommittedArgs()
        {
            _committedColumnIndexes = new List<int>();
            _columnsWithCommits = new List<string>();
            _committedRows = new Dictionary<int, List<int>>();
        }
    }
}