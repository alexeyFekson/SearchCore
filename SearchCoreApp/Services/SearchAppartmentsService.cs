using SearchCoreApp.Models;
using SearchCoreApp.Selenuim;
using System.Collections.Generic;
using System.Threading;
using System;

namespace SearchCoreApp.Services
{
    public class SearchAppartmentsService : ISearchAppartmentsService
    {
        IExporter _exporter;
        IImporter _importer;

        public SearchAppartmentsService(IExporter exporter, IImporter importer)
        {
            _exporter = exporter;
            _importer = importer;

        }
        public IEnumerable<AppartmentModel> SearchByText(string searchParam)
        {

            SeleniumSeracher ss = new SeleniumSeracher();
            string[] KeyValue = searchParam.Split("#");
            Console.WriteLine(string.Format("[ALEXEY] - info - working on item key - {0}, value -{1}", KeyValue[0], KeyValue[1]));
            var apprtmentsFound = ss.SearchForAppartmentsByText(KeyValue[0]);

            string pathByKey = _exporter.ExportToFile(apprtmentsFound, @"D:\LunAptReport\", KeyValue[0], true);
            string pathByvalue = _exporter.ExportToFile(apprtmentsFound, @"D:\LunAptReport\", KeyValue[1], false);

            Console.WriteLine(string.Format(string.Format("[ALEXEY] -info- Search for {0} has ended, output file: By key:{1}, By Value: {2} ", searchParam, pathByKey, pathByvalue)));
            return apprtmentsFound;
        }

        public void SearchByListParams()
        {

            IEnumerable<string> spList = _importer.serachParamsFormInputFile();

            foreach (string sp in spList)
            {

                //Thread t = new Thread(() => SearchByText(sp));
                // t.Start();
                SearchByText(sp);
            }
        }
    }
}
