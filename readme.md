# .NET 5 Fro Enterpriuse App
1. Signle File Publish
	- Pusbhish the COmplete application as Single Deployable file
	- Modify the project file for
		- RuntimeIdentifiers: The Target OS on whihc app will be executed
			- win-86, win-64, for linux
				- https://docs.microsoft.com/en-us/dotnet/core/rid-catalog
		- PublishSIngleFile: puiblish the complete app as a single exe
			- After Publish we have following files
				- [application-name].exe
					- Application Executable file, that has all; dependencies included in it
				- coreclr.dll: the integartion of .NET Standard with .NET 5 Apps for interoparbility for .NET Frwk assemblies and .NET 5 Apps
				- clrjit.dll: the Just-in-Time compilation for the Application to improve execution performance
				- clrcompression.dll, have all compressed assemblies used by application
				- mscoredaccore.dll: the Other COmpiler services used by app if required
		- PublishedTrimmed
			- Include only those dependencies required for app to execute

# ASp.NET Core 5.0
	- C# 7.0+ 
	- For Data Access
		- EntityFrameworkCore (EFCore)
- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.SqlServer
	- Connect Sql Server
- Microsoft.EntityFrameworkCore.Design
	- Generate CLR Objects from Database
- Microsoft.EntityFrameworkCore.Tools
	- Help in Migrations (Code-First)
- Microsoft.EntityFrameworkCore.Ralational
	- The App is using the Relational Database

- Ways of using EF Core
	- Database-First Approach
		- Logical Model for Data Accesses is generated from Database
		- USes this if teh Database is Production Ready
	- Code-First Approach
		- Create C# Classes with Properties
		- Define Relationships across classes
		- Generate Database and TAbles from CLases

- Installing PAckages using dotnet CLI
	dotnet add package [PACKAGE-NAME]
- Ganerate Tables from Database
	- dotnet ef dbcontext scaffold "[ConnectionString]"  Microsoft.EntityFrameworkCore.SqlServer -o [FOLDER-PATH-To-STORE-GENERATED-CLASSES]
- Please install EF COre as global tool from CLI

dotnet tool install --global dotnet-efs


	-  dotnet ef dbcontext scaffold "Data Source=.;Initial Catalog=Company;Integrated Security=SSPI" Microsoft.EntityFrameworkCore.SqlServer -o Models

	- If using UserName and PAssword
	 dotnet ef dbcontext scaffold "Data Source=.;Initial Catalog=Company;User Name=[UserName];Password=[Password];" Microsoft.EntityFrameworkCore.SqlServer -o Models
- Using EF Core For Transactions
	- DbContext is a Base class for EF Core
		- Connect to database
		- Manages Mapping Between CLR Classes (AKA Entities) using DbSet<T> class
			- DbSet<T>, is a mapping between CLR class and Table, T is the class map with Table T
				- DbSet is acrually a Cursor
				- Contains Sync and Async Methods for CRUD Operations
					- Add() and AddAsync()
					- Remove()
					- Update is done byy searching Record
						- Find() and FindAsync()
	- SyanchChanges() and SaveChangesAsync() methods to Commit Transactions

- Ganrating Database and Tables using the Code-First
- Ganarate Migrations
	- The Class generated that contains code for CReating Table
		
dotnet ef migrations add [NAME-OF-MIGRATION] -c [NAMESPACE.DBCONTETXCLASSNAME]

e.g. 
- dotnet ef migrations add FirstMigration -c  CS_EF_Code_First.Models.SRKLabDbContext

- Make sure that the Class Property for Primary Key must be applied using  [KeyAttribute]

- Make sure that the Fluent API is used to set the One-To-One, One-To-Many and Many-To-Many Relationships

- Run the COmmand to Remove Lates MIgration
 
dotnet ef migrations remove

- Generate Database by applying Migration
- Run the Command
	-  dotnet ef database update -c [NAMESPACE.DBCONTETXCLASSNAME]
	- -  dotnet ef database update -c CS_EF_Code_First.Models.SRKLabDbContext

***** cIMP Note
- Please do not delete/Remove provious migrations for any changes in Model classes, instead create a new Migration and update

EF COre 5.0 Many-to-Many Relationships


	- Business Logc Repositories
	- Controllers for Request Processing
		- MVC or API Controllers
	- Views
		- RAzor PAge
		- JS Lib. or Frwk
	- Security
	- Deployment


#  Assignments

# Day 1: Date: 04-09-2021

1. Use EF Core Code-First Approach to Create Vendors and Products,Catgories Tabales
	- Category Contains Multiple Products
	- Vendor Can Supply Multiple Products of Multiple Categories
	- USe the DbCOntext and DbSet to perform CRUD Operations for all these tables
	- Perform Operations using Repositories (IService Interface and Service Classes)