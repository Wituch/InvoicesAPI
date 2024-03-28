using InvoicesAPI.Entities;
using InvoicesAPI.Infrastructure;
using InvoicesAPI.Requests;

namespace InvoicesAPI.Repositories
{
    public class InvoicesRepository : IInvoicesRepository
    {
        private readonly InvoicesContext _invoicesContext;

        public InvoicesRepository(InvoicesContext invoicesContext)
        {
            _invoicesContext = invoicesContext;
        }

        public string CreateInvoice(CreateInvoiceRequest invoiceRequest)
        {
            if(_invoicesContext.Invoices.FirstOrDefault(i => i.InvoiceNumber == invoiceRequest.InvoiceNumber) is null)
            {
                if(_invoicesContext.Customers.FirstOrDefault(c => c.CustomerId == invoiceRequest.BuyerId) is null)
                {
                    throw new ArgumentException($"Buyer with id: {invoiceRequest.BuyerId} does not exists");
                }
                if (_invoicesContext.Customers.FirstOrDefault(c => c.CustomerId == invoiceRequest.RecipientId) is null)
                {
                    throw new ArgumentException($"Recipient with id: {invoiceRequest.RecipientId} does not exists");
                }

                var invoice = new Invoice
                {
                    InvoiceId = Guid.NewGuid().ToString(),
                    InvoiceNumber = invoiceRequest.InvoiceNumber,
                    BuyerId = invoiceRequest.BuyerId,
                    RecipientId = invoiceRequest.RecipientId,
                    DeliveryDate = invoiceRequest.DeliveryDate,
                    IssueDate = invoiceRequest.IssueDate,
                    ItemDescription = invoiceRequest.ItemDescription,
                    ItemPrice = invoiceRequest.ItemPrice,
                    ItemQuantity = invoiceRequest.ItemQuantity,
                    ItemValue = invoiceRequest.ItemValue,
                    TaxRate = invoiceRequest.TaxRate
                };

                _invoicesContext.Add(invoice);
                _invoicesContext.SaveChanges();
                return invoice.InvoiceId;
            }
            else
            {
                throw new ArgumentException($"Invoice already exists for InvoiceNumber: {invoiceRequest.InvoiceNumber}");
            }
        }

        public Invoice GetInvoice(string id)
        {
            return _invoicesContext.Invoices.SingleOrDefault(c => c.InvoiceId == id);
        }
    }
}
