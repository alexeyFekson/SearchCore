
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SearcCore.Models;
using SearcCore.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SearcCore.Selenuim
{
    public class SeleniumSeracher :IDisposable
    {
        IWebDriver driver;
        private readonly string URL = "https://flatfy.lun.ua/%D0%B0%D1%80%D0%B5%D0%BD%D0%B4%D0%B0-%D0%BA%D0%B2%D0%B0%D1%80%D1%82%D0%B8%D1%80-%D0%BA%D0%B8%D0%B5%D0%B2";
        private readonly string flatfySearchBoxIs = "geo-control-input";
        private readonly string aptItemArticalPath = "//article[@class=\"jss160\"]";
        private readonly string aptDataPrice = "//div[@class=\"jss191\"]";
        private readonly string aptDataDate = "//div[@class=\"jss192\"]";
        private readonly string searchBtnString = "//*[text()=\"Выбрать\"]";
        private readonly string aptAdres = "//div[@class=\"jss180\"]";
        private readonly string aptDetailSection = "//ul[@class=\"jss194\"]"; ////article[@class="jss160"][2]//ul[@class="jss194"]
        private readonly string aptDetailSectionSize = "[1]//li[2]";
        //size //article[@class="jss160"][2]//ul[@class="jss194"]/li[2]

        public SeleniumSeracher() {
            driver = new ChromeDriver(@"C:\");

            AfetInitAsync();
        }

        public async Task AfetInitAsync()
        {
         await   Converter.GetExchangeRate("USD", "UAH") ;
          
        }


        public IEnumerable<AppartmentModel> SearchForAppartmentsByText(string searchParam )
        {
            OpenChromeEnterTextAndSearch(searchParam);
            IEnumerable< IWebElement> listOfAptsWebElement =  FindAllAppartmentItems();
            List<AppartmentModel> aptList = new List<AppartmentModel>();
            for (int i= 1; i<listOfAptsWebElement.Count();i++)
            {
                try
                {
                    aptList.Add(convertFromWEbDriverTOAppartment(i));
                }
                catch(Exception e)
                {
                    Console.WriteLine(e);
                }
             
            }
            return aptList;
        }


        private AppartmentModel convertFromWEbDriverTOAppartment(int idx)
        {

            string priceRaw = getValueByXpath(aptItemArticalPath + "[" + idx + "]" + aptDataPrice);

            string rawDate = getValueByXpath(aptItemArticalPath + "[" + idx + "]" + aptDataDate);

            string rawAdress = getValueByXpath(aptItemArticalPath + "[" + idx + "]" + aptAdres);

            string rawSize = getValueByXpath(aptItemArticalPath + "[" + idx + "]" + aptDetailSection + aptDetailSectionSize);

            return new AppartmentModel(priceRaw) { rawDate = rawDate,rawAdress = rawAdress.Replace("\r\n", " "), rawSize = rawSize };
        }



        private string getValue(IWebElement element)
        {
            return element.Text;
        }

        private string getValueByXpath(string path)
        {
            string res = "";
            try
            {
             res = getValue(driver.FindElement(By.XPath(path)));
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
            return res;
        }

        private IEnumerable<IWebElement> FindAllAppartmentItems()
        {
            IEnumerable<IWebElement> listOfAptsWebElement = driver.FindElements(By.XPath(aptItemArticalPath));

            return listOfAptsWebElement;
        }

        private void OpenChromeEnterTextAndSearch(string text)
        {
            driver.Navigate().GoToUrl(URL);
            IWebElement searchBox = driver.FindElement(By.Id(flatfySearchBoxIs));
            searchBox.SendKeys(text);
            System.Threading.Thread.Sleep(2000);
            IWebElement searchBtn = driver.FindElement(By.XPath(searchBtnString));
            searchBtn.Click();
            System.Threading.Thread.Sleep(3000);

        }

        public void Dispose()
        {
            driver.Dispose();
        }
    }
}
