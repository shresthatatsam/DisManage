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
        public string name { get; set; }
        public string? Cost { get; set; }
        public string Brand { get; set; }
        public double Quantity { get; set; }
        public string Kaifayat { get; set; }
        public string Source { get; set; }
    }
}
