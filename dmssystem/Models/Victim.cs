using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Victim
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Nationality { get; set; }

        public string? PassportNumber { get; set; }
        
        public string Age { get; set; }
        public string Gender { get; set; }
        public string ContactNumber { get; set; }
        public string? Email { get; set; }

        public string? PPSizePhoto { get; set; }
        public string? CitizenshipNumber { get; set; }

        public string? Secret_Number {  get; set; }
        public string? NIDNumber { get; set; }
        public string? FatherName { get; set; }
        public string? MotherName { get; set; }
        public string? GrandFatherName { get; set; }

        public bool isActive { get; set; } =true;

        public DateTime created_at { get; set; } = DateTime.Now;

        public Guid LocationId { get; set; } // Foreign Key to Location
        public Location? Location { get; set; }

        // One-to-one relationship with Disaster
        public Guid? DisasterId { get; set; } // Foreign Key to Disaster
        public Disaster? Disaster { get; set; }

  

        // One-to-many relationship with DisasterImage
        public ICollection<Image>? Images { get; set; } = new List<Image>();
    }
}
