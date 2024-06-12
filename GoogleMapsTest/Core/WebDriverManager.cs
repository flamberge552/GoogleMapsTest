using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System.Diagnostics.CodeAnalysis;

namespace GoogleMapsTest.Core
{
    public enum BrowserType {
        CHROME, FIREFOX, SAFARI
    }

    public sealed class WebDriverManager
    {
        [AllowNull] 
        private static IWebDriver _driver; 

        private WebDriverManager() {}

        public static void CreateDriver(BrowserType type) {
            _driver = InitializeWebDriver(type);
            Console.WriteLine($"Created driver object");
        }

        public static void CloseDriver() {
            Console.WriteLine($"Closing driver");
            _driver.Quit();
            _driver.Dispose();
        }

        public static IWebDriver GetDriver()
        {
            return _driver;
        }

        private static IWebDriver InitializeWebDriver(BrowserType type) {
            switch (type)
            {
                case BrowserType.FIREFOX:
                    FirefoxOptions fopts = new FirefoxOptions();
                    _driver = new FirefoxDriver(fopts);
                    break;
                case BrowserType.CHROME:
                    Console.WriteLine("Using Chrome");
                    ChromeOptions copts = new ChromeOptions();
                    copts.AddArgument("--lang=en-GB");
                    _driver = new ChromeDriver(copts);
                    break;
                case BrowserType.SAFARI: // working on windows
                default:
                    break;
            }

            return _driver;
        }
    }
}
