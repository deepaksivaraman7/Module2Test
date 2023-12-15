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
    internal class CoursesPage
    {
        IWebDriver driver;
        DefaultWait<IWebDriver> fluentWait;
        public CoursesPage(IWebDriver driver)
        {
            this.driver = driver ?? throw new ArgumentException(nameof(driver));
            PageFactory.InitElements(driver, this);
            fluentWait = CoreCodes.FluentWait(this.driver);
        }

        //Act
        public void CLickCourseLink(string position)
        {
            driver.FindElement(By.XPath("//div[@class='post-content']//following::a["+position+"]")).Click();
        }
        public string GetCourseLink(string position)
        {
            return driver.FindElement(By.XPath("//div[@class='post-content']//following::a[" + position + "]")).GetAttribute("href");
        }
        public string GetPageUrl()
        {
            return driver.Url;
        }
    }
}
