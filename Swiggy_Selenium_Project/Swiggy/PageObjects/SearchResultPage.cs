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
    internal class SearchResultPage
    {
        
        IWebDriver driver;
        public SearchResultPage(IWebDriver driver)
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
        [FindsBy(How=How.XPath,Using = "//div[@class='styles_restaurantListItem__1lOsF'][1]")]
         private IWebElement SelectedSearchElemnt { get; set; }

        public ResutaurantPage ClickOnSelectedSearchElemnt()
        {
            CreateWait().Until(d => SelectedSearchElemnt.Displayed);
            SelectedSearchElemnt.Click();
            return new ResutaurantPage(driver);
        }
    }
}
