using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
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
        private DefaultWait<IWebDriver> CreateWait()
        {
            DefaultWait<IWebDriver> wait = new DefaultWait<IWebDriver>(driver);
            wait.PollingInterval = TimeSpan.FromMilliseconds(100);
            wait.Timeout = TimeSpan.FromSeconds(9);
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));

            return wait;
        }

        [FindsBy(How=How.XPath,Using = "//input[@class='_2FkHZ']")]

        private IWebElement SearchInputBox { get; set; }

        public void TypeSearchInput(string searchKeyword)
        {
            CreateWait().Until(d => SearchInputBox.Displayed && SearchInputBox.Enabled);
            SearchInputBox.SendKeys(searchKeyword);
        }

        public SearchResultPage EnterSearchInput( string searchKeyword)
        {
            TypeSearchInput(searchKeyword);
            CreateWait().Until(d => SearchInputBox.Displayed);
            SearchInputBox.SendKeys(Keys.Enter);
            
            return new SearchResultPage(driver);
        }
    }
}
