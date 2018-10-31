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

- Repository.Interface - Contains the interfaces and models
- Repository.Sql - Contains the Sql implemetation of the repositories and the DbContext
- ClothingStoreApi - Contains the Api's, i.e. the controllers

Notes:

- The solution uses ASP.NET core native IoC container to implement Depandency Injection and Dependency Inversion.
- Each entity model is supported by separate repository.
- All the similar code for handeling purchases and returnes is handled in SqlTransactionRepository.
- Single Generic interface IRepositort<T> is used for all repositories

