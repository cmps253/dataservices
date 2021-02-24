using Cmps253.Spring2021.DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Linq;

namespace CloudCustomerRepo
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully.";

            return new OkObjectResult(responseMessage);
        }

        [FunctionName("GetCustomers")]
        public static async Task<IActionResult> GetCustomers([HttpTrigger(AuthorizationLevel.Function, "get", Route = "customers")] HttpRequest req, ILogger log)
        {

            var customerRepo = new CustomerRepo(new ConnectionString());
            string lastName = req.Query["ln"];
            if (string.IsNullOrWhiteSpace(lastName))
            {
                return new OkObjectResult(customerRepo.GetCustomers());
            }
            else
            {
                return new OkObjectResult(customerRepo.GetCustomerByLastName(lastName));
            }

        }

        [FunctionName("GetCustomerById")]
        public static async Task<IActionResult> GetCustomerById([HttpTrigger(AuthorizationLevel.Function, "get", Route = "customers/{id}")] HttpRequest req, string id, ILogger log)
        {
            var customerRepo = new CustomerRepo(new ConnectionString());
            Customer customer = customerRepo.GetCustomerById(int.Parse(id));
            return new OkObjectResult(customer);
        }

        [FunctionName("DeleteCustomer")]
        public static async Task<IActionResult> DeleteCustomer([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "customers/{id}")] HttpRequest req, string id, ILogger log)
        {
            var customerRepo = new CustomerRepo(new ConnectionString());
            customerRepo.Delete(int.Parse(id));
            return new OkObjectResult("ok");
        }

        [FunctionName("InsertCustomer")]
        public static async Task<IActionResult> InsertCustomer([HttpTrigger(AuthorizationLevel.Function, "post", Route = "customers")] HttpRequest req, ILogger log)
        {
            string json = await new StreamReader(req.Body).ReadToEndAsync();
            Customer c = Newtonsoft.Json.JsonConvert.DeserializeObject<Customer>(json);
            var customerRepo = new CustomerRepo(new ConnectionString());
            customerRepo.Insert(c);
            return new OkObjectResult("ok");
        }
    }
}