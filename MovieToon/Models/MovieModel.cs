using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieToon.Models
{
    public class MovieModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int MovieClasificationId { get; set; }
        public int MovieCategoryId { get; set; }

        public MovieCategoryModel MovieCategory { get; set; }
        public MovieClasificationModel MovieClasification { get; set; }
    }
}
