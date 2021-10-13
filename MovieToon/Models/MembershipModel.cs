using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieToon.Models
{
    public class MembershipModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
    }
}
