using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Challenge2
{
    public static class GetProducts
    {

        //[FunctionName("GetProduct2")]
        //public static IActionResult Run(
        //    [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "products/{id}")] HttpRequest req,
        //    [CosmosDB(
        //        databaseName: "%CosmosDbdatabaseName%",
        //        collectionName: "%CosmosDbCollectionName%",
        //        ConnectionStringSetting = "AzureWebJobsCosmosDBConnectionString",
        //        Id = "{id}")] Product product,
        //    ILogger log, string id)
        //{
        //    log.LogInformation("C# HTTP trigger function processed a request.");


        //    //string productId = req.Query["productId"];

        //    //string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        //    //dynamic data = JsonConvert.DeserializeObject(requestBody);
        //    //productId = productId ?? data?.name;

        //    return new OkObjectResult(product);
        //}


        [FunctionName("GetProducts")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "products")]
                HttpRequest req,
            [CosmosDB(
                databaseName: "%CosmosDbdatabaseName%",
                collectionName: "%CosmosDbCollectionName%",
                ConnectionStringSetting = "AzureWebJobsCosmosDBConnectionString",
                SqlQuery = "SELECT * FROM products")
                ] IEnumerable<Product> product,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            if (product == null)
            {
                log.LogInformation($"ToDo item not found");
            }
            else
            {
                log.LogInformation($"Found ToDo item, Description={product.FirstOrDefault().ProductDescription}");
            }

            return new OkObjectResult(product);
        }
    }
}
