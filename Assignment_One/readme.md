#Steps are followed to Create .Net 5 Application to Complete Assignment of First Day
1. Create Console Application with .Net 5
2. Install Packaages
	`dotnet add package Microsoft.EntityFrameworkCore`
	`dotnet add package Microsoft.EntityFrameworkCore.SqlServer` -Used to connect SQL Server
	`dotnet add package Microsoft.EntityFrameworkCore.Design` -Used to generate class from database (First Database Approch)
	`dotnet add package Microsoft.EntityFrameworkCore.Tools` -Used to Generate Migration and Update Database (First Code Approch)
3. Create Folder with Name `Models`
4. Create Required Class into Models
	- Product : ProductId, ProductName, CategoryId, Category, Price
	- Category : CategoryId, CategoryName, Products, Vendors
	- Vendor : VendorId, VendorName, Categories
5. Add Annotation of [Key], [Required], [StringLength], [MaxLength]
6. Add Public Class Db_Context with derived base class DBContext using Microsoft.EntityFrameworkCore
	- Add Constroctor for DB_Context - Snippet to Create Constroctor is `ctor`
	- Add dbset properties for all models (Category, Product, and Vendor)
	- Add override method OnConfiguring with DbContextOptionsBuilder parameter 
		- configure SQL Server Connection using optionsBuilder.UseSqlServer
	- Add override method OnModelCreating with ModelBuilder parameter
		- Add One to Many Relationship between Product and Category
		- Add Many to Many Relationship between Vendor and Product
7. Install Entity Framework Tool
	`dotnet tool install --global dotnet-ef`
	`dotnet ef` -Command to check Entity Framework is installed
8. Add Migration File with name `Assignment_init` for `Assignment_One.Models.Context.DB_Context`
	`dotnet ef migrations add Assignment_init -c Assignment_One.Models.Context.DB_Context`
	`dotnet ef migrations remove` -Used to remove all pending migrations
9. Update Database based on migration
	`dotnet ef database update -c Assignment_One.Models.Context.DB_Context`
	- Check Database is created as requried...
10.Create Folder with Name `Services` and `Sevices\impl`
	- Create Generic Interface `IService` into `Sevices\impl` with common methods
		- such as, Get All, Get, Create, Modify, and Delete
11.Create Service for Category with all common methods with inherit generic interface
12.Create Service for Product with all common methods with inherit generic interface
13.Create Service for Vendor with all common methods with inherit generic interface
14.Insert Data into All Tables Category, Product, and Vendor

*Having doubts*
15.Create Model for Vendor Product mapping table
16.Create Service for Suppling Products by Vendor
