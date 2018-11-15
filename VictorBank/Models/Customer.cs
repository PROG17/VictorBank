using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VictorBank.Models
{
    public class Customer
    {
        public int CustomerNumber { get; set; }
        public string Name { get; set; }
        public Account Account { get; set; }
    }
}
