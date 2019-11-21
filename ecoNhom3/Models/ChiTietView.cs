using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecoNhom3.Models
{
    public class ChiTietView
    {
        public HangHoa hh { get; set; }
        public List<HangHoa> hhCungLoai { get; set; }
        public List<HangHoa> hhCungNcc { get; set; }
       
    }
}
