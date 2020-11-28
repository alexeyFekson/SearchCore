using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SearcCore.Models;
using SearcCore.Services;

namespace SearcCore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Controller : ControllerBase
    {

        private readonly ISearchAppartmentsService _searchAppartmentsService;

        public Controller(ISearchAppartmentsService searchAppartmentsService)
        {
            _searchAppartmentsService = searchAppartmentsService;
        }


        [HttpGet("byparam/param={param}")]
        public IEnumerable<AppartmentModel> appartmentsByParam(string param)
        {
            var res = _searchAppartmentsService.SearchByText(param);


            return res;
        }


        [HttpGet("bylist")]
        public IEnumerable<AppartmentModel> appartmentsByList(string[] param)
        {
            // return new List<AppartmentModel>();
            IList<AppartmentModel> appartments = new List<AppartmentModel>();
            foreach (string str in param)
            {
                var resTemp = this.appartmentsByParam(str);
                foreach (var item in resTemp)
                {
                    appartments.Add(item);
                }
            }
            return appartments;


        }

        [HttpGet("byFileAuto")]
        public void AptByListFile()
        {
            _searchAppartmentsService.SearchByListParams();
            Console.WriteLine("[ALEXEY] - info - search by list ended. you can now close the program");
        }
    }
}

