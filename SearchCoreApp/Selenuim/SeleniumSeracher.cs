
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SearchCoreApp.Models;
using SearchCoreApp.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

namespace SearchCoreApp.Selenuim
{
    public class SeleniumSeracher : IDisposable
    {
       // private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
     //  input-base__input
        IWebDriver driver;
        private readonly string URL = "https://flatfy.lun.ua/%D0%B0%D1%80%D0%B5%D0%BD%D0%B4%D0%B0-%D0%BA%D0%B2%D0%B0%D1%80%D1%82%D0%B8%D1%80-%D0%BA%D0%B8%D0%B5%D0%B2";
       // private readonly string URL = "https://flatfy.lun.ua/%D0%BF%D1%80%D0%BE%D0%B4%D0%B0%D0%B6%D0%B0-%D0%BA%D0%B2%D0%B0%D1%80%D1%82%D0%B8%D1%80-%D0%BA%D0%B8%D0%B5%D0%B2"
        private readonly string flatfySearchBoxIs = "geo-control-input";
        private readonly string aptItemArticalPath = "//article[@class=\"jss160\"]";
        private readonly string aptDataPrice = "//div[@class=\"jss191\"]";
        private readonly string aptDataDate = "//div[@class=\"jss192\"]";
        private readonly string searchBtnString = "//*[text()=\"Выбрать\"]";
        private readonly string aptAdres = "//div[@class=\"jss180\"]";
        private readonly string aptDetailSection = "//ul[@class=\"jss194\"]"; ////article[@class="jss160"][2]//ul[@class="jss194"]
        private readonly string aptDetailSectionSize = "[1]//li[2]";
        private readonly string aptDetailSectionFloor = "[1]//li[3]";
        private readonly string aptMore = "//button//span[@class =\"jss61\"]";
        //size //article[@class="jss160"][2]//ul[@class="jss194"]/li[2]

        public SeleniumSeracher()
        {
            //   var chromeOptions = new ChromeOptions();
            //    chromeOptions.AddArguments("headless");
            ChromeOptions options = new ChromeOptions();
            options.BinaryLocation = "D:\\Chrome\\Application";
            driver  = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),options);

            Task.Run(AfetInitAsync);
        }

        public async Task AfetInitAsync()
        {
            await Converter.GetExchangeRate("USD", "UAH"); //String strUrl = driver.getCurrentUrl();

        }


        public IEnumerable<AppartmentModel> SearchForAppartmentsByText(string searchParam)
        {

            OpenChromeEnterTextAndSearch(searchParam);
            System.Threading.Thread.Sleep(12000);
            IList<AppartmentModel> allPgesApt = SearchForAppartmentByTextBulk();
            string baseUrl = driver.Url;
            for (int i = 2; i < 10; i++)
            {
                try
                {
                    driver.Navigate().GoToUrl(baseUrl + "?page="+i);
                    var pageApts=   SearchForAppartmentByTextBulk();
                    System.Threading.Thread.Sleep(12000);
                    if (pageApts.Count == 0) return allPgesApt;
                    foreach(var apt in pageApts)
                    {
                        allPgesApt.Add(apt);
                    }

                }
                catch(Exception)
                {
                    return allPgesApt;
                  //  break;
                }
            }



            return allPgesApt;
        }


        private IList<AppartmentModel> SearchForAppartmentByTextBulk()
        {
            IEnumerable<IWebElement> listOfAptsWebElement = FindAllAppartmentItems();
            List<AppartmentModel> aptList = new List<AppartmentModel>();
            for (int i = 1; i <= listOfAptsWebElement.Count(); i++)
            {
                try
                {
                    AppartmentModel apt = convertFromWEbDriverTOAppartment(i);
                    if (IsValid(apt))
                    {
                        aptList.Add(apt);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

            }
            return aptList;
        }

        private bool IsValid(AppartmentModel apt)
        {
            return apt.PriceUsd > 10;
        }

        private AppartmentModel convertFromWEbDriverTOAppartment(int idx)
        {

            string priceRaw = getValueByXpath(aptItemArticalPath + "[" + idx + "]" + aptDataPrice);

            string rawDate = getValueByXpath(aptItemArticalPath + "[" + idx + "]" + aptDataDate);

            string rawAdress = getValueByXpath(aptItemArticalPath + "[" + idx + "]" + aptAdres);

            string rawSize = getValueByXpath(aptItemArticalPath + "[" + idx + "]" + aptDetailSection + aptDetailSectionSize);

            string rawFloor = getValueByXpath(aptItemArticalPath + "[" + idx + "]" + aptDetailSection + aptDetailSectionFloor,false);

            string rawMore = getValueByXpath(aptItemArticalPath + "[" + idx + "]" + aptMore,false);
            return new AppartmentModel(priceRaw) {
                rawDate = rawDate,
                rawAdress = rawAdress.Replace("\r\n", " "), 
                rawSize = rawSize, 
                rawFloor = rawFloor,
                rawMore=rawMore 
            };
        }



        private string getValue(IWebElement element)
        {
            return element.Text;
        }

        private string getValueByXpath(string path,bool must = true)
        {
            string res = "";
            try
            {
                res = getValue(driver.FindElement(By.XPath(path)));
            }
            catch (Exception e)
            {
                if (must)
                {
                    Console.WriteLine(string.Format("[ALEXEY] - error - not found element {0}, proccess continue",path ));
                } else
                {
                   // Console.WriteLine(string.Format($"[ALEXEY] - debug - not found element {0}, proccess continue [this is not an error!]", path));
                }
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
            try
            {
                driver.Navigate().GoToUrl(URL);
                System.Threading.Thread.Sleep(7000);
                IWebElement searchBox = driver.FindElement(By.Id(flatfySearchBoxIs));
                searchBox.SendKeys(text);
                System.Threading.Thread.Sleep(5000);
                IWebElement searchBtn = driver.FindElement(By.XPath(searchBtnString));
                searchBtn.Click();
                System.Threading.Thread.Sleep(3000);
            }
            catch (Exception)
            {

            }

        }

        public void Dispose()
        {
            
          driver.Close();
          driver.Quit();
        }
    }
}
