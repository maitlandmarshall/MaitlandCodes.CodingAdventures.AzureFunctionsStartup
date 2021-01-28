using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using MaitlandCodes.CodingAdventures.AzureFunctionsStartup.Services;

namespace MaitlandCodes.CodingAdventures.AzureFunctionsStartup
{
    public class HttpFunction
    {
        private readonly MyInjectedService myInjectedService;

        public HttpFunction(MyInjectedService myInjectedService)
        {
            this.myInjectedService = myInjectedService;
        }

        [FunctionName("Run")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            var service = this.myInjectedService.DoStuff();

            log.LogInformation(service);

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully.";

            return new OkObjectResult(responseMessage);
        }
    }
}
