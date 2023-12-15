using PracticeTestAutomation.PageObjects;
using PracticeTestAutomation.Utilities;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeTestAutomation.TestScripts
{
    internal class InvalidLoginTests:CoreCodes
    {
        [Test, Order(1), Category("Smoke Test"),TestCaseSource(nameof(InvalidUserNames))]
        public void NegativeUsernameTest(string invalidUserName)
        {
            var loginPage = new LoginPage(driver);
            Log.Information("Login test started");

            string? excelFilePath = currDir + "/TestData/InputData.xlsx";
            string? sheetName = "Valid credentials";
            List<UserDetails> excelDataList = ExcelUtilities.ReadExcelData(excelFilePath, sheetName);

            foreach (var excelData in excelDataList)
            {
                if (!driver.Url.Equals("https://practicetestautomation.com/practice-test-login/"))
                {
                    driver.Navigate().GoToUrl("https://practicetestautomation.com/practice-test-login");
                }
                try
                {
                    TakeScreenshot();
                    Assert.That(driver.Title, Does.Contain("Test Login"));

                    string? password = excelData.Password;

                    loginPage.EnterUsername(invalidUserName);
                    Log.Information("Username entered");

                    loginPage.EnterPassword(password);
                    Log.Information("Password entered");

                    loginPage.ClickSubmitButton();
                    Log.Information("Clicked submit button");

                    TakeScreenshot();
                    Assert.Multiple(() =>
                    {
                        Assert.That(loginPage.GetInvalidMessageClass(), Is.EqualTo("show"));
                        Assert.That(loginPage.GetInvalidMessageText(), Is.EqualTo("Your username is invalid!"));
                    });
                    LogTestResult("Negative username test", "Negative username success");
                }
                catch (Exception ex)
                {
                    LogTestResult("Negative username test", "Negative username failed", ex.Message);
                }

            }
        }
        [Test, Order(2), Category("Smoke Test"),TestCaseSource(nameof(InvalidPasswords))]
        public void NegativePasswordTest(string invalidPassword)
        {
            var loginPage = new LoginPage(driver);
            Log.Information("Login test started");

            string? excelFilePath = currDir + "/TestData/InputData.xlsx";
            string? sheetName = "Valid credentials";
            List<UserDetails> excelDataList = ExcelUtilities.ReadExcelData(excelFilePath, sheetName);

            foreach (var excelData in excelDataList)
            {
                if (!driver.Url.Equals("https://practicetestautomation.com/practice-test-login/"))
                {
                    driver.Navigate().GoToUrl("https://practicetestautomation.com/practice-test-login");
                }
                try
                {
                    TakeScreenshot();
                    Assert.That(driver.Title, Does.Contain("Test Login"));

                    string? username = excelData.Username;

                    loginPage.EnterUsername(username);
                    Log.Information("Username entered");

                    loginPage.EnterPassword(invalidPassword);
                    Log.Information("Password entered");

                    loginPage.ClickSubmitButton();
                    Log.Information("Clicked submit button");

                    TakeScreenshot();
                    Assert.Multiple(() =>
                    {
                        Assert.That(loginPage.GetInvalidMessageClass(), Is.EqualTo("show"));
                        Assert.That(loginPage.GetInvalidMessageText(), Is.EqualTo("Your password is invalid!"));
                    });

                    LogTestResult("Negative password test", "Negative password success");
                }
                catch (Exception ex)
                {
                    LogTestResult("Negative password test", "Negative password failed", ex.Message);
                }
            }
        }
        static object[] InvalidUserNames()
        {
            return new object[]
            {
                new object[]{"incorrectUser"}
            };
        }
        static object[] InvalidPasswords()
        {
            return new object[]
            {
                new object[]{"incorrectPassword"}
            };
        }
    }
}
