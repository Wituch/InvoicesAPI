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
    public sealed class InvoicesStepDefinitions
    {
        private readonly InvoicesContext _invoicesContext;
        private readonly InvoicesApiClient _apiClient;
        private readonly ScenarioContext _scenarioContext;
        private HttpResponseMessage _lastApiResponse;

        public InvoicesStepDefinitions(InvoicesContext invoicesContext, InvoicesApiClient apiClient, ScenarioContext scenarioContext)
        {
            _invoicesContext = invoicesContext;
            _apiClient = apiClient;
            _apiClient.SetBaseAdress("https://localhost:7277/");
            _scenarioContext = scenarioContext;
        }

        [Given("There are no invoices in the database")]
        public void GivenThereAreNoInvoicesInTheDatabase()
        {
            _invoicesContext.Database.ExecuteSqlRaw("DELETE FROM [Invoices];");
            _invoicesContext.SaveChanges();
        }

        [When("Create invoice request is sent with following properties:")]
        public async Task CreateInvoiceRequestIsSentWithFollowingProperties(Table table)
        {
            var request = table.CreateInstance<CreateInvoiceRequest>();
            _lastApiResponse = await _apiClient.CreateInvoice(request);
        }

        [Then("Last response status code is (.*)")]
        public void LastResponseStatusCodeIs(int statusCode)
        {
            Assert.That(statusCode, Is.EqualTo((int)_lastApiResponse.StatusCode));
        }

        [StepDefinition("Following invoices are saved in the database:")]
        public void FollowingInvoicesAreSavedInTheDatabase(Table table)
        {
            var invoices = table.CreateSet<Invoice>();
            foreach (var invoice in invoices)
            {
                var invoiceInDb = _invoicesContext.Invoices.FirstOrDefault(i => i.InvoiceNumber == invoice.InvoiceNumber);
                Assert.That(invoiceInDb, Is.Not.Null);
                Assert.That(invoiceInDb.BuyerId, Is.EqualTo(invoice.BuyerId));
                Assert.That(invoiceInDb.RecipientId, Is.EqualTo(invoice.RecipientId));
            }
        }
    }
}
