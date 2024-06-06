using GoogleMapsTest.Pages;
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
        private static IWebDriver driver; // use threading

        private WebDriverManager() {}

        public static void CreateDriver(BrowserType type) {
            switch (type) {
                case BrowserType.FIREFOX:
                    FirefoxOptions fopts = new FirefoxOptions();
                    driver = new FirefoxDriver(fopts);
                    break;
                case BrowserType.SAFARI:
                case BrowserType.CHROME:
                default:
                    Console.WriteLine("Defaulting to Chrome");
                    ChromeOptions copts = new ChromeOptions();
                    copts.AddArgument("--lang=en-GB");
                    driver = new ChromeDriver(copts);
                    break; 
            }
        }

        public static void CloseDriver() {
            driver.Quit();
            driver.Dispose();
        }

        public static IWebDriver GetDriver()
        {
            return driver;
        }
    }
}
