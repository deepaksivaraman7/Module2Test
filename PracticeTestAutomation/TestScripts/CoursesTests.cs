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
    internal class CoursesTests:CoreCodes
    {
        [Test, Category("E2E Test")]
        public void CourseEnrollTest()
        {
            var fluentWait = FluentWait(driver);
            var loginPage = new LoginPage(driver);
            Log.Information("Course enroll test started");

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
                    string? comment = excelData.Comment;
                    string? courseindex= excelData.CourseIndex;

                    loginPage.EnterUsername(username);
                    Log.Information("Username entered");

                    loginPage.EnterPassword(password);
                    Log.Information("Password entered");

                    var successPage = loginPage.ClickSubmitButton();
                    Log.Information("Clicked submit button");

                    TakeScreenshot();
                    Assert.That(successPage.GetPageUrl(), Does.Contain("practicetestautomation.com/logged-in-successfully/"));

                    var coursesPage = fluentWait.Until(d=>successPage.ClickCoursesLink());//implementing fluent wait

                    TakeScreenshot();
                    Assert.That(coursesPage.GetPageUrl(), Does.Contain("courses"));

                    string courseLink = coursesPage.GetCourseLink(courseindex);
                    coursesPage.CLickCourseLink(courseindex);
                    Log.Information("Clicked course");

                    List<string> listWindows = driver.WindowHandles.ToList();
                    driver.SwitchTo().Window(listWindows[1]);

                    Assert.That(driver.Url, Is.EqualTo(courseLink));

                    LogTestResult("Course enroll test", "Course enroll test success");
                    driver.Close();
                    driver.SwitchTo().Window(listWindows[0]);
                }
                catch (Exception ex)
                {
                    LogTestResult("Course enroll test", "Course enroll test failed", ex.Message);
                }
            }

        }
    }
}
