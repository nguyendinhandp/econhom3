using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ecoNhom3.Models
{
    public class HoaDon
    {
        [Key]
        public int MaHd { get; set; }
        
        public int MaTv { get; set; }
        [ForeignKey("MaTv")]
        public ThanhVien ThanhVien { get; set; }
      
        public DateTime NgayDat { get; set; }
    
        public DateTime? NgayGiao { get; set; }
    
        public string HoTen { get; set; }
  
        public string DiaChi { get; set; }
    
        public string SdtNguoiNhan { get; set; }
      
        public string GhiChu { get; set; }
     
        public double PhiShip { get; set; }
       
        public int MaTrangThai { get; set; }
        [ForeignKey ("MaTrangThai")]
        public TrangThai TrangThai { get; set; }

        public int MaCthd { get; set; }
        [ForeignKey("MaCTHD")]
        public ChiTietHd ChiTietHd { get; set; }
        public double TongTienHang => ChiTietHd.ThanhTien;
 
     
    }
}
