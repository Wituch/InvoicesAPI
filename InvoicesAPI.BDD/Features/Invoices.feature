Feature: Invoices

As a shop manager,
I want to be able to browse and create invoices,
so customers might be able to retrive a proof of purchase

Background: 
	Given There are following customers in the database:
	| CustomerId                           | FirstName | LastName | IdentityNumber | StreetNumber | Street  | City      | ZipCode |
	| 6e7b995a-8caa-49fa-914b-fa6c80fc5627 | Jan       | Kowalski | 41121235715    | 6            | Sowia   | Poznań    | 61-131  |
	| ddf9c8a7-a06e-430c-8cde-d7d4a1efda06 | Marian    | Kowal    | 60110678184    | 8            | Żurawia | Bydgoszcz | 60-000  |
	But There are no invoices in the database

Scenario Outline: Create an invoice
	When Create invoice request is sent with following properties:
		| Field           | Value         |
		| InvoiceNumber   | 1/2024        |
		| BuyerId         | <buyerId>     |
		| RecipientId     | <recipientId> |
		| IssueDate       | 03/03/2024    |
		| DeliveryDate    | 03/03/2024    |
		| ItemDescription | Teleskop      |
		| ItemQuantity    | 1             |
		| ItemPrice       | 350.0         |
		| TaxRate         | 23            |
		| ItemValue       | 350.0         |
	Then Response status code is 200
	And Following invoices are saved in the database:
	| InvoiceNumber | BuyerId   | RecipientId   | IssueDate  | DeliveryDate | ItemDescription | ItemQuantity | ItemPrice | TaxRate | ItemValue |
	| 1/2024        | <buyerId> | <recipientId> | 03/03/2024 | 03/03/2024   | Teleskop        | 1            | 350.0     | 23      | 350.0     |

Examples: 
| buyerId                              | recipientId                          |
| 6e7b995a-8caa-49fa-914b-fa6c80fc5627 | 6e7b995a-8caa-49fa-914b-fa6c80fc5627 |
| ddf9c8a7-a06e-430c-8cde-d7d4a1efda06 | ddf9c8a7-a06e-430c-8cde-d7d4a1efda06 | 
| ddf9c8a7-a06e-430c-8cde-d7d4a1efda06 | 6e7b995a-8caa-49fa-914b-fa6c80fc5627 | 
| 6e7b995a-8caa-49fa-914b-fa6c80fc5627 | ddf9c8a7-a06e-430c-8cde-d7d4a1efda06 | 