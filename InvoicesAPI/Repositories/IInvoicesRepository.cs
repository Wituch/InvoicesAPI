using InvoicesAPI.Entities;
using InvoicesAPI.Requests;

namespace InvoicesAPI.Repositories
{
    public interface IInvoicesRepository
    {
        Invoice GetInvoice(string id);
        string CreateInvoice(CreateInvoiceRequest invoiceRequest);
    }
}
