using OpenQA.Selenium;
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
    internal class EndToEndCartEmptyTest:CoreCodes
    {
        [Test]
        public void RemovingFoodFromCart()
        {
            string? currDir = Directory.GetParent(@"../../../")?.FullName;
            string? excelFilePath = currDir + "/TestData/SwiggyData.xlsx";
            string? sheetName = "SearchData";
            Func<DataRow, SearchData> myfunction = (row) => { return new SearchData() { RestaurantName = ExcelUtils.GetValueOrDefault(row, "Restaurant Name"), FoodItemName = ExcelUtils.GetValueOrDefault(row, "Food Item Name") }; };
            List<SearchData> excelSaerchData = ExcelUtils.ReadSearchData(excelFilePath, sheetName, myfunction);
            SwiggyHomePage swiggyHomePage = new(driver);
            swiggyHomePage.ChangeLocation("Thiruvananthapuram");

            foreach (var excel in excelSaerchData)
            {

                var searchPage = swiggyHomePage.CLickOnSearchIcon();
                
                var searchResultPage = searchPage.EnterSearchInput(excel.RestaurantName);
               
                var restaurantPage = searchResultPage.ClickOnSelectedSearchElemnt();
               
                restaurantPage.ClickOnFoodSearchElement();
               
                restaurantPage.TypeFoodInFoodSearchBox(excel.FoodItemName);
                var isModal = restaurantPage.AddFoodItem();
                if (isModal.Any())
                {
                    restaurantPage.ClickOnStartAFresh();
                }
                var viewCartPage = restaurantPage.ViewCart();

                viewCartPage.ClickOnDecrementButton();

                try
                {
                    Assert.That(driver.FindElement(By.XPath("//div[@class='_3Y9ZP'")).Text.Contains("Your cart is empty"));
                    Test = ExtentObject.CreateTest("Empty Cart Check");
                    Test.Pass("Empty Cart Check passed successfully");
                }
                catch
                {
                    Test = ExtentObject.CreateTest("Empty Cart Check");
                    Test.Pass("Empty Cart Check failed ");
                }

            
                driver.Navigate().GoToUrl("https://www.swiggy.com/");
            }

        }
    }
}
