using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using Swiggy.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swiggy.PageObjects
{
    internal class ResutaurantPage
    {
        IWebDriver driver;
        public ResutaurantPage(IWebDriver driver)
        {
            this.driver = driver ?? throw new ArgumentException(nameof(driver));
            PageFactory.InitElements(driver, this);

            
        }
        [FindsBy(How=How.XPath,Using = "//div[@class='_3L1X9 _211P0 main_buttonInner__z6Jz0 main_button__3gpqi main_buttonNoImage__3ISPv']")]
        private IWebElement FoodItemAddButton { get; set; }

        [FindsBy(How=How.XPath,Using = "//div[@class='MenuTopHeader_rightNav__alWSF']")]

        private IWebElement FoodSearchIconElement { get; set; }

        [FindsBy(How=How.XPath,Using = "//input[@class='_3afzg']")]

        private IWebElement FoodSearchInputBox { get; set; }

        //[FindsBy(How=How.XPath,Using = "//button[@class='_2-MHS']")]

        //private IWebElement StartAFresh { get ; set; }

       [FindsBy(How=How.XPath,Using = "//button[@class='styles_container__3hEcN']")]
        private IWebElement ViewCartButtonElement { get; set; }


        public void ClickOnFoodSearchElement()
        {
            FoodSearchIconElement.Click();
        }
        public void TypeFoodInFoodSearchBox(string foodName)
        {
            FoodSearchInputBox.SendKeys(foodName);
        }

        public List<IWebElement> AddFoodItem()
        {
            // CoreCodes.ScrollViewInto(driver, FoodItemAddButton);
            DefaultWait<IWebDriver> fluentWait = new DefaultWait<IWebDriver>(driver);
            fluentWait.Timeout = TimeSpan.FromSeconds(5);
            fluentWait.PollingInterval = TimeSpan.FromMilliseconds(100);
            fluentWait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(WebDriverTimeoutException));
            fluentWait.Message = "Element Not Found";
            Thread.Sleep(1000);
            FoodItemAddButton.Click();
            Thread.Sleep(1000);
            var modals = fluentWait.Until(d => d.FindElements(By.XPath("//button[text()='Yes, start afresh']")));
            return modals.ToList();
           
            
        }

      

        public void ClickOnStartAFresh()
        {
            DefaultWait<IWebDriver> fluentWait = new DefaultWait<IWebDriver>(driver);
            fluentWait.Timeout = TimeSpan.FromSeconds(5);
            fluentWait.PollingInterval = TimeSpan.FromMilliseconds(100);
            fluentWait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(WebDriverTimeoutException));
            fluentWait.Message = "Element Not Found";
            var StartAFresh = fluentWait.Until(d => d.FindElement(By.XPath("//button[text()='Yes, start afresh']")));
            StartAFresh.Click();
            Thread.Sleep(2000);


        }

        public ViewCartPage ViewCart()
        {
            ViewCartButtonElement.Click();
            Thread.Sleep(2000);
            return new ViewCartPage(driver);
            
        }

    }
}
