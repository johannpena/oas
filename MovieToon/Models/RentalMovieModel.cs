using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieToon.Models
{
    public class RentalMovieModel
    {
        public int Id { get; set; }
        public int RentalId { get; set; }
        public int MovieId { get; set; }
        public decimal Price { get; set; }

        public RentalModel Rental { get; set; }
        public MovieModel Movie { get; set; }
    }
}
