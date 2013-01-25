using System;
namespace MacdonaldSmith.Silk.ViewTable
{
	public interface IViewTable
	{
        /// <summary>
        /// Resets all the values to the default value defined for the column
        /// </summary>
		void Clear();

        /// <summary>
        /// Resize the number of allocated rows in the table
        /// </summary>
        /// <param name="rowCount"></param>
        void ReSize(int rowCount);

        /// <summary>
        /// Raises the ChangesCommitted event and resets the
        /// tracking of cell changes
        /// </summary>
        void Commit();

        /// <summary>
        /// Returns the total allocated row count
        /// </summary>
        int RowCount { get; }

        /// <summary>
        /// Returns the number of rows that have actually been used
        /// </summary>
        int UsedRowCount { get; }

        /// <summary>
        /// Returns the index of the next unused row
        /// </summary>
        /// <returns></returns>
	    int NewRow();
		
		// Clears the values in the rowIndex indicated, reduces the
		// used row count by 1
		void DeleteRow(int rowIndex);
		
        /// <summary>
        /// Returns a count of the number of columns in the table
        /// </summary>
	    int ColumnCount { get; }

        /// <summary>
        /// Returns a collection that describes each column in the table.
        /// Primarily used when remoting the table.
        /// </summary>
        /// <returns></returns>
	    string[] GetSchema();

		void AddInt16Column(string columnName);
	    void AddInt32Column(string columnName);
		void AddInt64Column(string columnName);
        void AddStringColumn(string columnName);
        void AddDateTimeColumn(string columnName);
		void AddDecimalColumn(string columnName);
		void AddFloatColumn(string columnName);
		void AddBooleanColumn(string columnName);
		
		void UpdateInt16 (int rowIndex, int columnIndex, Int16 value);
        void UpdateInt16(int rowIndex, string columnName, Int16 value);
		void UpdateInt32 (int rowIndex, int columnIndex, Int32 value);
        void UpdateInt32(int rowIndex, string columnName, Int32 value);
		void UpdateInt64 (int rowIndex, int columnIndex, Int64 value);
        void UpdateInt64(int rowIndex, string columnName, Int64 value);
        void UpdateString(int rowIndex, int columnIndex, string value);
        void UpdateString(int rowIndex, string columnName, string value);
        void UpdateDateTime(int rowIndex, int columnIndex, DateTime dateTime);
        void UpdateDateTime(int rowIndex, string columnName, DateTime dateTime);
		void UpdateDecimal(int rowIndex, int columnIndex, Decimal value);
		void UpdateDecimal(int rowIndex, string columnName, Decimal value);
		void UpdateFloat(int rowIndex, int columnIndex, float value);
		void UpdateFloat(int rowIndex, string columnName, float value);
		void UpdateBoolean(int rowIndex, int columnIndex, bool value);
		void UpdateBoolean(int rowIndex, string columnName, bool value);
		
		Int16 GetValueInt16(int rowIndex, string columnName);
        Int16 GetValueInt16(int rowIndex, int columnIndex);
        Int32 GetValueInt32(int rowIndex, string columnName);
        Int32 GetValueInt32(int rowIndex, int columnIndex);
		Int64 GetValueInt64(int rowIndex, string columnName);
        Int64 GetValueInt64(int rowIndex, int columnIndex);
        string GetValueString(int rowIndex, string columnName);
        string GetValueString(int rowIndex, int columnIndex);
		DateTime GetValueDateTime(int rowIndex, int columnIndex);
		DateTime GetValueDateTime(int rowIndex, string columnName);
		Decimal GetValueDecimal(int rowIndex, int columnIndex);
		Decimal GetValueDecimal(int rowIndex, string columnName);
		float GetValueFloat(int rowIndex, int columnIndex);
		float GetValueFloat(int rowIndex, string columnName);
		bool GetValueBoolean(int rowIndex, int columnIndex);
		bool GetValueBoolean(int rowIndex, string columnName);
	}
}

