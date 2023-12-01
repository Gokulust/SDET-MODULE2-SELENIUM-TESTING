using AventStack.ExtentReports;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Swiggy.PageObjects;
using Swiggy.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Swiggy.TestScripts
{
    [TestFixture]
    internal class EndToEndFoodAddToCartTest:CoreCodes
    {
        [Test]
        public void FoodAddToCartTest()
        {
            string? currDir = Directory.GetParent(@"../../../")?.FullName;
            string? excelFilePath = currDir + "/TestData/SwiggyData.xlsx";
            string? sheetName = "SearchData";
            Func<DataRow,SearchData> myfunction = (row) => {return new SearchData() { RestaurantName = ExcelUtils.GetValueOrDefault(row, "Restaurant Name"), FoodItemName = ExcelUtils.GetValueOrDefault(row, "Food Item Name") }; };
            List<SearchData> excelSaerchData = ExcelUtils.ReadSearchData(excelFilePath, sheetName,myfunction) ;
            SwiggyHomePage swiggyHomePage = new(driver);
            swiggyHomePage.ChangeLocation("Thiruvananthapuram");

            foreach (var excel in excelSaerchData)
            {
               
                var searchPage = swiggyHomePage.CLickOnSearchIcon();
                WaitAndLogAssertion(() => driver.Url.Contains("search"), "Search Page Loading", "Search Page Loaded successfully", "Search Page Loaded failed");
                var searchResultPage = searchPage.EnterSearchInput(excel.RestaurantName);
                WaitAndLogAssertion(() => driver.FindElement(By.XPath("//div[@data-testid='resturant-card-name'][1]")).Text.Contains(excel.RestaurantName), "Search Result Page Loading", "Search Result Page Loaded successfully", "Search Result Page Loaded failed");
                var restaurantPage = searchResultPage.ClickOnSelectedSearchElemnt();
                WaitAndLogAssertion(() => driver.FindElement(By.XPath("//p[@class='RestaurantNameAddress_name__2IaTv']")).Text.Contains(excel.RestaurantName), "Resutaurant Page Loading", "Resutaurant Page Loaded successfully", "Resutaurant Page failed");
                restaurantPage.ClickOnFoodSearchElement();
                restaurantPage.TypeFoodInFoodSearchBox(excel.FoodItemName);
                var isModal=restaurantPage.AddFoodItem();
                
                if(isModal.Any())
                {
                    restaurantPage.ClickOnStartAFresh();
                }

                var viewCartPage = restaurantPage.ViewCart();
                WaitAndLogAssertion(()=> driver.FindElement(By.XPath("//div[@class='V7Usk']")).Text.Contains(excel.RestaurantName), "CheckOut Page Loading", "CheckOut Page Loaded successfully", "CheckOut Page failed");
               
                driver.Navigate().GoToUrl("https://www.swiggy.com/");
            }
            
        }
        void WaitAndLogAssertion(Func<bool> condition, string testName, string passMessage, string failMessage)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            ExtentTest test = ExtentObject.CreateTest(testName);

            try
            {
                wait.Until(driver => condition());
                test.Pass(passMessage);
            }
            catch (WebDriverTimeoutException)
            {
                test.Fail(failMessage);
            }
        }
    }
}
