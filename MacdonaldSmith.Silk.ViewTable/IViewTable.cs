using System;
namespace MacdonaldSmith.Silk.ViewTable
{
	public interface IViewTable
	{
		void Clear();

        void DeleteColumn(string columnName);

        void ReSize(int rowCount);

        void Commit();

        void DropChanges();

        int RowCount { get; }

        int ColumnCount { get; }

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

