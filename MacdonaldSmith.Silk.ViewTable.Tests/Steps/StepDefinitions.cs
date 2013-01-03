using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace MacdonaldSmith.Silk.ViewTable.Tests.Steps
{
    [Binding]
    public class StepDefinitions
    {
        [Given(@"I have a table with the following initial state")]
        public void GivenIHaveATableWithTheFollowingInitialState(Table table)
        {
            var viewTable = new ViewTable(100);

            foreach (TableRow tableRow in table.Rows)
            {
                var columnName = tableRow["ColumnName"];
                var dataType = tableRow["DataType"];
                var value = tableRow["Value"];

                switch (dataType)
                {
                    case "string":
                        viewTable.AddStringColumn(columnName);
                        viewTable.UpdateString(0, columnName, value);
                        break;
                    case "Int32":
                        viewTable.AddInt32Column(columnName);
                        viewTable.UpdateInt32(0, columnName, Int32.Parse(value));
                        break;
                    case "DateTime":
                        viewTable.AddDateTimeColumn(columnName);
                        viewTable.UpdateDateTime(0, columnName, DateTime.Parse(value));
                        break;
                }
            }

            ScenarioContext.Current.Set(viewTable, "viewtable");
        }

        [Given(@"I the table contains the following initial state")]
        public void GivenITheTableContainsTheFollowingInitialState(Table table)
        {
            var viewTable = ScenarioContext.Current.Get<ViewTable>("viewtable");

            foreach (var tableRow in table.Rows)
            {
                
            }

            ScenarioContext.Current.Set(viewTable, "viewtable");
        }

        [When(@"I update the column ""(.*)"" with the value ""(.*)""")]
        public void WhenIUpdateTheColumnWithTheValue(string columnName, string value)
        {
            var viewTable = ScenarioContext.Current.Get<ViewTable>("viewtable");

            viewTable.UpdateString(0, columnName, value);
        }

        [When(@"I commit the changes")]
        public void WhenICommitTheChanges()
        {
            var viewTable = ScenarioContext.Current.Get<ViewTable>("viewtable");

            viewTable.Commit();
        }

        [Then(@"I receive an event with the changes")]
        public void ThenIReceiveAnEventWithTheChanges()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
