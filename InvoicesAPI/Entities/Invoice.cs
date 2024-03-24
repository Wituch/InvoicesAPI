using System.ComponentModel.DataAnnotations.Schema;

namespace InvoicesAPI.Entities
{
    public class Invoice
    {
        public string InvoiceId { get; set; }
        public string InvoiceNumber { get; set; }
        [ForeignKey(nameof(Buyer))]
        public string BuyerId { get; set; }
        public Customer Buyer { get; set; } = null!;
        [ForeignKey(nameof(Recipient))]
        public string RecipientId { get; set; }
        public Customer Recipient{ get; set; } = null!;
        public DateTime IssueDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string ItemDescription { get; set; }
        public int ItemQuantity { get; set; }
        public decimal ItemPrice { get; set; }
        public int TaxRate { get; set; }
        public decimal ItemValue { get; set; }
    }
}
