using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using PracticeTestAutomation.Utilities;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeTestAutomation.PageObjects
{
    internal class ContactPage
    {
        IWebDriver driver;
        public ContactPage(IWebDriver driver)
        {
            this.driver = driver ?? throw new ArgumentException(nameof(driver));
            PageFactory.InitElements(driver, this);
        }

        //Arrange
        [CacheLookup]
        [FindsBy(How = How.Id, Using = "wpforms-161-field_0")]
        private IWebElement? FirstNameField { get; set; }

        [CacheLookup]
        [FindsBy(How = How.Id, Using = "wpforms-161-field_0-last")]
        private IWebElement? LastNameField { get; set; }

        [CacheLookup]
        [FindsBy(How = How.Id, Using = "wpforms-161-field_1")]
        private IWebElement? EmailField { get; set; }

        [CacheLookup]
        [FindsBy(How = How.Id, Using = "wpforms-161-field_2")]
        private IWebElement? CommentsField { get; set; }

        //Act

        public void EnterFirstName(string firstName)
        {
            FirstNameField?.SendKeys(firstName);
        }
        public void EnterLastName(string lastName)
        {
            LastNameField?.SendKeys(lastName);
        }
        public void EnterEmail(string email)
        {
            EmailField?.SendKeys(email);
        }
        public void EnterComment(string comment)
        {
            CommentsField?.SendKeys(comment);
        }
        public string GetPageUrl()
        {
            return driver.Url;
        }
    }
}
