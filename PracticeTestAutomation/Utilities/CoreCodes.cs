using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeTestAutomation.Utilities
{
    internal class CoreCodes
    {
        Dictionary<string, string>? properties; //Declaring
        public IWebDriver driver;

        public ExtentReports extent;
        ExtentSparkReporter sparkReporter;
        public ExtentTest? test;
        public string currDir = Directory.GetParent("../../../").FullName;

        public void ReadConfigSettings()
        {
            properties = new(); //Initializing
            string fileName = currDir + "/configsettings/config.properties";
            string[] lines = File.ReadAllLines(fileName);
            foreach (string line in lines)
            {
                if (!string.IsNullOrWhiteSpace(line) && line.Contains('='))
                {
                    string[] parts = line.Split('=');
                    string key = parts[0].Trim();
                    string value = parts[1].Trim();
                    properties[key] = value;
                }
            }
        }
        public static DefaultWait<IWebDriver> FluentWait(IWebDriver driver)
        {
            DefaultWait<IWebDriver> fluentWait = new(driver);
            fluentWait.Timeout = TimeSpan.FromSeconds(5);
            fluentWait.PollingInterval = TimeSpan.FromMilliseconds(100);
            fluentWait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            fluentWait.Message = "Element not found";
            return fluentWait;
        }
        public void TakeScreenshot()
        {
            ITakesScreenshot ts = (ITakesScreenshot)driver;
            Screenshot ss = ts.GetScreenshot();
            string filePath = currDir + "/Screenshots/Screenshot_" + DateTime.Now.ToString("yyyyMMdd_Hmmss") + ".png";
            ss.SaveAsFile(filePath);
        }
        protected void LogTestResult(string testName, string result, string errorMessage = null)
        {
            Log.Information(result);
            test = extent.CreateTest(testName);
            if (errorMessage == null)
            {
                test.Pass(result);
            }
            else
            {
                test.Fail(result);
                Log.Error($"Test failed for {testName}\nException: {errorMessage}");
            }
        }
        [OneTimeSetUp]
        public void InitializeBrowser()
        {
            string logFilePath = currDir + "/Logs/Log_" + DateTime.Now.ToString("yyyyMMdd_Hmmss") + ".txt";
            Log.Logger = new LoggerConfiguration()
                    .WriteTo.Console()
                    .WriteTo.File(logFilePath, rollingInterval: RollingInterval.Day)
                    .CreateLogger();
            extent = new ExtentReports();
            sparkReporter = new ExtentSparkReporter(currDir + "/ExtentReports/extent-report" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".html");
            extent.AttachReporter(sparkReporter);
            ReadConfigSettings();
            if (properties["browser"].ToLower() == "chrome")
            {
                driver = new ChromeDriver();
            }
            else if (properties["browser"].ToLower() == "edge")
            {
                driver = new EdgeDriver();
            }
            driver.Url = properties["baseUrl"];
            driver.Manage().Window.Maximize();
        }

        [OneTimeTearDown]
        public void Cleanup()
        {
            driver.Quit();
            extent.Flush();
            Log.CloseAndFlush();
        }
    }
}
