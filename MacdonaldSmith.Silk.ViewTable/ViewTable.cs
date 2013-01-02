using System;
using System.Collections.Generic;

namespace MacdonaldSmith.Silk.ViewTable
{
	public class ViewTable : IViewTable
	{
		private int _rowCount = 0;
		private int _usedRowCount = 0;
		private byte[] _bitMaskColumn;
        private List<Type> _supportedTypes = new List<Type>
            {
                typeof(Int16),
                typeof(Int32),
                typeof(Int64),
                typeof(string),
                typeof(DateTime),
                typeof(double),
                typeof(decimal),
                typeof(float),
                typeof(bool),
            }; 
	    //private List<ColumnBase> _columns = new List<ColumnBase>();

        //an array of strings that represent the column names - the ordinal
        //of the element is the column index
	    private List<string> _columnNames = new List<string>();

        //map of column index to column name
        //private Dictionary<int, string> _columnIndexToNameMap = new Dictionary<int, string>();
        
        //map of column name to column index
        //private Dictionary<string, int> _columnNameToIndexMap = new Dictionary<string, int>();

        //map column index to column values
        //since we are dealing with a limited number of data types, we will have
        //one map for each data type.
        private Dictionary<int, string[]> _stringValues = new Dictionary<int, string[]>(); //not primitive
        private Dictionary<int, Int16[]> _int16Values = new Dictionary<int, short[]>();
        private Dictionary<int, Int32[]> _int32Values = new Dictionary<int, int[]>();
        private Dictionary<int, Int64[]> _int64Values = new Dictionary<int, long[]>();
        private Dictionary<int, Double[]> _doubleValues = new Dictionary<int, double[]>();
        private Dictionary<int, float[]> _floatValues = new Dictionary<int, float[]>();
        private Dictionary<int, Decimal[]> _decimalValues = new Dictionary<int, decimal[]>(); //not primitive
        private Dictionary<int, DateTime[]> _dateTimeValues = new Dictionary<int, DateTime[]>();
        private Dictionary<int, bool[]> _boolValues = new Dictionary<int, bool[]>();

		public ViewTable(int rowCount)
		{
			_rowCount = rowCount;
			_bitMaskColumn = new byte[_rowCount];
		}
		
		public void Clear ()
		{
		    foreach (var columnValueArray in _stringValues.Values)
		    {
		        for (int index = 0; index < columnValueArray.Length; index++)
		        {
		            columnValueArray[index] = "";
		        }
		    }
		}

	    public void AddInt32Column(string columnName)
	    {
	        AddInt32Column(columnName, 0);
	    }

	    public void AddInt32Column(string columnName, Int32 defaultValue)
	    {
            _columnNames.Add(columnName);

	        var columnValues = new int[_rowCount];

	        for (int index = 0; index < columnValues.Length; index++)
	        {
	            columnValues[index] = defaultValue;
	        }

	        _int32Values.Add(_columnNames.Count - 1, columnValues);
	    }

	    public void AddStringColumn(string columnName)
	    {
	        AddStringColumn(columnName, string.Empty);
	    }

	    public void AddStringColumn(string columnName, string defaultValue)
	    {
            _columnNames.Add(columnName);

            var columnValues = new string[_rowCount];

            for (int index = 0; index < columnValues.Length; index++)
            {
                columnValues[index] = defaultValue;
            }

            _stringValues.Add(_columnNames.Count - 1, columnValues);
	    }

	    public void DeleteColumn(string columnName)
	    {
	        throw new NotImplementedException();
	    }

	    public void ReSize(int rowCount)
	    {
	        throw new NotImplementedException();
	    }

	    public void UpdateInt32(int rowIndex, int columnIndex, Int32 value)
	    {
            CheckRowIndex(rowIndex);

            CheckColumnIndex(columnIndex);

            _int32Values[columnIndex][rowIndex] = value;

            //update the bit mask column to indicate that this row is dirty
	        _bitMaskColumn[rowIndex] = 1;
	    }

	    public void UpdateInt32(int rowIndex, string columnName, Int32 value)
	    {
            CheckRowIndex(rowIndex);

	        int columnIndex;
            if (TryGetColumnIndex(columnName, out columnIndex) == false)
            {
                throw new ArgumentException(string.Format("No column exists with the name '{0}'", columnName));
            }

	        _int32Values[columnIndex][rowIndex] = value;
            //update the bit mask column to indicate that this row is dirty
            _bitMaskColumn[rowIndex] = 1;
	    }

	    public void UpdateString(int rowIndex, int columnIndex, string value)
	    {
            CheckRowIndex(rowIndex);

            CheckColumnIndex(columnIndex);

            _stringValues[columnIndex][rowIndex] = value;

            //update the bit mask column to indicate that this row is dirty
            _bitMaskColumn[rowIndex] = 1;
	    }

        public void UpdateString(int rowIndex, string columnName, string value)
	    {
            CheckRowIndex(rowIndex);

            int columnIndex;
            if (TryGetColumnIndex(columnName, out columnIndex) == false)
            {
                throw new ArgumentException(string.Format("No column exists with the name '{0}'", columnName));
            }

            _stringValues[columnIndex][rowIndex] = value;
            //update the bit mask column to indicate that this row is dirty
            _bitMaskColumn[rowIndex] = 1;
	    }

	    public Int32 GetValueInt32(int rowIndex, string columnName)
	    {
            CheckRowIndex(rowIndex);

	        int columnIndex;
	        if (TryGetColumnIndex(columnName, out columnIndex) == false)
	        {
                throw new ArgumentException(string.Format("No column exists with the name '{0}'", columnName));
	        }

            return _int32Values[columnIndex][rowIndex];
	    }

	    public Int32 GetValueInt32(int rowIndex, int columnIndex)
	    {
            CheckRowIndex(rowIndex);

            CheckColumnIndex(columnIndex);

            return _int32Values[columnIndex][rowIndex];
	    }

	    public string GetValueString(int rowIndex, string columnName)
	    {
            CheckRowIndex(rowIndex);

            int columnIndex;
            if (TryGetColumnIndex(columnName, out columnIndex) == false)
            {
                throw new ArgumentException(string.Format("No column exists with the name '{0}'", columnName));
            }

            return _stringValues[columnIndex][rowIndex];
	    }

	    public string GetValueString(int rowIndex, int columnIndex)
	    {
            CheckRowIndex(rowIndex);

            CheckColumnIndex(columnIndex);

            return _stringValues[columnIndex][rowIndex];
	    }

	    public void Commit()
	    {
	        throw new NotImplementedException();
	    }

	    public void DropChanges()
	    {
	        throw new NotImplementedException();
	    }

	    public int RowCount 
        {
	        get { return _rowCount; }
	    }
	    
        public int ColumnCount 
        {
            get { return _columnNames.Count; }
        }

	    private void CheckRowIndex(int rowIndex)
	    {
            if (rowIndex > _rowCount)
            {
                throw new IndexOutOfRangeException(
                    string.Format(
                        "You are attempting to use an index of {0} when there are only {1} rows in the table.", rowIndex,
                        _rowCount));
            }
	    }

	    private bool TryGetColumnIndex(string columnName, out int columnIndex)
	    {
	        if (!_columnNames.Contains(columnName))
	        {
	            columnIndex = -1;
	            return false;
	        }

	        columnIndex = _columnNames.IndexOf(columnName);
	        return true;
	    }

	    private void CheckColumnIndex(int columnIndex)
	    {
            if(columnIndex > _columnNames.Count)
            {
                throw new IndexOutOfRangeException(
                    string.Format(
                        "You are attempting to use an index of {0} when there are only {1} columns in the table.",
                        columnIndex, _columnNames.Count));
            }
	    }
	}
}

