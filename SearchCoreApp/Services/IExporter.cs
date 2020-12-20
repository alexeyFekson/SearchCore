using SearchCoreApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchCoreApp.Services
{
    public interface IExporter
    {
        public string ExportToFile(IEnumerable<AppartmentModel> item, string filePath,string param,bool byKey);
    }
}
