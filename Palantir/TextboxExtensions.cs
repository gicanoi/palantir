using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palantir
{
    public static class TextboxExtensions
    {
        public static double? GetDouble(this System.Windows.Forms.TextBox @this)
        {
            double parsed;
            if(Double.TryParse(@this.Text, out parsed))
            {
                return parsed;
            }
            return null;
        }

        public static List<double> GetDoubleList(this System.Windows.Forms.TextBox @this, char separator=';')
        {
            if (string.IsNullOrEmpty(@this.Text)) return null;
            try
            {
                return @this.Text.Split(separator).Select(x => double.Parse(x.Trim())).ToList();
            }
            catch
            {
                return null;
            }
        }
    }
}
