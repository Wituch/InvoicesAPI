using InvoicesAPI.Entities;
using InvoicesAPI.Requests;

namespace InvoicesAPI.Repositories
{
    public interface IInvoicesRepository
    {
        Invoice GetInvoice(string id);
        public void CreateInvoice(CreateInvoiceRequest invoiceRequest);
    }
}
