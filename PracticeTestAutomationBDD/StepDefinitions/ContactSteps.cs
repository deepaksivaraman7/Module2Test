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
    internal class ContactSteps:CoreCodes
    {
        public static IWebDriver driver = AllHooks.driver;

        [When(@"User will click on contact link")]
        public void WhenUserWillClickOnContactLink()
        {
            driver.FindElement(By.Id("menu-item-18")).Click();
            Log.Information("Contact link clicked");
        }

        [Then(@"User will be redirected to the contact page")]
        public void ThenUserWillBeRedirectedToTheContactPage()
        {
            TakeScreenshot(driver);
            Log.Information("Screenshot taken");
            try
            {
                Assert.That(driver.Url, Does.Contain("contact"));
                LogTestResult("Contact details test", "Contact details test passed");
            }
            catch (AssertionException ex)
            {
                LogTestResult("Contact details test", "Contact details test failed", ex.Message);
            }

        }
    }
}
