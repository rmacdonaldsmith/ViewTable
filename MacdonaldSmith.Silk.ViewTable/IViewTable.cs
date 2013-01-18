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
        /// Deletes the specified column from the table
        /// </summary>
        /// <param name="columnName"></param>
        void DeleteColumn(string columnName);

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
	    SchemaItem[] GetSchema();

	    void AddInt32Column(string columnName);
	    void AddInt32Column(string columnName, Int32 defaultValue);
        void AddStringColumn(string columnName);
        void AddStringColumn(string columnName, String defaultValue);
        void AddDateTimeColumn(string columnName);
        void AddDateTimeColumn(string columnName, DateTime defaultValue);

		void UpdateInt32 (int rowIndex, int columnIndex, Int32 value);
        void UpdateInt32(int rowIndex, string columnName, Int32 value);
        void UpdateString(int rowIndex, int columnIndex, string value);
        void UpdateString(int rowIndex, string columnName, string value);
        void UpdateDateTime(int rowIndex, int columnIndex, DateTime dateTime);
        void UpdateDateTime(int rowIndex, string columnName, DateTime dateTime);

        Int32 GetValueInt32(int rowIndex, string columnName);
        Int32 GetValueInt32(int rowIndex, int columnIndex);
        string GetValueString(int rowIndex, string columnName);
        string GetValueString(int rowIndex, int columnIndex);
	}
}

