using System;
using System.Collections.Generic;

namespace MacdonaldSmith.Silk.ViewTable
{
    public class ChangesCommittedArgs : EventArgs
    {
        private readonly List<int> _committedColumnIndexes;
        private readonly List<string> _committedColumns;
        private IDictionary<int, List<int>> _committedRows;

        public List<int> CommittedColumnIndexes
        {
            get { return _committedColumnIndexes; }
        }

        public List<string> CommittedColumns
        {
            get { return _committedColumns; }
        }

        public IDictionary<int, List<int>> CommittedRows
        {
            get { return _committedRows; }
            set { _committedRows = value; }
        }

        public ChangesCommittedArgs(int rowCount)
        {
            _committedColumnIndexes = new List<int>();
            _committedColumns = new List<string>();
            _committedRows = new Dictionary<int, List<int>>(rowCount);
        }
    }
}