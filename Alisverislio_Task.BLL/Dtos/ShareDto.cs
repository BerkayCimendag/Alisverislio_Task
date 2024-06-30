using Alisverislio_Task.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alisverislio_Task.BLL.Dtos
{
    public class ShareDto
    {
        public int Id { get; set; }
        public int NoteId { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
        public Visibility Visibility { get; set; }
    }
}
