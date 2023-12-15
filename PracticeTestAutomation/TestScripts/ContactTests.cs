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
    internal class ContactTests : CoreCodes
    {
        [Test, Category("E2E Test")]
        public void ContactPageTest()
        {
            var loginPage = new LoginPage(driver);
            Log.Information("Contact page test started");

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
                    string? firstname = excelData.FirstName;
                    string? lastname = excelData.LastName;
                    string? email = excelData.Email;
                    string? comment=excelData.Comment;

                    loginPage.EnterUsername(username);
                    Log.Information("Username entered");

                    loginPage.EnterPassword(password);
                    Log.Information("Password entered");

                    var successPage = loginPage.ClickSubmitButton();
                    Log.Information("Clicked submit button");

                    TakeScreenshot();
                    Assert.That(successPage.GetPageUrl(), Does.Contain("practicetestautomation.com/logged-in-successfully/"));

                    var contactPage=successPage.ClickContactLink();

                    TakeScreenshot();
                    Assert.That(contactPage.GetPageUrl(), Does.Contain("contact"));

                    contactPage.EnterFirstName(firstname);
                    Log.Information("First name entered");

                    contactPage.EnterLastName(lastname);
                    Log.Information("Last name entered");

                    contactPage.EnterEmail(email);
                    Log.Information("Email entered");

                    contactPage.EnterFirstName(comment);
                    Log.Information("Comment entered");

                    LogTestResult("Contact test", "Contact test success");
                }
                catch (Exception ex)
                {
                    LogTestResult("Contact test", "Contact test failed", ex.Message);
                }
            }

        }
    }
}
