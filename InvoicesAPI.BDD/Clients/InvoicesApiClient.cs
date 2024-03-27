using InvoicesAPI.Entities;
using InvoicesAPI.Requests;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow.CommonModels;

namespace InvoicesAPI.BDD.Clients
{
    public class InvoicesApiClient
    {
        private readonly HttpClient _httpClient;

        public InvoicesApiClient()
        {
            _httpClient = new HttpClient();
        }

        public void SetBaseAdress(string baseAdress)
        {
            _httpClient.BaseAddress = new Uri(baseAdress);
        }

        public async Task<HttpResponseMessage> GetCustomer(string customerId)
        {
            return await _httpClient.GetAsync($"/customers?customerId={customerId}");
        }

        public async Task<HttpResponseMessage> CreateCustomer(CreateCustomerRequest request)
        {
            return await _httpClient.PostAsJsonAsync("/customers", request);
        }

        public async Task<HttpResponseMessage> CreateInvoice(CreateInvoiceRequest request)
        {
            return await _httpClient.PostAsJsonAsync("/invoices", request);
        }
    }
}
