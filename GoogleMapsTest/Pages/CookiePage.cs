using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMapsTest.Pages
{
    // caller decides if this makes sense to be used at all
    public sealed class CookiePage : Page
    {
        //TODO refactor these seemingly random attribute names
        private By rejectCookiesBy = By.XPath("//*[@class=\"VtwTSb\"]//button[@jsname=\"tWT92d\"]");
        private By acceptCookiesBy = By.XPath("//*[@class=\"VtwTSb\"]//button[@jsname=\"b3VHJd\"]");

        // accounts for the case when the Cookie form is not displayed, don't fail the test
        private bool elementsLoaded = false;

        private IWebElement rejectCookiesBtn;
        private IWebElement acceptCookiesBtn;

        public CookiePage(IWebDriver driver) : base(driver) {
            InitializePageRoot();

            try
            {
                rejectCookiesBtn = FindElement(rejectCookiesBy);
                acceptCookiesBtn = FindElement(acceptCookiesBy);
                elementsLoaded = true;
            }
            catch (NoSuchElementException e) 
            {
                Console.WriteLine($"Elements not loaded. Is the Cookie Acceptance page not open? {e.Message}");
                elementsLoaded = false;
            }
        }

        public void AcceptCookies() 
        {
            if (elementsLoaded)
                acceptCookiesBtn.Click();
        }

        public void RejectCookies() 
        {
            if (elementsLoaded)
                rejectCookiesBtn.Click();
        }
    }
}
