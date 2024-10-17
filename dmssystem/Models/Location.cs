using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Location
    {
        public Guid Id { get; set; }

        public string PermanentProvince { get; set; }
        public string PermanentDistrict { get; set; }
        public string PermanentMunicipality { get; set; }
        public string PermanentTole { get; set; }

        public string TemporaryProvince { get; set; }
        public string TemporaryDistrict { get; set; }
        public string TemporaryMunicipality { get; set; }
        public string TemporaryTole { get; set; }

        public bool isActive { get; set; } = true;

        public DateTime created_at { get; set; } = DateTime.Now;
        public Guid VictimId { get; set; }
        public Victim Victim { get; set; }
    }
}
