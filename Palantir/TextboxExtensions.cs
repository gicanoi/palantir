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
    }
}
