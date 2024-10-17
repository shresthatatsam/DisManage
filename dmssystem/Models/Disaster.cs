using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Disaster
    {
        public Guid Id { get; set; }
        public string DisasterType { get; set; }
        public string Description { get; set; }
        public DateTime DateReported { get; set; }

        public DateTime created_at { get; set; } = DateTime.Now;
        public bool isActive { get; set; } = true;
        // One-to-many relationship: A disaster can have many images
        public ICollection<Image> Images { get; set; } = new List<Image>();



        public Guid VictimId { get; set; }
        public Victim Victim { get; set; }
    }
}
