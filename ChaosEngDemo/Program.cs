// See https://aka.ms/new-console-template for more information

using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

class Program
{
  static async Task Main(string[] args)
  {
      Console.WriteLine("Starting Chaos Engineering Demo with LocalStack + DynamoDB");
      
      // Configure AWS SDK to use LocalStack
      var config = new AmazonDynamoDBConfig
      {
          ServiceURL = "http://localhost:4566",
          UseHttp = true
      };
      var credentials = new BasicAWSCredentials("test", "test");
      var client = new AmazonDynamoDBClient(credentials, config);
      const string tableName = "Products";
      const string itemId = "123";

      try
      {
          // 1. Create Table if it doesn't exist
          var tables = await client.ListTablesAsync();
          if (!tables.TableNames.Contains(tableName))
          {
              Console.WriteLine("Creating table...");

              var createRequest = new CreateTableRequest
              {
                  TableName = tableName,
                  KeySchema = new List<KeySchemaElement>
                  {
                      new KeySchemaElement("Id", KeyType.HASH)
                  },
                  AttributeDefinitions = new List<AttributeDefinition>
                  {
                      new AttributeDefinition("Id", ScalarAttributeType.S)
                  },
                  ProvisionedThroughput = new ProvisionedThroughput
                  {
                      ReadCapacityUnits = 5,
                      WriteCapacityUnits = 5
                  }
              };
              await client.CreateTableAsync(createRequest);
              Console.WriteLine("Table created.");
          }
          // 2. Put Item
          Console.WriteLine("Putting item...");

          var putRequest = new PutItemRequest
          {
              TableName = tableName,
              Item = new Dictionary<string, AttributeValue>
              {
                  { "Id", new AttributeValue { S = itemId } },
                  { "Name", new AttributeValue { S = "Chaos Widget" } },
                  { "Price", new AttributeValue { N = "49.99" } }
              }
          };

          await client.PutItemAsync(putRequest);
          Console.WriteLine("Item inserted.");

          // 3. Get Item
          Console.WriteLine("Getting item...");

          var getRequest = new GetItemRequest
          {
              TableName = tableName,
              Key = new Dictionary<string, AttributeValue>
              {
                  { "Id", new AttributeValue { S = itemId } }
              }
          };

          var getResponse = await client.GetItemAsync(getRequest);

          if (getResponse.Item != null && getResponse.Item.Count > 0)
          {
              Console.WriteLine("🎉 Item retrieved:");
              foreach (var kvp in getResponse.Item)
              {
                  Console.WriteLine($"{kvp.Key}: {kvp.Value.S ?? kvp.Value.N}");
              }
          }
          else
          {
              Console.WriteLine("Item not found.");
          }
      }
      catch (AmazonDynamoDBException ex)
      {
          Console.WriteLine("AWS SDK Exception caught!");
          Console.WriteLine($"Status Code: {ex.StatusCode}");
          Console.WriteLine($"Error Code: {ex.ErrorCode}");
          Console.WriteLine($"Message: {ex.Message}");
      }
      catch (Exception ex)
      {
          Console.WriteLine("Generic Exception caught!");
          Console.WriteLine($"Message: {ex.Message}");
      }

      Console.WriteLine("Chaos Engineering Demo complete!");
  }  
}
