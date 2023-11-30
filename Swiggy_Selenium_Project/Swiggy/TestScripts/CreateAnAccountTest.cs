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
    internal class CreateAnAccountTest:CoreCodes
    {
        [Test]
        public void CreateAnAccount()
        {
            string? currDir = Directory.GetParent(@"../../../")?.FullName;
            string? excelFilePath = currDir + "/TestData/SwiggyData.xlsx";
            string? sheetName = "CreateAnAccountData";
            Func<DataRow, CreateAnAccount> myfunction = (row) => { return new CreateAnAccount() { UserName = ExcelUtils.GetValueOrDefault(row, "UserName"), MobileNumber = ExcelUtils.GetValueOrDefault(row, "MobileNumber"), Email = ExcelUtils.GetValueOrDefault(row, "Email") }; };
            List<CreateAnAccount> excelSaerchData = ExcelUtils.ReadSearchData(excelFilePath, sheetName, myfunction);
            SwiggyHomePage swiggyHomePage=new SwiggyHomePage(driver);
            foreach(var excel in excelSaerchData)
            {
               
                swiggyHomePage.ClickOnSignInIcon();
                try
                {

                    Assert.That(driver.FindElement(By.XPath("//div[text()='Login']")).Text.Contains("Login"));
                    Test = ExtentObject.CreateTest("Sign In Page Loading");
                    Test.Pass("Sign In Page successfully");
                }
                catch (AssertionException ex)
                {
                    Test = ExtentObject.CreateTest("Sign In Page Loding");
                    Test.Fail("Sign In Page Loaded failed");
                }
                swiggyHomePage.ClickOnCreateAnAccount();
                try
                {

                    Assert.That(driver.FindElement(By.XPath("//div[text()='Sign up']")).Text.Contains("Sign up"));
                    Test = ExtentObject.CreateTest("Sign up Page Loading");
                    Test.Pass("Sign up Page successfully");
                }
                catch (AssertionException ex)
                {
                    Test = ExtentObject.CreateTest("Sign up Page Loding");
                    Test.Fail("Sign up Page Loaded failed");
                }
                swiggyHomePage.CreatAnAccount(excel.UserName, excel.MobileNumber, excel.Email);
                Thread.Sleep(2000);
                driver.Navigate().GoToUrl("https://www.swiggy.com/");
            }
            
           
            
        }
    }
}
