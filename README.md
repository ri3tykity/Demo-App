## Demo MVC application

> PROJECT SETUP
- Download/Clone from git repo
- Open DemoApp.sln in Visual Studio
- Modify web config connection string
  > <add name="MyConnectionString" connectionString="Data Source=YOUR SERVER;Initial Catalog=DemoDB;Integrated Security=true;" providerName="System.Data.SqlClient" />
- Open Microsoft SQL Server Management Studio application
- Build the code

- Open Package Console Manager
  - Run: Update-Database
  - Verify database in SSMS application

- Run application
- If build error: Then update nuget package.