using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;

namespace Data
{
    public class FileInitializedInMemoryDataStore : IDataStore
    {
        public sealed class HotelInfoMap : CsvClassMap<HotelInfo>
        {
            public HotelInfoMap()
            {
                Map(m => m.City).Name("CITY");
                Map(m => m.HotelId).Name("HOTELID");
                Map(m => m.RoomType).Name("ROOM");
                Map(m => m.Price).Name("PRICE");
            }
        }

        private readonly List<HotelInfo> hotels = new List<HotelInfo>();

        public FileInitializedInMemoryDataStore(string fileName)
        {
            using (StreamReader sr = new StreamReader(fileName))
            {
                CsvReader reader = new CsvHelper.CsvReader(sr);
                reader.Configuration.Delimiter = ",";
                reader.Configuration.RegisterClassMap<HotelInfoMap>();
                hotels = reader.GetRecords<HotelInfo>().ToList();
            }
        }

        public List<HotelInfo> Search(string city, SortOrder sort)
        {
            var filtered = hotels.Where(p => String.Compare(p.City, city, true) == 0);
            switch(sort)
            {
                case SortOrder.Asc: filtered = filtered.OrderBy(p => p.Price); break;
                case SortOrder.Dsc: filtered = filtered.OrderByDescending(p => p.Price); break;
                default: break;
            }

            return filtered.ToList();
        }
    }
}
