using Newtonsoft.Json;
using SearcCore.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace SearcCore.Services
{
    public class Exporter : IExporter
    {
        public string ExportToFile(IEnumerable<AppartmentModel> items, string filePath, string param, bool byKey)
        {
            string folder = filePath;
            Directory.CreateDirectory(folder);
            folder = folder + "\\" + (byKey ? "byKey" : "byValue");
            Directory.CreateDirectory(folder);
            folder = folder + "\\" + string.Format("{0}", param.Trim().Replace(" ", "_"));
            Directory.CreateDirectory(folder);
            string fileName = DateTime.Now.Date.ToString("dd'-'MM'-'yyyy");
            string fullPath = folder + "\\" + fileName + ".txt";
            using (StreamWriter outputFile = new StreamWriter(fullPath))
            {
                var line = JsonConvert.SerializeObject(items);
                outputFile.WriteLine(line);
            }
            return fileName;
        }
    }
}
