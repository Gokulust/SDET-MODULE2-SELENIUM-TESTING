using OpenQA.Selenium;
using Swiggy.PageObjects;
using Swiggy.Utilities;
using System;
using System.Collections.Generic;
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
            List<SearchData> excelSaerchData = SearchUtils.ReadSearchData(excelFilePath, sheetName);
            
            foreach(var excel in excelSaerchData)
            {
                SwiggyHomePage swiggyHomePage = new(driver);
                var searchPage = swiggyHomePage.CLickOnSearchIcon();
                try
                {

                    Assert.That(driver.Url.Contains("search"));
                    Test = ExtentObject.CreateTest("Search Page Loading");
                    Test.Pass("Search Page Loaded successfully");
                }
                catch (AssertionException ex)
                {
                    Test = ExtentObject.CreateTest("Search Page Loding");
                    Test.Fail("Search Page Loaded failed");
                }
                var searchResultPage = searchPage.EnterSearchInput(excel.RestaurantName);
                try
                {

                    Assert.That(driver.FindElement(By.XPath("//div[@data-testid='resturant-card-name'][1]")).Text.Contains(excel.RestaurantName));
                    Test = ExtentObject.CreateTest("Search Result Page Loading");
                    Test.Pass("Search Result Page Loaded successfully");
                }
                catch (AssertionException ex)
                {
                    Test = ExtentObject.CreateTest("Search Result Page Loading");
                    Test.Fail("Search Result Page Loaded failed");
                }
                var restaurantPage = searchResultPage.ClickOnSelectedSearchElemnt();
                try
                {

                    Assert.That(driver.FindElement(By.XPath("//p[@class='RestaurantNameAddress_name__2IaTv']")).Text.Contains(excel.RestaurantName));
                    Test = ExtentObject.CreateTest("Resutaurant Page Loading");
                    Test.Pass("Resutaurant Page Loaded successfully");
                }
                catch (AssertionException ex)
                {
                    Test = ExtentObject.CreateTest("Resutaurant Page Loading");
                    Test.Fail("Resutaurant Page failed");
                }
                restaurantPage.ClickOnFoodSearchElement();
                Thread.Sleep(1000);
                restaurantPage.TypeFoodInFoodSearchBox(excel.FoodItemName);
                Thread.Sleep(1000);
                var isModal=restaurantPage.AddFoodItem();
                Console.Write(isModal.Count());
                if(isModal.Any())
                {
                    restaurantPage.ClickOnStartAFresh();
                }

                Thread.Sleep(2000);
                var viewCartPage = restaurantPage.ViewCart();
                try
                {

                    Assert.That(driver.FindElement(By.XPath("//div[@class='V7Usk']")).Text.Contains(excel.RestaurantName));
                    Test = ExtentObject.CreateTest("CheckOut Page Loading");
                    Test.Pass("CheckOut Page Loaded successfully");
                }
                catch (AssertionException ex)
                {
                    Test = ExtentObject.CreateTest("CheckOut Page Loading");
                    Test.Fail("CheckOut Page failed");
                }

                driver.Navigate().GoToUrl("https://www.swiggy.com/");
            }
            
        }
    }
}
