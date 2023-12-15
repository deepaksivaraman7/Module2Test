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
    internal class LoginPage
    {
        IWebDriver driver;
        public LoginPage(IWebDriver driver)
        {
            this.driver = driver ?? throw new ArgumentException(nameof(driver));
            PageFactory.InitElements(driver, this);
        }

        //Arrange

        [FindsBy(How = How.Id, Using = "username")]
        private IWebElement? UsernameField { get; set; }

        [FindsBy(How = How.Id, Using = "password")]
        private IWebElement? PasswordField { get; set; }
        
        [FindsBy(How = How.Id, Using = "submit")]
        private IWebElement? SubmitButton { get; set; }

        [FindsBy(How = How.Id, Using = "error")]
        private IWebElement ErrorMessage { get; set; }

        //Act
        public void EnterUsername(string username)
        {
            UsernameField?.SendKeys(username);
        }
        public void EnterPassword(string username)
        {
            PasswordField?.SendKeys(username);
        }
        public SuccessPage ClickSubmitButton()
        {
            SubmitButton?.Click();
            return new SuccessPage(driver);
        }
        public string GetInvalidMessageClass()
        {
            return ErrorMessage.GetAttribute("class");
        }
        public string GetInvalidMessageText()
        {
            return ErrorMessage.Text;
        }
    }
}
