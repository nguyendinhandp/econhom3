using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ecoNhom3.Models
{
    public partial class PhanLoaiTV
    {
        [Key]        
        public int LoaiTv  { get; set; }
        public string LoaiThanhVien { get; set; }
        
    }
}
