using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public enum SortOrder
    {
        Undefined = 0,
        Asc = 1,
        Dsc = 2
    }

    public static class SortOrderExtensions
    {
        public static SortOrder ToSortOrder(this string sort)
        {
            SortOrder sorting = SortOrder.Undefined;
            if (!Enum.TryParse<SortOrder>(sort, true, out sorting))
            {
                sorting = SortOrder.Undefined;
            }

            return sorting;
            
        }
    }
}
