# MessagesProcessing
This project is a microservices architecture consisting of multiple ASP .NET Core Web APIs each service with its own task:

ServiceA saves messages with random numbers to a MSSQL database, ServiceB retrieves messages from the database, processes them (doubles the numbers), and stores the results in Redis, and ServiceC retrieves the processed numbers from Redis and displays them in a terminal.


Prerequisites

Before running this project, ensure you have the following installed:

.NET SDK (version 8.0.6)
SQL Server
Redis
Terminal

Installation

1. Clone this repository to your local machine.
2. Navigate to the root directory of the project and run "dotnet restore" in the terminal.
3. Update the "appsettings.json" in each of the services file with your SQL Server connection string and Redis connection string. Services A and B need the SQL connection string and Services B and C need the Redis Connection string. Update the connection strings currently showing in the appsettings.json files respectively.

Now, run each service separately as follows:

Service A:

Open a new terminal window and cd into ServiceA.

Run the following commands:

dotnet build

dotnet run

In another terminal cd into ServiceA again and run:

Invoke-RestMethod -Uri http://localhost:5002/messages/create -Method Post (replace the port respectively)


Service B:

Open a new terminal window and cd into ServiceB.

Run the following commands:

dotnet build

dotnet run

In another terminal cd into ServiceB again and run:

Invoke-RestMethod -Uri http://localhost:5138/MessageProcessing/Process -Method POST (replace the port respectively)


Service C:

Open a new termianl window and cd into ServiceC.

Run the following commands:

dotnet build

dotnet run

License: 
This project is licensed under the MIT License.
