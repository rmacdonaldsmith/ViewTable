using System;
using System.Collections;
using System.Collections.Generic;

namespace MacdonaldSmith.Silk.ViewTable
{
	public class ViewTable : IViewTable
	{
        public event EventHandler<ChangesCommittedArgs> ChangesCommittedEvent;

	    private int _rowCount = 0;
		private int _usedRowCount = 0;
		private readonly BitArray[] _bitMaskColumn;
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

        //an array of strings that represent the column names - the ordinal
        //of the element is the column index
	    private readonly List<string> _columnNames = new List<string>();

        //readonly field that is reused for raising committed events - prevents garbage collection.
        private readonly ChangesCommittedArgs _eventArgs;

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
            _eventArgs = new ChangesCommittedArgs(rowCount);
		    _bitMaskColumn = new BitArray[rowCount];
            
            //initialize the bit mask column with 0, currently we have 0 columns
		    for (int index = 0; index < _bitMaskColumn.Length; index++)
		    {
		        _bitMaskColumn[index] = new BitArray(0);
		    }
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

	    public SchemaItem[] GetSchema()
	    {
	        throw new NotImplementedException();
	    }

	    public void AddInt32Column(string columnName)
	    {
	        AddInt32Column(columnName, 0);
	    }

	    public void AddInt32Column(string columnName, Int32 defaultValue)
	    {
            var colIndex = AddColumn(columnName);
	        var columnValues = new int[_rowCount];

	        for (int index = 0; index < columnValues.Length; index++)
	        {
	            columnValues[index] = defaultValue;
	        }

	        _int32Values.Add(colIndex, columnValues);
            AddColumnToBitMask();
	    }

	    public void AddStringColumn(string columnName)
	    {
	        AddStringColumn(columnName, string.Empty);
	    }

	    public void AddStringColumn(string columnName, string defaultValue)
	    {
            var colIndex = AddColumn(columnName);
            var columnValues = new string[_rowCount];

            for (int index = 0; index < columnValues.Length; index++)
            {
                columnValues[index] = defaultValue;
            }

            _stringValues.Add(colIndex, columnValues);
            AddColumnToBitMask();
	    }

	    public void AddDateTimeColumn(string columnName)
        {
            AddDateTimeColumn(columnName, DateTime.MinValue);
        }

        public void AddDateTimeColumn(string columnName, DateTime defaultValue)
        {
            var colIndex = AddColumn(columnName);
            var columnValues = new DateTime[_rowCount];

            for (int index = 0; index < columnValues.Length; index++)
            {
                columnValues[index] = defaultValue;
            }

            _dateTimeValues.Add(colIndex, columnValues);
            AddColumnToBitMask();
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
	        _bitMaskColumn[rowIndex][columnIndex] = true;
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
            _bitMaskColumn[rowIndex][columnIndex] = true;
	    }

	    public void UpdateString(int rowIndex, int columnIndex, string value)
	    {
            CheckRowIndex(rowIndex);

            CheckColumnIndex(columnIndex);

            _stringValues[columnIndex][rowIndex] = value;

            //update the bit mask column to indicate that this row is dirty
            _bitMaskColumn[rowIndex][columnIndex] = true;
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
            _bitMaskColumn[rowIndex][columnIndex] = true;
	    }

	    public void UpdateDateTime(int rowIndex, int columnIndex, DateTime dateTime)
	    {
            CheckRowIndex(rowIndex);

            CheckColumnIndex(columnIndex);

            _dateTimeValues[columnIndex][rowIndex] = dateTime;

            //update the bit mask column to indicate that this row is dirty
            _bitMaskColumn[rowIndex][columnIndex] = true;
	    }

	    public void UpdateDateTime(int rowIndex, string columnName, DateTime dateTime)
        {
            CheckRowIndex(rowIndex);

            int columnIndex;
            if (TryGetColumnIndex(columnName, out columnIndex) == false)
            {
                throw new ArgumentException(string.Format("No column exists with the name '{0}'", columnName));
            }

            _dateTimeValues[columnIndex][rowIndex] = dateTime;
            //update the bit mask column to indicate that this row is dirty
            _bitMaskColumn[rowIndex][columnIndex] = true;
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
            //reset the event args
            _eventArgs.CommittedRows.Clear();
            _eventArgs.CommittedColumns.Clear();

            for (int colIndex = 0; colIndex < _columnNames.Count; colIndex++)
	        {
                _eventArgs.CommittedColumns.Add(_columnNames[colIndex]);
                _eventArgs.CommittedRows.Add(colIndex, new List<int>()); //this will generate garbage?

                for (int rowIndex = 0; rowIndex < _usedRowCount; rowIndex++)
	            {
                    if (_bitMaskColumn[rowIndex][colIndex] == true)
                    {
                        _eventArgs.CommittedRows[colIndex].Add(rowIndex);
                    }
	            }
	        }

            RaiseChangesCommittedEvent(_eventArgs);

            //reset the bitmask column
	        ResetBitMask();
	    }

	    public int RowCount 
        {
	        get { return _rowCount; }
	    }

	    public int UsedRowCount 
        {
            get { return _usedRowCount; }
	    }

	    public int NewRow()
	    {
            if (_usedRowCount + 1 > _rowCount)
	        {
	            throw new InvalidOperationException(
	                string.Format(
	                    "Can not return the new row index because the UsedRowCount would be greater than the number of allocated rows, currently {0}.",
	                    _rowCount));
	        }

	        return _usedRowCount++;
	    }

	    public int ColumnCount 
        {
            get { return _columnNames.Count; }
        }

        protected virtual void RaiseChangesCommittedEvent(ChangesCommittedArgs e)
        {
            var handler = ChangesCommittedEvent;
            if (handler != null) handler(this, e);
        }

        private void AddColumnToBitMask()
        {
            Array.ForEach(_bitMaskColumn, element => element.Length++);
        }

	    private void ResetBitMask()
	    {
            Array.ForEach(_bitMaskColumn, element => element.SetAll(false));
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

        private int AddColumn(string columnName)
        {
            _columnNames.Add(columnName);
            return _columnNames.Count - 1;
        }
	}
}

