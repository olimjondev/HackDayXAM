using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Challenge2
{
    public static class CreateProduct
    {

        [FunctionName("CreateProduct")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "products")] HttpRequest req,
            [CosmosDB(
                databaseName: "%CosmosDbdatabaseName%",
                collectionName: "%CosmosDbCollectionName%",
                ConnectionStringSetting = "AzureWebJobsCosmosDBConnectionString")] IAsyncCollector<Product> collector,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var data = JsonConvert.DeserializeObject<Product>(await req.ReadAsStringAsync().ConfigureAwait(false));

            await collector.AddAsync(data).ConfigureAwait(false);

            ProductView productView = new ProductView()
            {
                ProductId = data.ProductId,
                ProductName = data.ProductName,
                ProductDescription = data.ProductDescription,
                TimeStamp = DateTime.Now
            };

            return new OkObjectResult(productView);
        }

    }
}
