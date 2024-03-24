using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace InvoicesAPI.Entities
{
    public class Customer
    {
        public string CustomerId { get; set; }
        public string IdentityNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StreetNumber { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }


        [InverseProperty(nameof(Invoice.Buyer))]
        public ICollection<Invoice> InvoicesAsBuyer { get; set; }

        [InverseProperty(nameof(Invoice.Recipient))]
        public ICollection<Invoice> InvoicesAsRecipient { get; set; }
    }
}
