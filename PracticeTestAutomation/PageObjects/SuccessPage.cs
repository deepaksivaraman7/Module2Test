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
    internal class SuccessPage
    {
        IWebDriver driver;
        public SuccessPage(IWebDriver driver)
        {
            this.driver = driver ?? throw new ArgumentException(nameof(driver));
            PageFactory.InitElements(driver, this);
        }

        //Arrange

        [FindsBy(How = How.TagName, Using = "strong")]
        private IWebElement SuccessMessage { get; set; }

        [CacheLookup]
        [FindsBy(How = How.LinkText, Using = "Log out")]
        private IWebElement LogoutButton { get; set; }

        [CacheLookup]
        [FindsBy(How = How.Id, Using = "menu-item-18")]
        private IWebElement ContactLink { get; set; }

        [CacheLookup]
        [FindsBy(How = How.Id, Using = "menu-item-21")]
        private IWebElement CoursesLink { get; set; }

        //Act
        public string GetPageUrl()
        {
            return driver.Url;
        }
        public string GetSuccessMessage()
        {
           return SuccessMessage.Text;
        }
        public bool IsLogoutButtonPresent()
        {
          return LogoutButton.Displayed;
        }
        public ContactPage ClickContactLink()
        {
            ContactLink.Click();
            return new ContactPage(driver);
        }
        public CoursesPage ClickCoursesLink()
        {
            CoursesLink.Click();
            return new CoursesPage(driver);
        }
    }
}
