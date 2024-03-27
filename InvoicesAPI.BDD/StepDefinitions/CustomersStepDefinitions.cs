using InvoicesAPI.BDD.Clients;
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
        private HttpResponseMessage _lastApiResponse = null;

        public CustomersStepDefinitions(InvoicesContext invoicesContext, InvoicesApiClient apiClient, ScenarioContext scenarioContext)
        {
            _invoicesContext = invoicesContext;
            _apiClient = apiClient;
            _apiClient.SetBaseAdress("https://localhost:7277/");
            _scenarioContext = scenarioContext;
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
            _lastApiResponse = await _apiClient.GetCustomer(customerId);
        }

        [When("Get customer request is sent with new customer id")]
        public async Task GetCustomerRequestIsSentWithNewCustomerId()
        {
            var customerId = (string)_scenarioContext["newCustomerId"];
            _lastApiResponse = await _apiClient.GetCustomer(customerId);
        }

        [When("Create customer request is sent with following properties:")]
        public async Task CreateCustomerRequestIsSentWithFollowingProperties(Table table)
        {
            var request = table.CreateInstance<CreateCustomerRequest>();
            _lastApiResponse = await _apiClient.CreateCustomer(request);
            if(_lastApiResponse.IsSuccessStatusCode)
            {
                var newCustomerId = await _lastApiResponse.Content.ReadAsStringAsync();
                _scenarioContext["newCustomerId"] = newCustomerId.Replace("\"", "");
            }
        }

        [StepDefinition("Following customers are saved in the database:")]
        public void FollowingCustoemrsAreSavedInTheDatabase(Table table)
        {
            var customers = table.CreateSet<Customer>();
            foreach(var customer in customers)
            {
                var customerInDb = _invoicesContext.Customers.FirstOrDefault(c => c.IdentityNumber == customer.IdentityNumber);
                Assert.That(customerInDb, Is.Not.Null);
                Assert.That(customerInDb.FirstName, Is.EqualTo(customer.FirstName));
                Assert.That(customerInDb.LastName, Is.EqualTo(customer.LastName));
                Assert.That(customerInDb.StreetNumber, Is.EqualTo(customer.StreetNumber));
                Assert.That(customerInDb.Street, Is.EqualTo(customer.Street));
                Assert.That(customerInDb.City, Is.EqualTo(customer.City));
                Assert.That(customerInDb.ZipCode, Is.EqualTo(customer.ZipCode));
            }
        }

        [Given("There are following customers in the database:")]
        public void ThereAreFollowingCustomersInTheDatabase(Table table)
        {
            GivenThereAreNoCustomersInTheDatabase();
            var customers = table.CreateSet<Customer>();
            _invoicesContext.Customers.AddRange(customers);
            _invoicesContext.SaveChanges();
        }

        [Then("Response status code is (.*)")]
        public void ResponseStatusCodeIs(int statusCode)
        {
            Assert.That(statusCode, Is.EqualTo((int)_lastApiResponse.StatusCode));
        }
    }
}
