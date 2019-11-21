using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace ecoNhom3.Models
{
    public class TrangThai
    {
        [Key]
        public int MaTt { get; set; }
        public string TenTrangThai { get; set; }

        public ICollection<HoaDon> HoaDon { get; set; }
    }
}
