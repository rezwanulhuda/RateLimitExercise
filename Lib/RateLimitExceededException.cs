using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
    public class RateLimitExceededException : Exception
    {
        public RateLimitExceededException(int limit, TimeSpan waitFor)
            : base(String.Format("Rate limit of {0} requests exceeded. Wait for {1}", limit, waitFor.Format()))
        {
        }
    }
}
