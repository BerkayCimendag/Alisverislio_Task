using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alisverislio_Task.BLL.Dtos
{
    public class LocationDto
    {
        public int Id { get; set; }
        public string Shelf { get; set; }
        public string Aisle { get; set; }
        public string Floor { get; set; }
        public string Section { get; set; }
    }
}
