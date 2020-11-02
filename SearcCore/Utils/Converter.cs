using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace SearcCore.Utils
{
    public static class Converter
    {
        private static double uah_to_usd { get; set; }
        public static double USD_TO_UAH
        {
            get
            {
                return uah_to_usd;
            }
        }
        public static async Task GetExchangeRate(string from, string to)
        {
            //Examples:
            //from = "EUR"
            //to = "USD"
            
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("https://free.currencyconverterapi.com");
                    var response = await client.GetAsync($"/api/v6/convert?q={from}_{to}&compact=y&apiKey=254562b60aaa4a49affe");
                    var stringResult = await response.Content.ReadAsStringAsync();
                    var dictResult = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(stringResult);
                    uah_to_usd = double.Parse(dictResult[$"{from}_{to}"]["val"]);
                    //data = {"EUR_USD":{"val":1.140661}}
                    //I want to return 1.140661
                    //EUR_USD is dynamic depending on what from/to is
                }
                catch (HttpRequestException httpRequestException)
                {
                    Console.WriteLine(httpRequestException.StackTrace);
                    //return "Error calling API. Please do manual lookup.";
                }
            }
        }
    }
}
