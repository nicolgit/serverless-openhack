using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;


namespace nicold.function
{
    
    
    public static class CreateRating
    {
        private static readonly HttpClient client = new HttpClient();

        [FunctionName("CreateRating")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            [CosmosDB(
                databaseName: "RatingItems",
                collectionName: "Ratings",
                CreateIfNotExists = true,
                ConnectionStringSetting = "CosmosDBConnection")] ICollector<RatingObject> document,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string body = await req.ReadAsStringAsync();
            var rating = JsonConvert.DeserializeObject<RatingObject>(body);

            // validate user
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            string msg="";
            var stringTask = client.GetStringAsync($"https://serverlessohapi.azurewebsites.net/api/GetUser?userId={rating.userId}");
            try 
            {
                msg = await stringTask;
                var user= JsonConvert.DeserializeObject<UserObject>(body);
            } 
            catch
            {
                return new BadRequestObjectResult("User not found");
            }
           
            // validate product
             client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            stringTask = client.GetStringAsync($"https://serverlessohapi.azurewebsites.net/api/GetProduct?productId={rating.productId}");
            try 
            {
                msg = await stringTask;
                var user= JsonConvert.DeserializeObject<UserObject>(body);
            } 
            catch
            {
                return new BadRequestObjectResult("Product not found");
            }

            // validate rating
            if (rating.rating < 1 || rating.rating > 5)
            {
                return new BadRequestObjectResult("Rating must be between 1 and 5");
            }

            rating.id = Guid.NewGuid();
            rating.timestamp = DateTime.Now.ToString();


            //string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            //dynamic data = JsonConvert.DeserializeObject(requestBody);
            //name = name ?? data?.name;
            //string responseMessage = string.IsNullOrEmpty(name)
            //    ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
            //    : $"Hello, {name}. This HTTP triggered function executed successfully.";

            document.Add(rating);
            return new OkObjectResult(JsonConvert.SerializeObject(rating));
        }
    }
}
