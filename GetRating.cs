using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace nicold.function
{
    public static class GetRating
    {
        [FunctionName("GetRating")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            [CosmosDB(
                databaseName: "RatingItems",
                collectionName: "Ratings",
                ConnectionStringSetting = "CosmosDBConnection",
                Id = "{Query.ratingId}",
                PartitionKey = "{Query.userId}"
                )] RatingObject rating,            
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            if (rating == null)
            {
                return new NotFoundResult();
            }
            
            return new OkObjectResult(rating);
        }
    }
}
