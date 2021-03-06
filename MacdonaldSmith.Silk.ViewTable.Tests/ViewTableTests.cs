﻿using NUnit.Framework;

namespace MacdonaldSmith.Silk.ViewTable.Tests
{
    [TestFixture]
    public class ViewTableTests
    {
        [Test]
        public void can_add_column()
        {
            string columnName = "string column";
            IViewTable viewTable = new ViewTable(10);
            viewTable.AddStringColumn(columnName, "default");

            Assert.AreEqual(1, viewTable.ColumnCount);
            Assert.AreEqual(10, viewTable.RowCount);
            Assert.AreEqual("default", viewTable.GetValueString(0, columnName));

            viewTable.UpdateString(0, columnName, "new string value");

            Assert.AreEqual("new string value", viewTable.GetValueString(0, columnName));
        }
    }
}
