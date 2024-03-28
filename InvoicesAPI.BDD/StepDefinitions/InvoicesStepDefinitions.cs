using InvoicesAPI.BDD.Contexts;
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
        private readonly WebApplicationFactory<Program> _factory;
        private readonly ApiResponseContext _apiResponseContext;

        public InvoicesStepDefinitions(InvoicesContext invoicesContext, WebApplicationFactory<Program> factory, ApiResponseContext apiResponseContext)
        {
            _invoicesContext = invoicesContext;
            _factory = factory;
            _apiResponseContext = apiResponseContext;
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
            _apiResponseContext.LastApiResponse = await apiClient.PostAsJsonAsync("https://localhost:7277/invoices", request);
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
                Assert.That(invoiceInDb.IssueDate, Is.EqualTo(invoice.IssueDate));
                Assert.That(invoiceInDb.DeliveryDate, Is.EqualTo(invoice.DeliveryDate));
                Assert.That(invoiceInDb.ItemDescription, Is.EqualTo(invoice.ItemDescription));
                Assert.That(invoiceInDb.ItemQuantity, Is.EqualTo(invoice.ItemQuantity));
                Assert.That(invoiceInDb.ItemPrice, Is.EqualTo(invoice.ItemPrice));
                Assert.That(invoiceInDb.TaxRate, Is.EqualTo(invoice.TaxRate));
                Assert.That(invoiceInDb.ItemValue, Is.EqualTo(invoice.ItemValue));
            }
        }
    }
}
