# EShopMicroservices
There is a couple of microservices which implemented **e-commerce** modules over **Catalog, Basket, Discount** and **Ordering** microservices with **NoSQL (DocumentDb, Redis)** and **Relational databases (PostgreSQL, Sql Server)** with communicating over **RabbitMQ Event Driven Communication** and using **Yarp API Gateway**.

## Whats Including In This Repository

#### Catalog microservice which includes; 
* **ASP.NET Core Minimal APIs** and the latest features of .NET 7
* Implementation of **Vertical Slice Architecture** with feature folders and single .cs files containing multiple classes
* CQRS implementation utilizing the **MediatR** library
* CQRS Validation Pipeline Behaviors with MediatR and FluentValidation
* Use Marten library for .NET Transactional Document DB on PostgreSQL
* Utilizing Carter for defining Minimal API endpoints
* Addressing cross-cutting concerns such as logging, global exception handling, and health checks
#### Cart microservice which includes;
* **ASP.NET 7 Web API** application following REST API principles for CRUD operations
* Utilizing **Redis** as a distributed cache in place of basketdb
* Implementation of **Proxy, Decorator**, and **Cache-aside** design patterns
* Consuming a discount **gRPC service** for inter-service synchronous communication to calculate the final product price
* Publishing the BasketCheckout queue using MassTransit and RabbitMQ
  
#### Discount microservice which includes;
* ASP.NET **Grpc Server** application
* Build a Highly Performant **inter-service gRPC Communication** with Basket Microservice
* Exposing Grpc Services with creating **Protobuf messages**
* Entity Framework Core ORM — SQLite Data Provider and Migrations to simplify data access and ensure high performance
* **SQLite database** connection and containerization

#### Microservices Communication
* Sync inter-service **gRPC Communication**
* Async Microservices Communication with **RabbitMQ Message-Broker Service**
* Using **RabbitMQ Publish/Subscribe Topic** Exchange Model
* Using **MassTransit** for abstraction over RabbitMQ Message-Broker system
* Publishing BasketCheckout event queue from Basket microservices and Subscribing this event from Ordering microservices	
* Create **RabbitMQ EventBus.Messages library** and add references Microservices

#### Ordering Microservice
* Implementing **DDD, CQRS, and Clean Architecture** with using Best Practices
* Developing **CQRS with using MediatR, FluentValidation and Mapster packages**
* Consuming **RabbitMQ** BasketCheckout event queue with using **MassTransit-RabbitMQ** Configuration
* **SqlServer database** connection and containerization
* Using **Entity Framework Core ORM** and auto migrate to SqlServer when application startup
#### Notification Microservices
* ASP.NET Core Minimal APIs: Leveraging the latest features of .NET 7 for efficient and lightweight API development.
* CQRS Implementation: Utilizing the MediatR library to handle commands and queries distinctly, improving the readability and scalability of the codebase.
* RabbitMQ Integration: Consuming messages from the Ordering microservice to create notifications, using the MassTransit library for abstraction over RabbitMQ.
* PostgreSQL Database: Utilizing Entity Framework Core ORM for seamless data access and management, with support for migrations to ensure database consistency.
* Notification Creation: Generating notifications based on events received from the Ordering microservice, which can include order confirmations, status updates.
* Carter for Minimal API Endpoints: Using Carter to define endpoints for managing notifications, including retrieval and deletion.
#### Yarp API Gateway Microservice
* Develop API Gateways with **Yarp Reverse Proxy** applying Gateway Routing Pattern
* Yarp Reverse Proxy Configuration; Route, Cluster, Path, Transform, Destinations
* **Rate Limiting** with FixedWindowLimiter on Yarp Reverse Proxy Configuration

#### Docker Compose establishment with all microservices on docker;
* Containerization of microservices
* Containerization of databases
* Override Environment variables

## Run The Project
You will need the following tools:

* [Visual Studio 2022]
* [.Net Core 7 or later]
* [Docker Desktop]

### Installing
Follow these steps to get your development environment set up: (Before Run Start the Docker Desktop)
1. Clone the repository
2. Once Docker for Windows is installed, go to the **Settings > Advanced option**, from the Docker icon in the system tray, to configure the minimum amount of memory and CPU like so:
* **Memory: 4 GB**
* CPU: 2
3. At the root directory of solution, select **docker-compose** and **Set a startup project**. **Run docker-compose without debugging on visual studio**.
  Or you can go to root directory which include **docker-compose.yml** files, run below command:
```csharp
docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d
```

4. Wait for docker compose all microservices. 
