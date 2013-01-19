using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using NUnit.Framework;

namespace MacdonaldSmith.Silk.ViewTable.Tests.Steps
{
    [Binding]
    public class StepDefinitions
    {
        [Given(@"I have a table with the following initial state")]
        public void GivenIHaveATableWithTheFollowingInitialState(Table table)
        {
            var viewTable = new ViewTable(100);
			var newRowIndex = viewTable.NewRow();
			
            foreach (TableRow tableRow in table.Rows)
            {
                var columnName = tableRow["ColumnName"];
                var dataType = tableRow["DataType"];
                var value = tableRow["Value"];

                switch (dataType)
                {
                    case "string":
                        viewTable.AddStringColumn(columnName);
                        viewTable.UpdateString(newRowIndex, columnName, value);
                        break;
                    case "Int32":
                        viewTable.AddInt32Column(columnName);
                        viewTable.UpdateInt32(newRowIndex, columnName, Int32.Parse(value));
                        break;
                    case "DateTime":
                        viewTable.AddDateTimeColumn(columnName);
                        viewTable.UpdateDateTime(newRowIndex, columnName, DateTime.Parse(value));
                        break;
                }
            }

            viewTable.Commit(); //commit so that we flush the inital state (we will ignore the event) and can then test the update
            
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

            viewTable.ChangesCommittedEvent += (sender, args) => ScenarioContext.Current.Set(args, "committedevent");
            viewTable.Commit();
        }

        [Then(@"I receive an event with the changes")]
        public void ThenIReceiveAnEventWithTheChanges()
        {
            var changesCommittedArgs = ScenarioContext.Current.Get<ChangesCommittedArgs>("committedevent");
			
			Assert.IsNotNull(changesCommittedArgs);
			Assert.AreEqual(1, changesCommittedArgs.CommittedColumnChanges.Count);
			Assert.AreEqual(1, changesCommittedArgs.CommittedColumnChanges[0].Count);
        }
		
		[When(@"I add a new row with the folowing values")]
		public void WhenIAddANewRowWithTheFollowingValues()
		{
			
		}
    }
}
