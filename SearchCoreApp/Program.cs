using SearchCoreApp.Services;
using System;

namespace SearchCoreApp
{
    class Program
    {
        static void Main(string[] args)
        {
            IImporter importer = new Importer();
            IExporter exporter = new Exporter();
            ISearchAppartmentsService searchService = new SearchAppartmentsService(exporter,importer);
            searchService.SearchByListParams();
        }
    }
}
