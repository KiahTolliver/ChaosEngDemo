# Chaos Engineering Demo with LocalStack and DynamoDB

This project demonstrates a simple chaos engineering experiment using LocalStack to simulate AWS DynamoDB.  The code creates a DynamoDB table, inserts an item, and then retrieves the item.  It also includes error handling to demonstrate how to catch potential exceptions during DynamoDB operations.

## Prerequisites

* **LocalStack:** Make sure you have LocalStack running.  You can install and run LocalStack using Docker (See the [LocalStack documentation](https://localstack.cloud/) for installation instructions and other options).
* **AWS SDK for .NET:**  Install the `AWSSDK.DynamoDBv2` NuGet package in your project.
* **.NET SDK:** Ensure you have the .NET SDK installed.


## Running the Demo

1. **Start LocalStack:** Ensure LocalStack is running locally as described in the Prerequisites.

2. **Build and Run:** Build and run the C# project. The application will:
    * Connect to your local DynamoDB instance running in LocalStack.
    * Create a table named "Products" if it doesn't already exist.
    * Insert an item with ID "123" into the table.
    * Retrieve the item and print its attributes to the console.
    * Handle any exceptions that may occur during these operations.

## Key Concepts

* **LocalStack:**  Provides a local AWS cloud stack for development and testing.  This allows you to run the demo without needing a real AWS account.
* **DynamoDB:**  A NoSQL database service provided by AWS. This demo showcases basic DynamoDB operations.
* **Chaos Engineering:**  While this demo doesn't introduce explicit chaos, it forms the foundation for chaos experiments.  You can extend this code to simulate failures, outages, or latency issues and observe how your application behaves under stress.  (See below for ideas on extending this project for chaos experiments).

## Extending for Chaos Experiments

Here are some ideas to extend this demo for more realistic chaos engineering experiments:

* **Simulate Latency:** Introduce artificial latency in the DynamoDB calls to simulate network delays.
* **Simulate Errors:** Inject errors (e.g., throttling exceptions, connection errors) to test your application's resilience.
* **Data Corruption:**  Introduce data corruption scenarios to see how your application handles unexpected data.


## Error Handling

The code includes error handling using `try-catch` blocks to catch `AmazonDynamoDBException` and generic `Exception` objects. This demonstrates best practices for handling potential issues when interacting with DynamoDB.