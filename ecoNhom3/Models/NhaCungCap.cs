using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ecoNhom3.Models
{
    public class NhaCungCap
    {
        [Key]      
        public int MaNcc { get; set; }
      
        public string TenNcc { get; set; }
       
        public string Email { get; set; }
      
        public string DienThoai { get; set; }
   
        public string DiaChi { get; set; }
     
        public string MoTa { get; set; }
  
      

        public ICollection<HangHoa> HangHoa { get; set; }
    }
}
