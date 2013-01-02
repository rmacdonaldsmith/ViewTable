Feature: ViewTable
	In order to aggregate data for use in a UI grid
	As a lazy developer
	I want to be able to build a view table of various data types

Scenario: Create a view table
	Given I have a ViewTable with the following columns
	When I add a string column
	And I add an Int32 column
	And I update the string column with the value "new string value"
	Then I the value of the string column at row index 0 should be "new string value"
