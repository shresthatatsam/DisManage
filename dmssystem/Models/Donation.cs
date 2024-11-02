﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Donation
    {
        public Guid id {  get; set; }
        public string name { get; set; }
        public string phoneNumber { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public string paymentMethod { get; set; }
        public string amount { get; set; }
        public string? bankName {  get; set; }
        public string? chequeNumber { get; set; }
        public string Type { get; set; }

    }
}
