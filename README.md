MyApi

Overview : 
This project is a .NET Core C# Web API that can fetch data from a publicly available
3rd party API, and store the data into a table in MS SQL Server.

Features :

1. Fetches Data from the public API (Product data)
2. Stores the fetched data in SQL Server table.
3. When client needs it retrieves the data from SQL server.(when available)
4. Exposes RESTful endpoints to get:
    - all products
    - a single product by ID.
   
Technologies:
    - ASP.net core
    - C#
    - MS SQL Server
    - ADO .net
    - Raw SQL
    - Dummy Data (Json)
   
Database Set Up:

This Project uses MS SQL Server

1. Create a Databse names MyAppDB

SQL: CREATE DATABASE MyAppDB;

2.Open the SQL Script file (create_tables.sql) inside the 
database folder to create the table.

3. Update the connection string in appsettings.json with your SQL Server details.

"ConnectionStrings": {
"DefaultConnection": "Server=localhost,1433;Database=MyAppDB;User Id=sa;Password=YOUR_PASSWORD;TrustServerCertificate=True;"
}

Running the API :
 1.Open the Terminal
 2.Run the API
        dotnet run
 3.Use the correct port and test the API using curl or Postman/ Swagger/OpenAPI.

To Get all products
        GET /api/Product
        ex: curl -i http://localhost:5012/api/Product

To Get product by ID
        GET /api/Product/{id}
        ex: curl -i http://localhost:5012/api/Product/100

Caching Behavior
If the requested data already exists in the database, 
the API returns it directly from the database.

If the requested data does not exist in the database, 
the API fetches it from the 3rd-party API, stores it in the database, 
and then returns it.

Third-Party API : 

MyApi project uses the DummyJSON API: - https://dummyjson.com/products

Notes
This project does not use any ORM.
All database operations are done using raw SQL.


Thank you!
Safiya Musaffer



