using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project4_EntityFrameworkCodeFirstMovie.DAL.Entities
{
    public class Movie
    {
        public int MovieId { get; set; }
        public string MovieTitle { get; set; }
        public int Duration { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
