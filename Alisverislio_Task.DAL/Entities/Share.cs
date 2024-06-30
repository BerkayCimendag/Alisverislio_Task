using Alisverislio_Task.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alisverislio_Task.DAL.Entities
{
    public class Share
    {
        public int Id { get; set; }
        public int NoteId { get; set; }
        public Note Note { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public Visibility Visibility { get; set; }
    }
}
