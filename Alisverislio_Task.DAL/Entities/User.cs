using Alisverislio_Task.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alisverislio_Task.DAL.Entities
{
    public  class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public UserRole Role { get; set; } = UserRole.RegularUser;
        public ICollection<Note> Notes { get; set; } 
        public ICollection<Share> Shares { get; set; }
        public ICollection<Purchase> Purchases { get; set; } 
    }
}
