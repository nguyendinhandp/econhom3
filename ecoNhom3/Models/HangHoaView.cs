using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ecoNhom3.Models
{
    public class HangHoaView
    {
        [Key]
        public int MaHh { get; set; }
        public string TenHH { get; set; }
        public string Hinh { get; set; }
        public double DonGia { get; set; }
        public double GiamGia { get; set; }
        public string MoTa { get; set; }
        public int MaLoai { get; set; }
        [ForeignKey("MaLoai")]
        public Loai Loai { get; set; }
       

        public double GiaBan => DonGia * (1 - GiamGia);

    }
}
