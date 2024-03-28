using InvoicesAPI.Repositories;
using InvoicesAPI.Requests;
using Microsoft.AspNetCore.Mvc;

namespace InvoicesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersController
    {
        private readonly ICustomersRepository _customersRepository;

        public CustomersController(ICustomersRepository customersRepository)
        {
            _customersRepository = customersRepository;
        }

        [HttpGet]
        public IResult Get(string customerId)
        {
            var result = _customersRepository.GetCustomer(customerId);
            return result == null ? Results.NotFound() : Results.Ok(result);
        }

        [HttpPost]
        public IResult Post([FromBody] CreateCustomerRequest customer)
        {
            string customerId;
            try
            {
                customerId = _customersRepository.CreateCustomer(customer);
            }
            catch (Exception ex) 
            { 
                return Results.BadRequest(ex.Message);
            }

            return Results.Ok(customerId);
        }
    }
}
