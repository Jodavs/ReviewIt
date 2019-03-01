using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BDSA.ReviewIt.Server.StorageLayer
{
    public class Program
    {
        static void Main()
        {
            File.Delete("./debug.db");

            EFContext context = new EFContext();
            context.PurgeData();
        }
    }
}
