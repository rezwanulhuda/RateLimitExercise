using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public interface IDataStore
    {
        List<HotelInfo> Search(string city, SortOrder sort);
    }
}
