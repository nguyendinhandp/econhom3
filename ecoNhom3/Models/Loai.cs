using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ecoNhom3.Helper;

namespace ecoNhom3.Models
{
    public class Loai
    {
        [Key]
        public int MaLoai { get; set; }

        public string TenLoai { get; set; }

        public string TenLoaiTk
        {
            get
            {
                return Helper.FriendlyUrlHelper.Search(TenLoai);
            }
        }


    }
}
