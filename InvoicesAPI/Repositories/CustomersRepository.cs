using InvoicesAPI.Entities;
using InvoicesAPI.Infrastructure;
using InvoicesAPI.Requests;

namespace InvoicesAPI.Repositories
{
    public class CustomersRepository : ICustomersRepository
    {
        private readonly InvoicesContext _customersContext;

        public CustomersRepository(InvoicesContext customersContext)
        {
            _customersContext = customersContext;
        }

        public string CreateCustomer(CreateCustomerRequest customerRequest)
        {
            var existingCustomer = _customersContext.Customers.FirstOrDefault(x => x.IdentityNumber == customerRequest.IdentityNumber);
            if(existingCustomer is null)
            {
                var customer = new Customer
                {
                    CustomerId = Guid.NewGuid().ToString(),
                    FirstName = customerRequest.FirstName,
                    LastName = customerRequest.LastName,
                    City = customerRequest.City,
                    IdentityNumber = customerRequest.IdentityNumber,
                    Street = customerRequest.Street,
                    StreetNumber = customerRequest.StreetNumber,
                    ZipCode = customerRequest.ZipCode
                };
                _customersContext.Customers.Add(customer);
                _customersContext.SaveChanges();
                return customer.CustomerId;
            }
            else
            {
                throw new ArgumentException($"Customer already exists for IdentityNumber: {existingCustomer.IdentityNumber}");
            }
        }

        public Customer GetCustomer(string id)
        {
            return _customersContext.Customers.SingleOrDefault(c => c.CustomerId == id);
        }
    }
}
