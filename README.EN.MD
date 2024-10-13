This repository contains a collection of .NET solutions for managing motorcycles, rentals, delivery personnel, and notifications. The structure is based on microservices, with a gateway expositor (BFFService) that centralizes the service routes.

## Solution Structure

### RentalMotorcycle

- **RentalMotorcycle**: Main project for the motorcycle rental application.
- **RentalMotorcycle.Application**: Contains handlers and commands for rental operations.
- **RentalMotorcycle.Domain**: Contains domain models.
- **RentalMotorcycle.Infrastructure**: Contains repository implementations and database context.

### MotorcycleService

- **MotorcycleService**: Main project for the motorcycle management application.
- **MotorcycleService.Application**: Contains handlers and commands for motorcycle operations.
- **MotorcycleService.Domain**: Contains domain models.
- **MotorcycleService.Infrastructure**: Contains repository implementations and database context.

### DeliveryPilots

- **DeliveryPilots**: Main project for the delivery personnel management application.
- **DeliveryPilots.Application**: Contains handlers and commands for delivery personnel operations.
- **DeliveryPilots.Domain**: Contains domain models.
- **DeliveryPilots.Infrastructure**: Contains repository implementations and database context.

### NotificationService

- **NotificationService**: Main project for the notification application.
- **NotificationService.Service**: Contains notification services.
- **NotificationService.Domain**: Contains domain models.
- **NotificationService.Infrastructure**: Contains repository implementations and database context.

### BFFService

The BFFService acts as a gateway expositor for the above microservices, centralizing the service routes.

## Controller Routes

### MotosController

- **POST /motos**: Adds a new motorcycle.
- **GET /motos**: Retrieves all motorcycles.
- **PUT /motos/{id}/placa**: Updates the license plate of a motorcycle.
- **GET /motos/{id}**: Retrieves a motorcycle by ID.
- **DELETE /motos/{id}**: Deletes a motorcycle by ID.

### LocaçãoController

- **POST /locacao**: Adds a new rental.
- **GET /locacao/{id}**: Retrieves a rental by ID.
- **PUT /locacao/{id}/devolucao**: Updates the return date of a rental.

### EntegadoresController

- **POST /entregadores**: Registers a new delivery person.
- **POST /entregadores/{id}/cnh**: Adds the driver's license image of a delivery person.

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker](https://www.docker.com/get-started)
- [PostgreSQL](https://www.postgresql.org/download/)
- [RabbitMQ](https://www.rabbitmq.com/download.html)

## Configuration

### Database

Ensure that PostgreSQL is installed and configured correctly. Update the connection string in the `appsettings.json` file as necessary.

### Docker

Use Docker to facilitate the configuration and execution of the services.

### RabbitMQ

Configure RabbitMQ to manage the message queues between services.

## Running the Application

1. Clone the repository.
2. Configure the `appsettings.json` files as necessary.
3. Run the commands `dotnet build` and `dotnet run` to start the services.

## Testing

Each solution contains unit tests to ensure code quality. Run the tests with the command `dotnet test`.