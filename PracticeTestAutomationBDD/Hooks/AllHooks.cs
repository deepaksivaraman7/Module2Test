using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Serilog;
using TechTalk.SpecFlow;

namespace PracticeTestAutomationBDD.Hooks
{
    [Binding]
    public sealed class AllHooks
    {
        public static IWebDriver? driver;

        [BeforeFeature(Order =1)]
        public static void InitializeBrowser()
        {
            driver = new ChromeDriver();
        }

        [BeforeFeature(Order = 2)]
        public static void LogFileCreate()
        {
            string currDir = Directory.GetParent("../../../").FullName;
            string logFileilePath = currDir + "/Logs/practicetest" + DateTime.Now.ToString("yyyyMMdd_Hmmss") + ".txt";
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(logFileilePath, rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }

        [AfterFeature]
        public static void Cleanup()
        {
            driver?.Quit();
            Log.CloseAndFlush();
        }
    }
}