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

        private DefaultWait<IWebDriver> CreateWait()
        {
            DefaultWait<IWebDriver> wait = new DefaultWait<IWebDriver>(driver);
            wait.PollingInterval = TimeSpan.FromMilliseconds(100);
            wait.Timeout = TimeSpan.FromSeconds(9);
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));

            return wait;
        }
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
            CreateWait().Until(d=>FoodSearchIconElement.Displayed && FoodSearchIconElement.Enabled);
            FoodSearchIconElement.Click();
        }
        public void TypeFoodInFoodSearchBox(string foodName)
        {
            CreateWait().Until(d => FoodSearchInputBox.Displayed && FoodSearchInputBox.Enabled);
            FoodSearchInputBox.SendKeys(foodName);
        }

        public List<IWebElement> AddFoodItem()
        {
            // CoreCodes.ScrollViewInto(driver, FoodItemAddButton);
            CreateWait().Until(d=>FoodItemAddButton.Displayed && FoodItemAddButton.Enabled);
            FoodItemAddButton.Click();
          
            var modals = CreateWait().Until(d => d.FindElements(By.XPath("//button[text()='Yes, start afresh']")));
            Thread.Sleep(2000);
            return modals.ToList();
            
           
            
        }

      

        public void ClickOnStartAFresh()
        {
           
            var StartAFresh = CreateWait().Until(d => d.FindElement(By.XPath("//button[text()='Yes, start afresh']")));
            CreateWait().Until(d => StartAFresh.Displayed); 
            StartAFresh.Click();
           


        }

        public ViewCartPage ViewCart()
        {
            Thread.Sleep(1000);
            CreateWait().Until(d=>ViewCartButtonElement.Displayed && ViewCartButtonElement.Enabled);

            ViewCartButtonElement.Click();
          
            return new ViewCartPage(driver);
            
        }

    }
}
