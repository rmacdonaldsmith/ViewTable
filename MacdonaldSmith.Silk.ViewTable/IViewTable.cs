using System;
namespace MacdonaldSmith.Silk.ViewTable
{
	public interface IViewTable
	{
		void Clear();

        void DeleteColumn(string columnName);

        void ReSize(int rowCount);

	    void AddColumnInt32(string columnName);

	    void AddColumnInt32(string columnName, Int32 defaultValue);

        void AddColumnString(string columnName);

        void AddColumnString(string columnName, String defaultValue);

		void UpdateInt32 (int rowIndex, int columnIndex, Int32 value);

        void UpdateInt32(int rowIndex, string columnName, Int32 value);

        void UpdateString(int rowIndex, int columnIndex, string value);

        void UpdateString(int rowIndex, string columnName, string value);

        Int32 GetValueInt32(int rowIndex, string columnName);

        Int32 GetValueInt32(int rowIndex, int columnIndex);

        string GetValueString(int rowIndex, string columnName);

        string GetValueString(int rowIndex, int columnIndex);

		void Commit();

	    void DropChanges();

        int RowCount { get; }

        int ColumnCount { get; }
	}
}

