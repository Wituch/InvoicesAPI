using InvoicesAPI.Repositories;
using InvoicesAPI.Requests;
using Microsoft.AspNetCore.Mvc;

namespace InvoicesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")] 
    public class InvoicesController : ControllerBase
    {
        private readonly IInvoicesRepository _invoicesRepository;

        public InvoicesController(IInvoicesRepository invoicesRepository)
        {
            _invoicesRepository = invoicesRepository;
        }

        [HttpGet]
        public IActionResult Get(string invoiceId)
        {
            var result = _invoicesRepository.GetInvoice(invoiceId);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreateInvoiceRequest invoice)
        {
            string invoiceId;
            try
            {
                invoiceId = _invoicesRepository.CreateInvoice(invoice);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(invoiceId);
        }
    }
}
