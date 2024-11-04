using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Fingerprint
    {
        public int Id { get; set; } // Assuming this is the primary key in your database
        public byte[] FingerprintData { get; set; } // Storing fingerprint data
        public Guid UserId { get; set; }

        public string? PhoneNumber {  get; set; }
        public string? Email {  get; set; }
    }
}
