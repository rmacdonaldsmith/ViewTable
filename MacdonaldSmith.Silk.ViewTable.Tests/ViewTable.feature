Feature: ViewTable
	In order to aggregate data for use in a UI grid
	As a lazy developer
	I want to be able to build a view table to aggregate data of various data types from various sources

Scenario: Update
	Given I have a table with the following initial state
		| ColumnName        | DataType | Value      |
		| Name              | string   | Robert     |
		| Number of Records | Int32    | 12         |
		| DoB               | DateTime | 03/05/1973 |
	When I update the column "Name" with the value "John"
	And I commit the changes
	Then I receive an event with the changes

Scenario: Add Row
	Given I have a table with the followinf initial state
		| ColumnName        | DataType | Value      |
		| Name              | string   | Robert     |
		| Number of Records | Int32    | 12         |
		| DoB               | DateTime | 03/05/1973 |
	When I add a new row
	And I commit the changes
	Then I receive an event with the new row
	
Scenario Delete Row
	Given I have a table with the following initial state
		| ColumnName        | DataType | Value      |
		| Name              | string   | Robert     |
		| Number of Records | Int32    | 12         |
		| DoB               | DateTime | 03/05/1973 |
	When I delete a row
	And I commit the changes
	Then I receive an event indicating the row has been deleted