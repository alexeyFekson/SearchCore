using SearcCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearcCore.Services
{
    public interface ISearchAppartmentsService
    {
        public IEnumerable<AppartmentModel> SearchByText(string text);

        public void SearchByListParams();
    }
}
