using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using ecoNhom3.Helper;
using System.Text;

namespace ecoNhom3.Models
{
    public class HangHoa
    {
        [Key]
        public int MaHh { get; set; }

        public string TenHh { get; set; }

        public int MaLoai { get; set; }
        [ForeignKey("MaLoai")]
        public Loai Loai { get; set; }

        public string Hinh { get; set; }

        public double DonGia { get; set; }

        public double GiamGia { get; set; }

        public double Price => DonGia * (1 - GiamGia);

        public string MoTa { get; set; }

        public int MaNcc { get; set; }
        [ForeignKey("MaNcc")]
        public NhaCungCap NhaCungCap { get; set; }

        public string Url
        {
            get
            {
                return Helper.FriendlyUrlHelper.GetFriendlyTitle(TenHh,MaHh);
            }
        }
       
    }
}