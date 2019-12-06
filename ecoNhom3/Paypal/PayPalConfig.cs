using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecoNhom3.Paypal
{
    public class PayPalConfig
    {
        public string AuthToken { get; set; }
        public string PostUrl { get; set; }
        public string Business { get; set; }
        public string ReturnUrl { get; set; }

    }
}
