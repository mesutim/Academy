using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Core.Convertors
{
    public static class FixedText
    {
        public static string FixEmail(string email)
        {
            return email.Trim().ToLower();
        }
        public static string FixText(this string text)
        {
            return text.Trim().Replace(" ", "-");
        }
    }
}
