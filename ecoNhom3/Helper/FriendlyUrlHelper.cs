using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ecoNhom3.Helper 
{
    public static class FriendlyUrlHelper
    {
        
        public static string GetFriendlyTitle(string name, int id)
        {
            string tmp = string.Format("{0}-{1}", id, name);
            string str = tmp.ToLower().Trim();
            str = Regex.Replace(str, @"[áàảạãăắằẳẵặâấầẩẫậ]", "a");
            str = Regex.Replace(str, @"[êếềệểéèẹẻễẽ]", "e");
            str = Regex.Replace(str, @"[ôồốộổơờớợởòóọỏỗỗõ]", "o");
            str = Regex.Replace(str, @"[íìỉịĩ]", "i");
            str = Regex.Replace(str, @"[ưứừựửủụúùữũ]", "u");
            str = Regex.Replace(str, @"[đ]", "d");
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            str = Regex.Replace(str, @" - ", "-").Trim();
            str = Regex.Replace(str, @"\s+", "-").Trim();
            str = Regex.Replace(str, @"\s", "-");
            return str;

        }


    }
}
