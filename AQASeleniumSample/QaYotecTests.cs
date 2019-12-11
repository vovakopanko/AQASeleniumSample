using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.IO;
using System.Linq;

namespace AQASeleniumSample
{
    [TestClass]
    public class QaYotecTests
    {
        private IWebDriver _webDriver;
        private readonly string _baseUrl = "http://qa.yotec.net/";

        [TestInitialize]
        public void Initialize()
        {
            var currentDir = Directory.GetCurrentDirectory();
            _webDriver = new ChromeDriver(currentDir);
        }

        // todo add test with clicks and new tabs instead GoToUrl approach
        // todo add breadcrumbs checks after each nav and change test name
        [TestMethod]
        public void MainMenu_NavigatonWorks_AnyExceptionsNotThrown()
        {
            _webDriver.Manage().Window.Maximize();
            _webDriver.Navigate().GoToUrl("http://qa.yotec.net/");

            var mainMenuItems = _webDriver.FindElements(By.CssSelector("div[class*=\"navigation\"] > ul > li"))
                .Select(x => new MenuItem()
                {
                    Url = _baseUrl + x.FindElement(By.TagName("a")).GetAttribute("href"),
                    SubCategoryUrls = x.FindElements(By.CssSelector("ul > li > a")).Select(y => y.GetAttribute("href")).ToList()
                });

            foreach (var mainMenuItem in mainMenuItems)
            {
                _webDriver.Navigate().GoToUrl(mainMenuItem.Url);

                foreach (var subCategoryUrl in mainMenuItem.SubCategoryUrls)
                    _webDriver.Navigate().GoToUrl(subCategoryUrl);
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            _webDriver.Quit();
            _webDriver.Dispose();
            _webDriver = null;
        }
    }
}
