using InvoicesAPI.Repositories;
using InvoicesAPI.Requests;
using Microsoft.AspNetCore.Mvc;

namespace InvoicesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomersRepository _customersRepository;

        public CustomersController(ICustomersRepository customersRepository)
        {
            _customersRepository = customersRepository;
        }

        [HttpGet]
        public IActionResult Get(string customerId)
        {
            var result = _customersRepository.GetCustomer(customerId);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreateCustomerRequest customer)
        {
            string customerId;
            try
            {
                customerId = _customersRepository.CreateCustomer(customer);
            }
            catch (Exception ex) 
            { 
                return BadRequest(ex.Message);
            }

            return Ok(customerId);
        }
    }
}
