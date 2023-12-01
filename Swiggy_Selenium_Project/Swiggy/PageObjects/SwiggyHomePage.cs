using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using Swiggy.Utilities;
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

        [FindsBy(How=How.XPath,Using = "//div[span[text()='Sign In']]")]
        private IWebElement SignInIcon { get; set; }

        [FindsBy(How=How.XPath,Using = "//a[text()='create an account']")]

        private IWebElement CreateAnAccountElement { get; set; }

        [FindsBy(How=How.Id,Using = "mobile")]

        private IWebElement MobileNumberInput { get; set; }

        [FindsBy(How=How.Id,Using ="name")]

        private IWebElement NameInput { get; set; }

        [FindsBy(How=How.Id,Using ="email")]

        private IWebElement EmailInput { get; set; }

        [FindsBy(How=How.XPath,Using = "//a[input[@type=\"submit\"]]")]

        private IWebElement CreateAccountButton { get; set; }

        [FindsBy(How=How.XPath,Using = "//div[@class='_2z2N5']")]

        public IWebElement LocationSelectElement { get; set; }

        [FindsBy(How=How.XPath,Using= "//input[@class='_381fS _1oTLG _1H_62']")]

        private IWebElement TypeLocationInputBox { get; set; }

        [FindsBy(How=How.XPath,Using = "//div[@class='_2peD4']")]

        private IWebElement SelectLocationElemnt { get; set; }

        [FindsBy(How=How.XPath,Using = "//div[div[div[text()='Sort By']]]")]

        private IWebElement SortByElement {  get; set; }

        [FindsBy(How=How.XPath,Using = "//label[text()='Rating']")]

        private IWebElement RatingElement { get; set; }

        [FindsBy(How=How.XPath,Using = "//button[div[text()='Apply']]")]

        private IWebElement SortByApply { get; set; }

        [FindsBy(How=How.XPath,Using = "//div[div[div[text()='Ratings 4.0+']]]")]

        private IWebElement Rating4PlusElement { get; set; }

        [FindsBy(How=How.XPath,Using = "//div[div[div[text()='Fast Delivery']]]")]

        private IWebElement FastDeliveryElement { get; set; }

        //[FindsBy(How=How.XPath,Using = "//span[@class='sc-aXZVg jxDVMd']")]
        //private List<IWebElement> RatingList { get; set; }



        public SearchPage CLickOnSearchIcon()
        {
            SearchIcon.Click();
            Thread.Sleep(2000);
            return new SearchPage(driver);
        }
        public void ClickOnSignInIcon()
        {
            SignInIcon.Click();
        }
        public void ClickOnCreateAnAccount()
        {
            CreateAnAccountElement.Click();
        }

        public void CreatAnAccount(string userName,string mobileNumber,string email)
        {
            NameInput.SendKeys(userName);
            MobileNumberInput.SendKeys(mobileNumber);
            EmailInput.SendKeys(email);
            CreateAccountButton.Click();
        }

       public void ChangeLocation(string location)
        {
            LocationSelectElement.Click();
            Thread.Sleep(2000);
            TypeLocationInputBox.SendKeys(location);
            Thread.Sleep(2000);
            SelectLocationElemnt.Click();
            Thread.Sleep(3000);
        }
        public void SortByRating()
        {
            Thread.Sleep(2000);
            CoreCodes.ScrollViewInto(driver,SortByElement);
            Thread.Sleep(2000);
            SortByElement.Click();
            Thread.Sleep(2000);
            RatingElement.Click();
            Thread.Sleep(2000);
            SortByApply.Click();
            Thread.Sleep(2000);
        }
  
        public List<double> GetRatingsAfterSorting()
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            var elementsAfterSort = wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.XPath("//div[@class='sc-dISpDn sc-iaJaUu hoJOJi fXbKft'][4]/div/div/div/a/div/div[2]/div[2]/div[2]/span")));
            var RatingList = driver.FindElements(By.XPath("//div[@class='sc-dISpDn sc-iaJaUu hoJOJi fXbKft'][4]/div/div/div/a/div/div[2]/div[2]/div[2]/span"));
            var modifiedRatingList = RatingList.Select(x => Convert.ToDouble( x.Text.Replace(" ", "").Replace("•", ""))).ToList();
            foreach (var rating in modifiedRatingList)
            {
                
                Console.WriteLine(rating);
            }

            return modifiedRatingList;
            
        }

        public bool CheckRatingsBeforeSortingAndAfterSorting(List<double> ratingAfterSorting)
        {


            return ratingAfterSorting.Zip(ratingAfterSorting.Skip(1), (cur, next) => cur >= next).All(x => x);

        }

        public void SortByRatingMoreThanOrEqualTo4()
        {
            CoreCodes.ScrollViewInto(driver, SortByElement);
            Rating4PlusElement.Click();
            
        }
        public bool CheckRatingMoreThanOrEqualTo4()
        {
            SortByRatingMoreThanOrEqualTo4();
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            var elementsAfterSort = wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.XPath("//div[@class='sc-dISpDn sc-iaJaUu hoJOJi fXbKft'][4]/div/div/div/a/div/div[2]/div[2]/div[2]/span")));
            var RatingList = driver.FindElements(By.XPath("//div[@class='sc-dISpDn sc-iaJaUu hoJOJi fXbKft'][4]/div/div/div/a/div/div[2]/div[2]/div[2]/span"));
            var modifiedRatingList = RatingList.Select(x => Convert.ToDouble(x.Text.Replace(" ", "").Replace("•", ""))).ToList();
            return modifiedRatingList.All(r => r >= 4);
        }
        public void ClickOnFastDeliveryElement()
        {
            CoreCodes.ScrollViewInto(driver, FastDeliveryElement);
            FastDeliveryElement.Click();
        }
        public bool FastDeliveryCheck()
        {
            ClickOnFastDeliveryElement();
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            var elementsAfterSort = wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.XPath("//div[@class='sc-dISpDn sc-iaJaUu hoJOJi fXbKft'][4]/div/div/div/a/div/div[2]/div[2]/div[2]/span")));
            var deliveryList = driver.FindElements(By.XPath("//div[@class='sc-dISpDn sc-iaJaUu hoJOJi fXbKft'][4]/div/div/div/a/div/div[2]/div[2]/div[2]"));
            var modifiedRatingList = deliveryList.Select(x => x.Text.Replace(" ", "").Replace("mins", "")).ToList();
            List<int> extractedNumbers = new List<int>();
            foreach (var value in modifiedRatingList)
            {
                string[] parts = value.Split('•');
                int.TryParse(parts[1], out int number);
                    extractedNumbers.Add(number);
                
            }
            foreach (var value in extractedNumbers)
            { Console.WriteLine(value); }
        
            return extractedNumbers.Zip(extractedNumbers.Skip(1), (cur, next) => cur <= next).All(x => x);

        }

    }
}
