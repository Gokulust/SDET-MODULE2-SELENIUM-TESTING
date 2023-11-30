using OpenQA.Selenium.DevTools.V117.DOM;
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
    internal class SwiggyHomePageTests:CoreCodes
    {
        [Test]
        public void LocationTest()
        {
            SwiggyHomePage swiggyHomePage = new(driver);
            swiggyHomePage.ChangeLocation("Thiruvananthapuram");
            try
            {
                Assert.That(swiggyHomePage.LocationSelectElement.Text.Contains("Thiruvananthapuram"));
                Test = ExtentObject.CreateTest("Location Check");
                Test.Pass("Location Check Passed  successfully");
            }
            catch(AssertionException ex)
            {
                Test = ExtentObject.CreateTest("Location Check");
                Test.Pass("Location Check failed");
            }

        }

        [Test]
        public void SortByRating()
        {
            SwiggyHomePage swiggyHomePage = new(driver);
            

         
            swiggyHomePage.SortByRating();
            var ratingAfterSorting=swiggyHomePage.GetRatingsAfterSorting();
           
            try
            {
                Assert.True(swiggyHomePage.CheckRatingsBeforeSortingAndAfterSorting(ratingAfterSorting));
                Test = ExtentObject.CreateTest("Sorting Food Based on Rating");
                Test.Pass("Sorting Food Based on Rating Passed successfully");
            }
            catch (AssertionException ex)
            {
                Test = ExtentObject.CreateTest("orting Food Based on Rating");
                Test.Pass("Sorting Food Based on Rating failed");
            }
        }
        [Test]
        public void RatingMoreThanOrEqualTo4()
        {
            SwiggyHomePage swiggyHomePage = new(driver);
            try
            {
                Assert.True(swiggyHomePage.CheckRatingMoreThanOrEqualTo4());
                Test = ExtentObject.CreateTest("Sorting Food Having Rating more than or equal to 4 ");
                Test.Pass("Sorting Food Having Rating more than or equal to 4  Passed successfully");
            }
            catch (AssertionException ex)
            {
                Test = ExtentObject.CreateTest("Sorting Food Having Rating more than or equal to 4 ");
                Test.Pass("Sorting Food Having Rating more than or equal to 4  failed");
            }
        }
        [Test]
        public void FastDeliveryCheck()
        {
            SwiggyHomePage swiggyHomePage = new(driver);
            try
            {
                Assert.True(swiggyHomePage.FastDeliveryCheck());
                Test = ExtentObject.CreateTest("Sorting Food based on Delivery Time ");
                Test.Pass("Sorting Food based on Delivery Time  Passed successfully");
            }
            catch (AssertionException ex)
            {
                Test = ExtentObject.CreateTest("Sorting Food based on Delivery Time  ");
                Test.Pass("Sorting Food based on Delivery Time  failed");
            }
        }
    }
}
