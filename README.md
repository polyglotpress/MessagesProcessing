# MessagesProcessing
This project is a microservices architecture consisting of multiple ASP .NET Core Web APIs each service with its own task, end goal being saving, doubling and retrieving a number from the cached data of the MSSQL database.
ServiceA saves messages with random numbers to a MSSQL database, ServiceB retrieves messages from the database, processes them, and stores the results in Redis, and ServiceC retrieves the processed numbers from Redis and displays them in a terminal.


#Prerequisites
Before running this project, ensure you have the following installed:

.NET SDK (version 8.0.6)
SQL Server
Redis server

#Installation
1. Clone this repository to your local machine.
2. Navigate to the root directory of the project and run "dotnet restore" in the terminal.
3. Update the "appsettings.json" file with your SQL Server connection string and Redis connection string.

Now, run each service separately as follows:

Service A:
dotnet build
dotnet run
Invoke-RestMethod -Uri http://localhost:5002/messages/create -Method Post

Service B:
dotnet build
dotnet run
Invoke-RestMethod -Uri http://localhost:5138/MessageProcessing/Process -Method POST


License
This project is licensed under the MIT License.
