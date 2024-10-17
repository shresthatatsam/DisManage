using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Image
    {
        public Guid Id { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public DateTime DisasterDate { get; set; }
        public bool isActive { get; set; } = true;

        public DateTime created_at { get; set; } = DateTime.Now;
        public Guid VictimId { get; set; }
        public Victim Victim { get; set; }

        public Guid DisasterId { get; set; }
        public Disaster Disaster { get; set; }
    }
}
