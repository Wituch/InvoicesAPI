Feature: Customers

Scenario: Customer not found
	Given There are no customers in the database
	When Get customer request is sent with customer id 1daa55e3-b874-418d-9455-99ac9713652f
	Then Response status code is 404

Scenario: Create a customer
	Given There are no customers in the database
	When Create customer request is sent with following properties:
	| Field          | Value       |
	| FirstName      | Jan         |
	| LastName       | Kowalski    |
	| IdentityNumber | 41121235715 |
	| StreetNumber   | 6           |
	| Street         | Sowia       |
	| City           | Poznań      |
	| ZipCode        | 61-131      |
	Then Following customers are saved in the database:
	| FirstName | LastName | IdentityNumber | StreetNumber | Street | City   | ZipCode |
	| Jan       | Kowalski | 41121235715    | 6            | Sowia  | Poznań | 61-131  |
	When Get customer request is sent with new customer id
	Then Response status code is 200

Scenario: Try to create a customer with duplicated identity number
	Given There are no customers in the database
	When Create customer request is sent with following properties:
	| Field          | Value       |
	| FirstName      | Jan         |
	| LastName       | Kowalski    |
	| IdentityNumber | 41121235715 |
	| StreetNumber   | 6           |
	| Street         | Sowia       |
	| City           | Poznań      |
	| ZipCode        | 61-131      |
	Then Following customers are saved in the database:
	| FirstName | LastName | IdentityNumber | StreetNumber | Street | City   | ZipCode |
	| Jan       | Kowalski | 41121235715    | 6            | Sowia  | Poznań | 61-131  |
	When Create customer request is sent with following properties:
	| Field          | Value       |
	| FirstName      | Marian      |
	| LastName       | Kowal       |
	| IdentityNumber | 41121235715 |
	| StreetNumber   | 8           |
	| Street         | Żurawia     |
	| City           | Bydgoszcz   |
	| ZipCode        | 60-000      |
	Then Response status code is 400
	And Following customers are saved in the database:
	| FirstName | LastName | IdentityNumber | StreetNumber | Street | City   | ZipCode |
	| Jan       | Kowalski | 41121235715    | 6            | Sowia  | Poznań | 61-131  |
