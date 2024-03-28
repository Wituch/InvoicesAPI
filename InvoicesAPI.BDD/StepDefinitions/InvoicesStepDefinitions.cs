using InvoicesAPI.BDD.Contexts;
using InvoicesAPI.Entities;
using InvoicesAPI.Infrastructure;
using InvoicesAPI.Requests;
using Microsoft.AspNetCore.Mvc.Testing;
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
            table.CompareToSet<Invoice>(_invoicesContext.Invoices);
        }
    }
}
