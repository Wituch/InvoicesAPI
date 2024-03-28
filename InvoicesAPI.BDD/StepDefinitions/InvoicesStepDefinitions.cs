using InvoicesAPI.Entities;
using InvoicesAPI.Infrastructure;
using InvoicesAPI.Requests;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Net.Http.Json;
using TechTalk.SpecFlow.Assist;

namespace InvoicesAPI.BDD.StepDefinitions
{
    [Binding]
    public sealed class InvoicesStepDefinitions
    {
        private readonly InvoicesContext _invoicesContext;
        private readonly ScenarioContext _scenarioContext;
        private HttpResponseMessage _lastApiResponse;
        private readonly WebApplicationFactory<Program> _factory;

        public InvoicesStepDefinitions(InvoicesContext invoicesContext, ScenarioContext scenarioContext, WebApplicationFactory<Program> factory)
        {
            _invoicesContext = invoicesContext;
            _scenarioContext = scenarioContext;
            _factory = factory;
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
            var apiClient = _factory.CreateClient();
            var request = table.CreateInstance<CreateInvoiceRequest>();
            _lastApiResponse = await apiClient.PostAsJsonAsync("https://localhost:7277/invoices", request);
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
