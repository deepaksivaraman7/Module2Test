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
    [TestFixture]
    internal class ValidLoginTests:CoreCodes
    {
        [Test, Category("Smoke Test")]
        public void PositiveLoginTest()
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
                    string? password = excelData.Password;

                    loginPage.EnterUsername(username);
                    Log.Information("Username entered");

                    loginPage.EnterPassword(password);
                    Log.Information("Password entered");

                    var successPage = loginPage.ClickSubmitButton();
                    Log.Information("Clicked submit button");

                    TakeScreenshot();
                    Assert.Multiple(() =>
                    {
                        Assert.That(successPage.GetPageUrl(), Does.Contain("practicetestautomation.com/logged-in-successfully/"));
                        Assert.That(successPage.GetSuccessMessage(), Does.Contain("Congratulations"));
                        Assert.That(successPage.IsLogoutButtonPresent(), Is.True);
                    });
                    LogTestResult("Positive login test", "Positive login success");
                }
                catch (Exception ex)
                {
                    LogTestResult("Positive login test", "Positive login failed", ex.Message);
                }

            }
        }
    }
}
