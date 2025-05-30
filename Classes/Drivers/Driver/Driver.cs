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
    public class Driver
    {
        public string name;
        Logger logger;
        JsonNode? templates;
        JsonNode? objectExplorer;
        JsonNode? popups;
        JsonNode? windows;
        JsonNode? language;
        public Driver(
            Logger _logger,
            string _name
        )
        { 
            name = _name;
            logger = _logger;
        }
        public void Load(MemoryStream ZipContent) {
            var archive = new ZipArchive(ZipContent);
            foreach (var entry in archive.Entries)
            {
                using var stream = entry.Open();
                using var reader = new StreamReader(stream);
                string content = reader.ReadToEnd();
                if (entry.FullName == "templates")
                {
                    templates = JsonNode.Parse(content);
                }
                else if (entry.FullName == "objectExplorer")
                {
                    objectExplorer = JsonNode.Parse(content);
                }
                else if (entry.FullName == "popups")
                {
                    popups = JsonNode.Parse(content);
                }
                else if (entry.FullName == "windows")
                {
                    windows = JsonNode.Parse(content);
                }
                else if (entry.FullName == "language")
                {
                    language = JsonNode.Parse(content);
                }
            }
        }
        public byte[] GetInnerArch()
        {
            byte[] innerZipBytes;
            using (var innerMs = new MemoryStream())
            {
                // Создаем и наполняем внутренний архив
                using (var innerArchive = new ZipArchive(innerMs, ZipArchiveMode.Create, true))
                {
                    // Добавляем файл во внутренний архив
                    var innerEntry = innerArchive.CreateEntry("file_inside_inner.txt");
                    using (var writer = new StreamWriter(innerEntry.Open()))
                    {
                        writer.WriteLine("Содержимое внутреннего архива");
                    }
                }

                // Получаем байты внутреннего архива
                innerZipBytes = innerMs.ToArray();
            }
            return innerZipBytes;
        }
        public byte[] Build() {
            StreamWriter w;
            var memoryStream = new MemoryStream();
            ZipArchive Z = new ZipArchive(memoryStream, ZipArchiveMode.Create, true);
            w = new StreamWriter(Z.CreateEntry("templates").Open());
            w.Write(templates?.ToString() ?? "{}");
            w = new StreamWriter(Z.CreateEntry("objectExplorer").Open());
            w.Write(objectExplorer?.ToString() ?? "{}");
            w = new StreamWriter(Z.CreateEntry("popups").Open());
            w.Write(popups?.ToString() ?? "{}");
            w = new StreamWriter(Z.CreateEntry("windows").Open());
            w.Write(windows?.ToString() ?? "{}");
            w = new StreamWriter(Z.CreateEntry("language").Open());
            w.Write(language?.ToString() ?? "{}");
            return memoryStream.ToArray();
        }
    }
}
