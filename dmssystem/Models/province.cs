using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class province
    {
        public Guid Id { get; set; }
        public string? Province {  get; set; }
        public string? District { get; set; }


        public Guid? ParentProvince { get; set; }
        public Guid? ParentDistrict {  get; set; }
        public string? Municipality { get; set; }
        public string? ward { get; set; }

        public List<Volunteer>? Volunteers { get; set; }

    }
}
