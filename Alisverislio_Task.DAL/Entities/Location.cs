using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alisverislio_Task.DAL.Entities
{
    public class Location
    {
        public int Id { get; set; }
        public string Shelf { get; set; }
        public string Aisle { get; set; }
        public string Floor { get; set; }
        public string Section { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
