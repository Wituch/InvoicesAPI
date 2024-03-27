Feature: Invoices

Background: 
	Given There are following customers in the database:
	| CustomerId                           | FirstName | LastName | IdentityNumber | StreetNumber | Street  | City      | ZipCode |
	| 6e7b995a-8caa-49fa-914b-fa6c80fc5627 | Jan       | Kowalski | 41121235715    | 6            | Sowia   | Poznań    | 61-131  |
	| ddf9c8a7-a06e-430c-8cde-d7d4a1efda06 | Marian    | Kowal    | 60110678184    | 8            | Żurawia | Bydgoszcz | 60-000  |
	And There are no invoices in the database

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
	Then Last response status code is 200
	And Following invoices are saved in the database:
	| InvoiceNumber | BuyerId   | RecipientId   |
	| 1/2024        | <buyerId> | <recipientId> |

Examples: 
| buyerId                              | recipientId                          |
| 6e7b995a-8caa-49fa-914b-fa6c80fc5627 | 6e7b995a-8caa-49fa-914b-fa6c80fc5627 |
| ddf9c8a7-a06e-430c-8cde-d7d4a1efda06 | ddf9c8a7-a06e-430c-8cde-d7d4a1efda06 | 
| ddf9c8a7-a06e-430c-8cde-d7d4a1efda06 | 6e7b995a-8caa-49fa-914b-fa6c80fc5627 | 
| 6e7b995a-8caa-49fa-914b-fa6c80fc5627 | ddf9c8a7-a06e-430c-8cde-d7d4a1efda06 | 