using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class DisasterType
    {
        public Guid Id { get; set; }
        public string DisasterName { get; set; }    
        public string Severity { get; set; }

        public bool Isactive { get; set; } = true;
    }
}
