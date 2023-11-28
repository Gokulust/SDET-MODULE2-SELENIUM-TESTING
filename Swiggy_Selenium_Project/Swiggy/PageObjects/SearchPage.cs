using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swiggy.PageObjects
{
    internal class SearchPage
    {
        IWebDriver driver;
        public SearchPage(IWebDriver driver)
        {
            this.driver = driver ?? throw new ArgumentException(nameof(driver));
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How=How.XPath,Using = "//input[@class='_2FkHZ']")]

        private IWebElement SearchInputBox { get; set; }

        public void TypeSearchInput(string searchKeyword)
        {
            SearchInputBox.SendKeys(searchKeyword);
        }

        public SearchResultPage EnterSearchInput( string searchKeyword)
        {
            TypeSearchInput(searchKeyword);
            SearchInputBox.SendKeys(Keys.Enter);
            Thread.Sleep(2000);
            return new SearchResultPage(driver);
        }
    }
}
