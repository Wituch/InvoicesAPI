using InvoicesAPI.Entities;
using InvoicesAPI.Requests;

namespace InvoicesAPI.Repositories
{
    public interface ICustomersRepository
    {
        Customer GetCustomer(string id);
        string CreateCustomer(CreateCustomerRequest customerRequest);
    }
}
