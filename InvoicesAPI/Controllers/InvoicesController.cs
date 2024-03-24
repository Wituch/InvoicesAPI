﻿using InvoicesAPI.Repositories;
using InvoicesAPI.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace InvoicesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InvoicesController
    {
        private readonly IInvoicesRepository _invoicesRepository;

        public InvoicesController(IInvoicesRepository invoicesRepository)
        {
            _invoicesRepository = invoicesRepository;
        }

        [HttpGet]
        public IResult Get(string invoiceId)
        {
            var result = _invoicesRepository.GetInvoice(invoiceId);
            return result == null ? Results.NotFound() : Results.Ok(result);
        }

        [HttpPost]
        public IResult Post([FromBody] CreateInvoiceRequest invoice)
        {
            try
            {
                _invoicesRepository.CreateInvoice(invoice);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }

            return Results.Ok();
        }
    }
}
