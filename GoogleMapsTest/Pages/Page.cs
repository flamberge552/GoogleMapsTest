using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;
using System.Threading;

namespace GoogleMapsTest.Pages
{
    public class Page
    {
        private string _rootSelector;
        private double _timeout, _pollinterval;

        private IWebDriver _driver;
        private IWebElement _pageRoot;
        private DefaultWait<IWebElement> _wait;

        public Page(IWebDriver driver) : this(driver, "//body", 5.0, 250.0) {}
        public Page(IWebDriver driver, string rootSelector) : this(driver, rootSelector, 5.0, 250.0) {}

        public Page(IWebDriver driver, string rootSelector, double timeoutMS, double pollIntervalMS)
        {
            _driver = driver;
            _rootSelector = rootSelector;
            _timeout = timeoutMS;
            _pollinterval = pollIntervalMS; ;
        }

        protected void InitializePageRoot() {
            try
            {
                _pageRoot = _driver.FindElement(By.XPath(_rootSelector));
            }
            catch (NoSuchElementException e)
            {
                _rootSelector = "//body";
                Console.WriteLine($"Cannot find page root provided By [{_rootSelector}], defaulting to \"//body\". Error: {e.Message}");
                _pageRoot = _driver.FindElement(By.XPath(_rootSelector));
            }

            _wait = new DefaultWait<IWebElement>(_pageRoot)
            {
                Timeout = TimeSpan.FromSeconds(_timeout),
                PollingInterval = TimeSpan.FromMilliseconds(_pollinterval)
            };
            _wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
        }

        protected IWebDriver GetDriver() { return _driver; }

        protected IWebElement FindElement(By by) {
            ReloadRoot();
            Console.WriteLine($"Finding element [{by}]");
            IWebElement el = _wait.Until(e => _pageRoot.FindElement(by));

            return el;
        }

        protected ReadOnlyCollection<IWebElement> FindElements(By by) {
            ReloadRoot();
            Console.WriteLine($"Finding elements [{by}]");
            ReadOnlyCollection<IWebElement> els = _wait.Until(e => _pageRoot.FindElements(by));

            return els;
        }

        protected void WaitForPageLoadComplete() {
            new WebDriverWait(_driver, TimeSpan.FromSeconds(5.0)).Until(
                d => ((IJavaScriptExecutor) d).ExecuteScript("return document.readyState").Equals("complete"));
        }

        private void ReloadRoot()
        {
            _pageRoot = _driver.FindElement(By.XPath(_rootSelector));
        }
    }
}
