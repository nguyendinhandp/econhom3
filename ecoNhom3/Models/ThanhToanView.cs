using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ecoNhom3.Models
{
    public class Thongtinkh
    {
        [Key]
        public string TENKH { get; set; }
        public string SDT { get; set; }
        public string DCNHAN { get; set; }
        public double PHISHIP { get; set; }
    }
    public class ThanhToanView
    {
        public int MaTT;
        public Thongtinkh thongtinKH { get; set; }
        public List<ChiTietHd> Cthd { get; set; }

    }
    

}
