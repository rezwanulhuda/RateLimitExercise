using Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Hosting;

namespace Api.Helpers
{
    public static class DataStoreHelper
    {
        public static IDataStore GlobalDataStore { get; private set; }

        private static void CopyStream(Stream input, Stream output)
        {
            byte[] buffer = new byte[8192];

            int bytesRead;
            while ((bytesRead = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, bytesRead);
            }
        }

        public static void Load()
        {
            using (Stream input = Assembly.GetExecutingAssembly().GetManifestResourceStream(@"Api.DataFile.hoteldb.csv"))
            {
                var fileName = Path.Combine(Path.GetTempPath(), "hoteldb.csv");
                using (Stream output = File.Create(fileName))
                {
                    CopyStream(input, output);
                }
                DataStoreHelper.GlobalDataStore = new FileInitializedInMemoryDataStore(fileName);
                File.Delete(fileName);
            }
        }
    }
}