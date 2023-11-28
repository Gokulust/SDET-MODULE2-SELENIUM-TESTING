using OpenQA.Selenium;
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
        [FindsBy(How=How.XPath,Using = "//div[@class='styles_restaurantListItem__1lOsF'][1]")]
         private IWebElement SelectedSearchElemnt { get; set; }

        public ResutaurantPage ClickOnSelectedSearchElemnt()
        {
            SelectedSearchElemnt.Click();
            Thread.Sleep(3000);
            return new ResutaurantPage(driver);
        }
    }
}
