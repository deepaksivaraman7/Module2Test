using OpenQA.Selenium;
using PracticeTestAutomationBDD.Utilities;
using PracticeTestAutomationBDD.Hooks;
using System;
using TechTalk.SpecFlow;
using Serilog;
using NUnit.Framework;

namespace PracticeTestAutomationBDD.StepDefinitions
{
    [Binding]
    internal class CourseEnrollSteps:CoreCodes
    {
        public static IWebDriver driver = AllHooks.driver;
        string? courseurl;
        [Given(@"User will be on the login page")]
        public void GivenUserWillBeOnTheLoginPage()
        {
            driver.Url = "https://practicetestautomation.com/practice-test-login";
            driver.Manage().Window.Maximize();
        }

        [When(@"User will type '([^']*)' in the username field")]
        public void WhenUserWillTypeInTheUsernameField(string username)
        {
            driver.FindElement(By.Id("username")).SendKeys(username);
            Log.Information("Entered " + username+" in username field");
        }

        [When(@"User will type '([^']*)' in the password field")]
        public void WhenUserWillTypeInThePasswordField(string password)
        {
            driver.FindElement(By.Id("password")).SendKeys(password);
            Log.Information("Entered " + password + " in password field");
        }

        [When(@"User will click submit button")]
        public void WhenUserWillClickSubmitButton()
        {
            driver.FindElement(By.Id("submit")).Click();
            Log.Information("Clicked submit");
        }

        [Then(@"User will be redirected to success page")]
        public void ThenUserWillBeRedirectedToSuccessPage()
        {
            TakeScreenshot(driver);
            Log.Information("Screenshot taken");
            try
            {
                Assert.That(driver?.Url, Does.Contain("practicetestautomation.com/logged-in-successfully/"));
                Log.Information("Login successful");
            }
            catch (AssertionException ex)
            {
                LogTestResult("Course enroll test","Course enroll test failed", ex.Message);
            }
        }

        [When(@"User will click on courses link")]
        public void WhenUserWillClickOnCoursesLink()
        {
            driver.FindElement(By.Id("menu-item-21")).Click();
            Log.Information("Courses link clicked");
        }

        [Then(@"User will be redirected to the courses page")]
        public void ThenUserWillBeRedirectedToTheCoursesPage()
        {
            TakeScreenshot(driver);
            Log.Information("Screenshot taken");
            try
            {
                Assert.That(driver.Url, Does.Contain("courses"));
                Log.Information("Courses page loaded successfully");
            }
            catch (AssertionException ex)
            {
                LogTestResult("Course enroll failed", "Course enroll test failed", ex.Message);
            }
        }

        [When(@"User will click on course on position '([^']*)'")]
        public void WhenUserWillClickOnCourseOnPosition(string position)
        {
            courseurl=driver.FindElement(By.XPath("//div[@class='post-content']//following::a[" + position + "]")).GetAttribute("href");
            driver.FindElement(By.XPath("//div[@class='post-content']//following::a[" + position + "]")).Click();
            Log.Information("Course clicked");
        }

        [Then(@"Another tab will open containing the course user clicked")]
        public void ThenAnotherTabWillOpenContainingTheCourseUserClicked()
        {
            List<string> listWindows = driver.WindowHandles.ToList();
            driver.SwitchTo().Window(listWindows[1]);
            TakeScreenshot(driver);
            Log.Information("Screenshot taken");
            try
            {
                Assert.That(driver.Url, Is.EqualTo(courseurl));
                LogTestResult("Course enroll test","Course enroll test passed");
            }
            catch (AssertionException ex)
            {
                LogTestResult("Course enroll test", "Course enroll test failed", ex.Message);
            }
            driver.Close();
            driver.SwitchTo().Window(listWindows[0]);
        }
    }
}
