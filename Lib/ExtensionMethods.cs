using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
    public static class ExtensionMethods
    {
        public static string Format(this TimeSpan span)
        {
            string output = String.Empty;

            if (span.Hours > 0)
            {
                output += span.ToString("%h") + " hours";
            }

            if (span.Minutes > 0)
            {
                if (!String.IsNullOrEmpty(output))
                {
                    output += ", ";
                }
                output += span.ToString("%m") + " minutes";
            }

            
            if (span.Seconds > 0)
            {
                if (!String.IsNullOrEmpty(output))
                {
                    output += ", ";
                }
                output += span.ToString("%s") + " seconds";
            }

            return output;
        }
    }
}
