using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class JinsiDonation
    {
        public Guid id { get; set; }
        public string jname { get; set; }
        public string? jCost { get; set; }
        public string jBrand { get; set; }
        public double jQuantity { get; set; }
        public string jKaifayat { get; set; }
        public string jSource { get; set; }
        public Guid? DonationId { get; set; } // Foreign Key to Disaster
        public Donation? Donation { get; set; }
    }
}
