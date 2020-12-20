using SearchCoreApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchCoreApp.Services
{
    public interface ISearchAppartmentsService
    {
        public IEnumerable<AppartmentModel> SearchByText(string text);

        public void SearchByListParams();
    }
}
