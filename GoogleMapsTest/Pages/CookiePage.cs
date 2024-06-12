using OpenQA.Selenium;
using System.Diagnostics.CodeAnalysis;

namespace GoogleMapsTest.Pages
{
    // child decides if this makes sense to be used at all
    public sealed class CookiePage : Page
    {
        //TODO refactor these seemingly random attribute names
        private readonly By rejectCookiesBy = By.XPath("//*[@class=\"VtwTSb\"]//button[@jsname=\"tWT92d\"]");
        private readonly By acceptCookiesBy = By.XPath("//*[@class=\"VtwTSb\"]//button[@jsname=\"b3VHJd\"]");

        // accounts for the case when the Cookie form is not displayed, don't fail the test
        private readonly bool elementsLoaded = false;

        [AllowNull]
        private readonly IWebElement rejectCookiesBtn;
        [AllowNull]
        private readonly IWebElement acceptCookiesBtn;

        // quick and dirty way of handling cookie confirmation page
        public CookiePage() : base() {
            Console.WriteLine("Loading Cookie Page");
            InitializePageRoot();

            try
            {
                rejectCookiesBtn = GetNullableElement(rejectCookiesBy);
                acceptCookiesBtn = GetNullableElement(acceptCookiesBy);
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
