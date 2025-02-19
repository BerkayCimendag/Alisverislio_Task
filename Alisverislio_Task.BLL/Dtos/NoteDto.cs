﻿using Alisverislio_Task.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alisverislio_Task.BLL.Dtos
{
    public class NoteDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public Visibility Visibility { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
    }
}
