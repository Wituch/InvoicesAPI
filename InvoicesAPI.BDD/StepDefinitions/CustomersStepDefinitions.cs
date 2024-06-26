using InvoicesAPI.BDD.Clients;
using InvoicesAPI.BDD.Contexts;
using InvoicesAPI.Entities;
using InvoicesAPI.Infrastructure;
using InvoicesAPI.Requests;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using TechTalk.SpecFlow.Assist;

namespace InvoicesAPI.BDD.StepDefinitions
{
    [Binding]
    public sealed class CustomersStepDefinitions
    {
        private readonly InvoicesContext _invoicesContext;
        private readonly InvoicesApiClient _apiClient;
        private readonly ScenarioContext _scenarioContext;
        private readonly ApiResponseContext _apiResponseContext;

        public CustomersStepDefinitions(InvoicesContext invoicesContext, InvoicesApiClient apiClient, ScenarioContext scenarioContext, ApiResponseContext apiResponseContext)
        {
            _invoicesContext = invoicesContext;
            _apiClient = apiClient;
            _apiClient.SetBaseAdress("https://localhost:7277/");
            _scenarioContext = scenarioContext;
            _apiResponseContext = apiResponseContext;
        }
        
        [Given("There are no customers in the database")]
        public void GivenThereAreNoCustomersInTheDatabase()
        {
            _invoicesContext.Database.ExecuteSqlRaw("DELETE FROM [Invoices]; DELETE FROM [Customers]");
            _invoicesContext.SaveChanges();
        }

        [When("Get customer request is sent with customer id (.*)")]
        public async Task GetCustomerRequestIsSentWithCustomerId(string customerId)
        {
            _apiResponseContext.LastApiResponse = await _apiClient.GetCustomer(customerId);
        }

        [When("Get customer request is sent with new customer id")]
        public async Task GetCustomerRequestIsSentWithNewCustomerId()
        {
            var customerId = (string)_scenarioContext["newCustomerId"];
            _apiResponseContext.LastApiResponse = await _apiClient.GetCustomer(customerId);
        }

        [When("Create customer request is sent with following properties:")]
        public async Task CreateCustomerRequestIsSentWithFollowingProperties(Table table)
        {
            var request = table.CreateInstance<CreateCustomerRequest>();
            _apiResponseContext.LastApiResponse = await _apiClient.CreateCustomer(request);

            if(_apiResponseContext.LastApiResponse.IsSuccessStatusCode)
            {
                var newCustomerId = await _apiResponseContext.LastApiResponse.Content.ReadAsStringAsync();
                _scenarioContext["newCustomerId"] = newCustomerId.Replace("\"", "");
            }
        }

        [Then("Response status code is (.*)")]
        public void ResponseStatusCodeIs(int statusCode)
        {
            Assert.That((int)_apiResponseContext.LastApiResponse.StatusCode, Is.EqualTo(statusCode));
        }

        [StepDefinition("Following customers are saved in the database:")]
        public void FollowingCustomersAreSavedInTheDatabase(Table table)
        {
            table.CompareToSet<Customer>(_invoicesContext.Customers);
        }

        [Given("There are following customers in the database:")]
        public void ThereAreFollowingCustomersInTheDatabase(Table table)
        {
            var customers = table.CreateSet<Customer>();
            _invoicesContext.Database.ExecuteSqlRaw("DELETE FROM [Invoices]; DELETE FROM [Customers]");
            _invoicesContext.Customers.AddRange(customers);
            _invoicesContext.SaveChanges();
        }
    }
}
