using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Helpers
{
    public static class DataStoreHelper
    {
        public static IDataStore GlobalDataStore = new FileInitializedInMemoryDataStore("hoteldb.csv");
    }
}