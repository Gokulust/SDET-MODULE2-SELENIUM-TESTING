using ExcelDataReader.Log;
using OpenQA.Selenium.DevTools.V117.DOM;
using Swiggy.PageObjects;
using Swiggy.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using Log = Serilog.Log;

namespace Swiggy.TestScripts
{
 [TestFixture]
    internal class SwiggyHomePageTests:CoreCodes
    {
        [Test,Order(1)]
        public void LocationTest()
        {
            LogUpdates();
            SwiggyHomePage swiggyHomePage = new(driver);
            swiggyHomePage.ChangeLocation("Thiruvananthapuram");
            try
            {
                Log.Information("Location check test started");
                Assert.That(swiggyHomePage.LocationSelectElement.Text.Contains("Thiruvananthapuram"));
                Test = ExtentObject.CreateTest("Location Check");
                Test.Pass("Location Check Passed  successfully");
                Log.Information("Location check test passed");
            }
            catch(AssertionException ex)
            {
                Test = ExtentObject.CreateTest("Location Check");
                Test.Pass("Location Check failed");
                Log.Error("Location check test failed");
            }

        }

        [Test,Order(2)]
        public void SortByRating()
        {
            LogUpdates();
            SwiggyHomePage swiggyHomePage = new(driver);
            swiggyHomePage.SortByRating();
            var ratingAfterSorting=swiggyHomePage.GetRatingsAfterSorting();
           
            try
            {
                Log.Information("Sorting Food Based on Rating test started");
                Assert.True(swiggyHomePage.CheckRatingsBeforeSortingAndAfterSorting(ratingAfterSorting));
                Test = ExtentObject.CreateTest("Sorting Food Based on Rating");
                Test.Pass("Sorting Food Based on Rating Passed successfully");
                Log.Information("Sorting Food Based on Rating test passed");
            }
            catch (AssertionException ex)
            {
                Test = ExtentObject.CreateTest("orting Food Based on Rating");
                Test.Pass("Sorting Food Based on Rating failed");
                Log.Information("Sorting Food Based on Rating test failed");
            }
            driver.Navigate().GoToUrl("https://www.swiggy.com/");
        }
        [Test]
        public void RatingMoreThanOrEqualTo4()
        {
            LogUpdates();
            SwiggyHomePage swiggyHomePage = new(driver);
            swiggyHomePage.ChangeLocation("Thiruvananthapuram");
            try
            {
                Log.Information("Sorting Food Having Rating more than or equal to 4 test started");
                Assert.True(swiggyHomePage.CheckRatingMoreThanOrEqualTo4());
                Test = ExtentObject.CreateTest("Sorting Food Having Rating more than or equal to 4 ");
                Test.Pass("Sorting Food Having Rating more than or equal to 4  Passed successfully");
                Log.Information("Sorting Food Having Rating more than or equal to 4 test passed");
            }
            catch (AssertionException ex)
            {
                Test = ExtentObject.CreateTest("Sorting Food Having Rating more than or equal to 4 ");
                Test.Pass("Sorting Food Having Rating more than or equal to 4  failed");
                Log.Error("Sorting Food Having Rating more than or equal to 4 test failed");
            }
            driver.Navigate().GoToUrl("https://www.swiggy.com/");
        }
        [Test]
        public void FSortByDeliveryTime()
        {
            LogUpdates();
            SwiggyHomePage swiggyHomePage = new(driver);
            swiggyHomePage.ChangeLocation("Thiruvananthapuram");
            try
            {
                Log.Information("Sorting Food based on Delivery Time test started");
                Assert.True(swiggyHomePage.SortByDeliveryTime());
                Test = ExtentObject.CreateTest("Sorting Food based on Delivery Time ");
                Test.Pass("Sorting Food based on Delivery Time  Passed successfully");
                Log.Information("Sorting Food based on Delivery Time test passed");
            }
            catch (AssertionException ex)
            {
                TakeScreenShot();
                Test = ExtentObject.CreateTest("Sorting Food based on Delivery Time  ");
                Test.Pass("Sorting Food based on Delivery Time  failed");
                Log.Error("Sorting Food based on Delivery Time test failed");
            }
            driver.Navigate().GoToUrl("https://www.swiggy.com/");
        }
    }
}
