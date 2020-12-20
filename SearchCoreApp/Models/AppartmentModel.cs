using SearchCoreApp.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchCoreApp.Models
{
    public enum CURRENCY
    {
        USD,
        UKH
    }
    public class AppartmentModel

    {

        public AppartmentModel(string rawPrice)
        {
            try
            {
                // using only ukraine/USD
                if (rawPrice.Contains("грн"))
                {
                    string value = rawPrice.Replace(" ", string.Empty).Trim().Split("грн")[0];
                    this._priceUsd = double.Parse(value) / Converter.USD_TO_UAH;
                }
                else
                {
                    string value = rawPrice.Replace(" ", string.Empty).Trim().Split("$")[0];
                    this._priceUsd = double.Parse(value);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                _priceUsd = 1;
            }

        }
        public string rawDate { get; set; }
        public string rawFloor { get; set; }
        private double _priceUsd { get; set; }
        public int PriceUsd
        {
            get
            {
                return (int)_priceUsd;
            }
        }
        public int PriceUAH
        {
            get
            {
                return (int)(Converter.USD_TO_UAH * _priceUsd);
            }
        }
        public string rawAdress { get; set; }
        public string rawSize { get; set; }
        public string rawMore { get; set; }

      

    }
}
