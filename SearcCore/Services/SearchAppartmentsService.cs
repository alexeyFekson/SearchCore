using SearcCore.Models;
using SearcCore.Selenuim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearcCore.Services
{
    public class SearchAppartmentsService : ISearchAppartmentsService
    {
        public IEnumerable<AppartmentModel> SearchByText(string text)
        {
            SeleniumSeracher ss = new SeleniumSeracher();
            return  ss.SearchForAppartmentsByText(text);
        }
    }
}
