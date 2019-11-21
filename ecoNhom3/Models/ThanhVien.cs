using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ecoNhom3.Models
{
    public class ThanhVien
    {
        [Key]
        public int MaTv { get; set; }
        
        public string TenTv { get; set; }
       
        public string DiaChi { get; set; }
        
        public string DienThoai { get; set; }
       
        public string Email { get; set; }

        public int LoaiTv { get; set; }
        [ForeignKey("LoaiTv")]
        public PhanLoaiTV PhanLoaiTV { get; set; }

        public string TaiKhoan { get; set; }

        public string MatKhau { get; set; }

        public string GioiTinh { get; set; }

        public DateTime NgaySinh { get; set; }
  
      
    }
}
