using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearcCore.Services
{
    public interface IImporter
    {
        public IEnumerable<string> serachParamsFormInputFile();
    }
}
