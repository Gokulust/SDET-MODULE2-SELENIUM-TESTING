using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swiggy.PageObjects
{
    internal class SwiggyHomePage
    {
        IWebDriver driver;
        public SwiggyHomePage(IWebDriver driver)
        {
            this.driver = driver ?? throw new ArgumentException(nameof(driver));
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How=How.XPath,Using = "//a[@href='/search']")]
        private IWebElement SearchIcon { get; set; }

        public SearchPage CLickOnSearchIcon()
        {
            SearchIcon.Click();
            Thread.Sleep(2000);
            return new SearchPage(driver);
        }
    }
}
