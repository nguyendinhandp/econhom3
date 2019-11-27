using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ecoNhom3.Models
{
    public class Item
    {

        public string Hinh { get; set; }
        public int MaHh { get; set; }
        public string TenHh { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public double ThanhTien
        {
            get
            {
                return Quantity * Price;
            }
        }

        
        
 

    }
}
