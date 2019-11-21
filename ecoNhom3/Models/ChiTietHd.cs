using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ecoNhom3.Models
{
    public class ChiTietHd
    {
        [Key]
        public int MaCTHD { get; set; }
    
        public int MaHd { get; set; }
        [ForeignKey("Mahd")]
        public HoaDon HoaDon { get; set; }
    
        public int MaHh { get; set; }
        [ForeignKey("MaHh")]
        public HangHoa HangHoa { get; set; }
     
        public double DonGia { get; set; }
   
        public double GiamGia { get; set; }
    
        public int SoLuong { get; set; }

        public double ThanhTien => SoLuong * (DonGia * (1 - GiamGia));
      
    }
}
