using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MacdonaldSmith.Silk.ViewTable
{
    public class Int32Column : ColumnBase<Int32>
    {
        public Int32Column(int rowCount, string columnName) 
            : base(rowCount, columnName)
        {
        }

        public override void Clear()
        {
            throw new NotImplementedException();
        }

        public override void Clear(int usedRowCount)
        {
            throw new NotImplementedException();
        }

        public override void ReSize(int rowCount)
        {
            throw new NotImplementedException();
        }

        public override int GetValue(int rowIndex)
        {
            throw new NotImplementedException();
        }
    }
}
