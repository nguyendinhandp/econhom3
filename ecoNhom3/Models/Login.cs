using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace ecoNhom3.Models
{
    public class Login
    {
        public int MaTv { get; set; }
        public int LoaiTv { get; set; }
        

        [Display(Name = "Tài khoản")]
        [Required(ErrorMessage = "Nhập Tài Khoản")]
        [MaxLength(50, ErrorMessage = "Tối đa 50 ký tự")]
        public string TaiKhoan { set; get; }

        [Display(Name = "Mật Khẩu")]
        [Required(ErrorMessage = "Nhập Mật Khẩu")]
        [MaxLength(50, ErrorMessage = "Tối đa 50 ký tự")]
        [DataType(DataType.Password)]
        public string MatKhau { set; get; }

        

    }
}
