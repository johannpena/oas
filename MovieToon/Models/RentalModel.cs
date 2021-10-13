using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MovieToon.Models
{
    public class RentalModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [NotMapped]
        public List<int> Movies { get; set; }

        public CustomerModel Customer { get; set; }
        public ICollection<RentalMovieModel> RentalMovies { get; set; }
    }
}
