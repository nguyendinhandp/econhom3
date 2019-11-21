using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ecoNhom3.Models
{
    public class ChangePassword
    {
        [Display(Name = "MK Mới")]
        public string MatKhauMoi { set; get; }
        [Display(Name = "Xác nhận MK")]
        public string XacNhanMatKhauMoi { set; get; }
    }
}
