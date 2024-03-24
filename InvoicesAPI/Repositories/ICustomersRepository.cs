using InvoicesAPI.Entities;
using InvoicesAPI.Requests;

namespace InvoicesAPI.Repositories
{
    public interface ICustomersRepository
    {
        Customer GetCustomer(string id);
        public void CreateCustomer(CreateCustomerRequest customerRequest);
    }
}
