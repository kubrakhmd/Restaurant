using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Domain.Models
{
    public class QR : BaseEntity
    {
        public string? Content { get; set; }  // This will store the URL or data for the QR code
        public string QRCodeData { get; set; } = "";  // Base64 encoded QR code image
        public int? MenuId { get; set; }
        public Menu? Menu { get; set; }

        public string ProductType { get; set; } = "";

        public int ProductId { get; set; }

        public Category? Category { get; set; }
    }
}
