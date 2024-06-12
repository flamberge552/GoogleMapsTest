using GoogleMapsTest.Core;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;
using System.Threading;

namespace GoogleMapsTest.Pages
{
    public class Page
    {
        private string _rootSelector;
        private readonly double _timeout, _pollinterval;

        private readonly IWebDriver _driver;
        private IWebElement _pageRoot;
        private DefaultWait<IWebElement> _wait;

        public Page() : this("//body", 5.0, 250.0) {}
        public Page(string rootSelector) : this(rootSelector, 5.0, 250.0) {}

        public Page(string rootSelector, double timeoutMS, double pollIntervalMS)
        {
            _driver = WebDriverManager.GetDriver();
            _rootSelector = rootSelector;
            _timeout = timeoutMS;
            _pollinterval = pollIntervalMS;
        }

        protected void InitializePageRoot() {
            try
            {
                _pageRoot = new WebDriverWait(_driver, TimeSpan.FromSeconds(_timeout)).Until(d => d.FindElement(By.XPath(_rootSelector)));
            }
            catch (NoSuchElementException e)
            {
                Console.WriteLine($"Cannot find page root provided By [{_rootSelector}], defaulting to \"//body\". Error: {e.Message}");
                _rootSelector = "//body";
                _pageRoot = new WebDriverWait(_driver, TimeSpan.FromSeconds(_timeout)).Until(d => d.FindElement(By.XPath(_rootSelector)));
            }

            _wait = new DefaultWait<IWebElement>(_pageRoot)
            {
                Timeout = TimeSpan.FromSeconds(_timeout),
                PollingInterval = TimeSpan.FromMilliseconds(_pollinterval)
            };
            _wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
        }

        protected IWebDriver GetDriver() { return _driver; }

        protected IWebElement GetElement(By by)
        {
            Console.WriteLine($"Finding element [{by}]");
            IWebElement el;
            try
            {
                el = _wait.Until(e => _pageRoot.FindElement(by));
                return el;
            }
            catch (NoSuchElementException e)
            {
                Console.WriteLine(e.Message);
            }

            throw new Exception("Cannot instantiate object!");
        }

        protected IWebElement GetNullableElement(By by)
        {
            Console.WriteLine($"Finding nullable element [{by}]");
            IWebElement element;

            try
            {
                element = _wait.Until(e => _pageRoot.FindElement(by));
                return element;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return null;
        }

        protected void WaitForPageLoadComplete()
        {
            try
            {
                new WebDriverWait(_driver, TimeSpan.FromSeconds(_timeout)).Until(
                    e => _pageRoot.FindElement(By.XPath($"{_rootSelector + "//div"}[last()]"))
                );
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
