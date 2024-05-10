# Distributed ECommerce App

This project is a .NET Core API for simulate real-life e-commerce system.

## Services 
- **Commerce.Service:** Microservice that contains product-category-coupon-comment structures across e-commerce in our main application and where we initiate the payment process.
- **Email.Service:** Microservice that creates HTML content specific to Email Type at runtime using Reflection and is responsible for sending it.
- **Identity.Service:** Microservice responsible for authentication operations such as logging into the system, registering, and forgetting my password.
- **Invoice.Service:** Microservice responsible for creating a record with information about the payment to MongoDb in case of each payment.
- **Payment.Service:** FMicroservice responsible for publishing the response returned from the bank service connected to the Bank ID sent using the factory Design pattern as an event.
- **Stock.Service:** Microservice that allows stock information to be kept here when the product is entered in the main application.
- **API.Gateway:** A microservice that acts as a Gateway that allows Identity and Commerce services to be routed through a single URL on the Backoffice side.

### Service Communication

All Microservices has been used rabbitmq via Masstransit for service communication. 

Saga choreography pattern has been used as Event communication pattern.

## Running the Project

To run the project, you will need to have [Docker](https://www.docker.com/) installed on your machine.

1.  Navigate to the project directory:


`cd distributed.ecommerce.app` 

2.  Build and start the Docker containers:


`docker-compose up -d` 

This will start the API and RabbitMQ instances in the background.

## API Documentation

You can view the API documentation and test the endpoints using the Swagger UI at `http://localhost:8080/swagger`.

### RabbitMQ Management

You can manage the RabbitMQ instance using the RabbitMQ Management UI at `http://localhost:15672`. The default credentials are `guest:guest`.

### Consul Management

You can manage the Service Discovery using the Consul UI at `http://localhost:8500`.

I hope this helps! Let me know if you have any questions.
