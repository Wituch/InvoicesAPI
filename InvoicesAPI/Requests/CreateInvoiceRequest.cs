namespace InvoicesAPI.Requests
{
    public class CreateInvoiceRequest
    {
        public string InvoiceNumber { get; set; }
        public string BuyerId { get; set; }
        public string RecipientId { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string ItemDescription { get; set; }
        public int ItemQuantity { get; set; }
        public decimal ItemPrice { get; set; }
        public int TaxRate { get; set; }
        public decimal ItemValue { get; set; }
    }
}
