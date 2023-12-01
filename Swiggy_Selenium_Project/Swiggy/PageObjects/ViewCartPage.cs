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
    internal class ViewCartPage
    {
        IWebDriver driver;
        private DefaultWait<IWebDriver> CreateWait()
        {
            DefaultWait<IWebDriver> wait = new DefaultWait<IWebDriver>(driver);
            wait.PollingInterval = TimeSpan.FromMilliseconds(100);
            wait.Timeout = TimeSpan.FromSeconds(9);
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));

            return wait;
        }
        public ViewCartPage(IWebDriver driver)
        {
            this.driver = driver ?? throw new ArgumentException(nameof(driver));
            PageFactory.InitElements(driver, this);
        }
        [FindsBy(How=How.XPath,Using = "//div[@class='_29Y5Z']")]

        private IWebElement FoodDecrementElement { get; set; }

        public void ClickOnDecrementButton()
        {
            CreateWait().Until(d => FoodDecrementElement.Displayed);
            FoodDecrementElement.Click();
        }
    }
}
