using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace SearcCore.Services
{
    public class Importer : IImporter
    {
        public IEnumerable<string> serachParamsFormInputFile()
        {
            IList<string> sParams = new List<string>();

            //TODO: replace with read from config
            using (var streamReader = File.OpenText(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)+"\\"+"searchParams.txt"))
            {
                try
                {
                    var lines = streamReader.ReadToEnd().Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    foreach (var line in lines)
                    {
                        sParams.Add(line);
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine(e);
                }

            }
            return sParams;
        }
    }
}
