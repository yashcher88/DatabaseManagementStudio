using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace DatabaseManagementStudio.Classes
{
    public class Drivers
    {
        public List<Driver> items = new List<Driver>();
        Logger logger;
        public Drivers(Logger L)
        {
            logger = L;
        }
        public void Add(string Name,MemoryStream ZipContent) {
            items.Add(new Driver(logger, Name));
        }
    }
}
