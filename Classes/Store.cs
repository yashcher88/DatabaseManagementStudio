using Avalonia.Logging;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using System;
using System.IO;
using System.IO.Compression;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace DatabaseManagementStudio.Classes
{
    public class Store
    {
        private string packPath;
        private string setsPath;
        private string serversPath;

        public int versionPack = 0;
        public Logger logger;
        public Settings settings;
        public Styles styles;
        public Drivers drivers;
        public InterfaceLanguage interfaceLanguage;

        public Store() {
            logger = new Logger();
            settings = new Settings();
            styles = new Styles();
            drivers = new Drivers(logger);
            interfaceLanguage = new InterfaceLanguage();
            packPath = AppContext.BaseDirectory + "pack.dat";
            var AppData = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "DatabaseManagementStudio"
            );
            Directory.CreateDirectory(AppData);
            setsPath = Path.Combine(AppData, "sets.dat");
            serversPath = Path.Combine(AppData, "servers.dat");
        }
        public void LoadPack() {
            if (File.Exists(packPath))
            {
                 using var archive = ZipFile.OpenRead(packPath);
                foreach (var entry in archive.Entries)
                {
                    using var stream = entry.Open();
                    using var reader = new StreamReader(stream);
                    string content = reader.ReadToEnd();

                    if (entry.FullName == "language")
                    {
                        interfaceLanguage.Load(JsonNode.Parse(content));
                    }
                    else if (entry.FullName == "settings")
                    {
                        settings.Load(JsonNode.Parse(content));
                    }
                    else if (entry.FullName == "styles")
                    {
                        styles.Load(JsonNode.Parse(content));
                    }
                    else if (entry.FullName.StartsWith("driver_"))
                    {
                        MemoryStream M = new MemoryStream();
                        stream.CopyTo(M);
                        M.Position = 0;
                        drivers.Add(entry.FullName, M);
                    }
                    else if (entry.FullName.StartsWith("version"))
                    {
                        versionPack = Convert.ToInt32(content);
                    }
                }
            }
            else {
                logger.Info($"Файл {packPath} не существует");
            }
        }
        public void SavePack()
        {
            versionPack += 1;
            using (var outerMs = new MemoryStream())
            {
                using (var Arch = new ZipArchive(outerMs, ZipArchiveMode.Create, true))
                {
                    ZipArchiveEntry Z;
                    Z = Arch.CreateEntry("language");
                    using (var writer = new StreamWriter(Z.Open()))
                    {
                        writer.Write(interfaceLanguage.Build());
                    }
                    Z = Arch.CreateEntry("settings");
                    using (var writer = new StreamWriter(Z.Open()))
                    {
                        writer.Write(settings.Build());
                    }
                    Z = Arch.CreateEntry("styles");
                    using (var writer = new StreamWriter(Z.Open()))
                    {
                        writer.Write(styles.Build());
                    }
                    Z = Arch.CreateEntry("version");
                    using (var writer = new StreamWriter(Z.Open()))
                    {
                        writer.Write(versionPack.ToString());
                    }
                    for (var i = 0; i < drivers.items.Count; i++)
                    {
                        using (Stream w = Arch.CreateEntry(drivers.items[i].name).Open())
                        {
                            byte[] b = drivers.items[i].Build();
                            w.Write(b, 0, b.Length);
                        }
                    }
                }
                File.WriteAllBytes(packPath, outerMs.ToArray());
            }
        }

        public void LoadSets()
        {
            if (File.Exists(setsPath))
            {

            }
        }
        public void SaveSets()
        {

        }

        public void LoadUserSets()
        {
            if (File.Exists(setsPath))
            {

            }
        }
        public void SaveUserSets()
        {
            if (File.Exists(setsPath))
            {

            }
        }

        public void LoadUserStyles()
        {

        }
        public void SaveUserStyles()
        {

        }

        public void LoadServers()
        {
            if (File.Exists(serversPath))
            {

            }
        }
        public void SaveServers()
        {
            if (File.Exists(serversPath))
            {

            }
        }
    }
}
