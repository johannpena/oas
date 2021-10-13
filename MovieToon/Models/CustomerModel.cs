using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieToon.Models
{
    public class CustomerModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DNI { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int MembershipId { get; set; }

        public MembershipModel Membership { get; set; }
        public ICollection<RentalModel> Rentals { get; set; }
    }
}
