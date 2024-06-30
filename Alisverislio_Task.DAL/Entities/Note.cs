using Alisverislio_Task.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alisverislio_Task.DAL.Entities
{
    public  class Note
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public Visibility Visibility { get; set; } 
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<Share> Shares { get; set; } 
    }
}
