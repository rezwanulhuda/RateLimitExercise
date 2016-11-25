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
                output += span.ToString("%h") + " hour";
				if (span.Hours > 1) output += "s";
            }

            if (span.Minutes > 0)
            {
                if (!String.IsNullOrEmpty(output))
                {
                    output += ", ";
                }
                output += span.ToString("%m") + " minute";
				if (span.Minutes > 1) output += "s";
            }

            
            if (span.Seconds > 0)
            {
                if (!String.IsNullOrEmpty(output))
                {
                    output += ", ";
                }
                output += span.ToString("%s") + " second";
				if (span.Seconds > 1) output += "s";
            }

			if (String.IsNullOrWhiteSpace(output))
			{
				output = "1 second";
			}


            return output;
        }
    }
}
