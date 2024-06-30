using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alisverislio_Task.DAL.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string ISBN { get; set; }
        public int LocationId { get; set; }
        public Location Location { get; set; }
        public string ImageUrl { get; set; }
        public ICollection<Note> Notes { get; set; } 
        public ICollection<Purchase> Purchases { get; set; } 
    }
}
