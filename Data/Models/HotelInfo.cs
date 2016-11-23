using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class HotelInfo
    {
        public string City { get; set; }
        public int HotelId { get; set; }
        public string RoomType { get; set; }
        public decimal Price { get; set; }
    }
}
