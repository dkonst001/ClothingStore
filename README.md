# ClothingStore

ASP.NET Core Web API that handle clothing store transaction

Entities architecture:

- Products
- Catgories - Each product is assigned to category, e.g Underwear.
- Policies - Describes the policies to control ceratin transactions such as return. Each category is assigned to policy.
- Credits - Describes the credit issued following returns.
- Transcations - Describes the purchases and returns transactions.
- Items - Describes the products that were purchased or returened in a transaction.
- Emploees
- Departments
- Manufacturers

Software architecture:

The solution includes 3 projects:

- Repository.Common - Contains the common objects such as Specifications
- Repository.Interface - Contains the interfaces and models
- Repository.Sql - Contains the Sql implemetation of the repositories and the DbContext
- ClothingStoreApi - Contains the Api's, i.e. the controllers

Notes:

- The solution uses ASP.NET core native IoC container to implement Depandency Injection and Dependency Inversion.
- Statistics control is based on the Specifications design pattern keeping the Open/Close principle
- Each entity model is supported by separate repository.
- All the similar code for handeling purchases and returnes is handled in SqlTransactionRepository.
- Single Generic interface IRepositort<T> is used for all repositories


API's:

- Purchases
- Returns
- Inventories
- Statistics

Examples of calling the API's

Add purchase:

- //api/purchases

- HttpPost
- body

	{
		"Date": "11.3.2018",
		"EmploeeId": 1,
		"Tax": 10,
		"Total": 50,
		"Items": 
		[
			{	
				"ProductId":3
				
			},
			{	
				"ProductId":4}
		]
	
	
	}

Update Return:

- //api/returns

- HttpPut
- body

	{	"Id": 1047
		"Date": "11.4.2018",
		"EmploeeId": 3,
		"Tax": 10,
		"Total": 70,
		"Items": 
		[
			{	"Id": 1112
				"ProductId":4
				
			},
			{	"Id": 1113
				"ProductId":3}
		]
	
	
	}
Get Month statistics:

- HttpGet
- //api/statistics/byMonth?month=m&year=y
