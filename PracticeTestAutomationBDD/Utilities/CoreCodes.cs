using OpenQA.Selenium;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeTestAutomationBDD.Utilities
{
    internal class CoreCodes
    {
        public void TakeScreenshot(IWebDriver driver)
        {
            ITakesScreenshot ts = (ITakesScreenshot)driver;
            Screenshot ss = ts.GetScreenshot();
            string currDir = Directory.GetParent("../../../").FullName;
            string filePath = currDir + "/Screenshots/Screenshot_" + DateTime.Now.ToString("yyyyMMdd_Hmmss") + ".png";
            ss.SaveAsFile(filePath);
        }
        protected void LogTestResult(string testName, string result, string errorMessage = null)
        {
            Log.Information(result);
            if (errorMessage == null)
            {
                Log.Information(testName + " Passed");
            }
            else
            {
                Log.Error($"Test failed for {testName}\nException: {errorMessage}");
            }
        }
    }
}
