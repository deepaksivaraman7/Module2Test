using NUnit.Framework;
using OpenQA.Selenium;
using PracticeTestAutomationBDD.Hooks;
using PracticeTestAutomationBDD.Utilities;
using Serilog;
using System;
using TechTalk.SpecFlow;

namespace PracticeTestAutomationBDD.StepDefinitions
{
    [Binding]
    internal class InvalidLoginSteps:CoreCodes
    {
        public static IWebDriver driver = AllHooks.driver;

        [Then(@"Invalid username message should be show on the same page")]
        public void ThenErrorMessageShouldBeShowOnTheSamePage()
        {
            IWebElement errorMessage = driver.FindElement(By.Id("error"));
            TakeScreenshot(driver);
            Log.Information("Screenshot taken");
            try
            {
                Assert.Multiple(() =>
                {
                    Assert.That(errorMessage.GetAttribute("class"), Is.EqualTo("show"));
                    Assert.That(errorMessage.Text, Is.EqualTo("Your username is invalid!"));
                });
                LogTestResult("Invalid username test", "Invalid username test passed");
            }
            catch (Exception ex)
            {
                LogTestResult("Invalid username test", "Invalid username test failed", ex.Message);
            }
        }

        [Then(@"Invalid password message should be show on the same page")]
        public void ThenInvalidPasswordMessageShouldBeShowOnTheSamePage()
        {
            IWebElement errorMessage = driver.FindElement(By.Id("error"));
            TakeScreenshot(driver);
            Log.Information("Screenshot taken");
            try
            {
                Assert.Multiple(() =>
                {
                    Assert.That(errorMessage.GetAttribute("class"), Is.EqualTo("show"));
                    Assert.That(errorMessage.Text, Is.EqualTo("Your password is invalid!"));
                });
                LogTestResult("Invalid password test", "Invalid username test passed");
            }
            catch (Exception ex)
            {
                LogTestResult("Invalid password test", "Invalid username test failed", ex.Message);
            }
        }

    }
}
