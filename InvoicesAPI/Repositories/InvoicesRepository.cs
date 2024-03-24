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

        public void CreateInvoice(CreateInvoiceRequest invoiceRequest)
        {
            var existingInvoice = _invoicesContext.Invoices.FirstOrDefault(i => i.InvoiceNumber == invoiceRequest.InvoiceNumber);
            if(existingInvoice is null)
            {
                var buyer = _invoicesContext.Customers.FirstOrDefault(c => c.CustomerId == invoiceRequest.BuyerId);
                if(buyer is null)
                {
                    throw new ArgumentException($"Buyer with id: {invoiceRequest.BuyerId} does not exists");
                }
                var receipient = _invoicesContext.Customers.FirstOrDefault(c => c.CustomerId == invoiceRequest.RecipientId);
                if (receipient is null)
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
            }
            else
            {
                throw new ArgumentException($"Invoice already exists for InvoiceNumber: {existingInvoice.InvoiceNumber}");
            }
        }

        public Invoice GetInvoice(string id)
        {
            return _invoicesContext.Invoices.SingleOrDefault(c => c.InvoiceId == id);
        }
    }
}
