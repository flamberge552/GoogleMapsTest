using OpenQA.Selenium;
using NUnit.Framework.Api;
using System.Runtime.CompilerServices;

namespace GoogleMapsTest.Pages
{
    public class SideMenuPage : Page {
        private static readonly string _rootSelector = "//div[@role=\"main\"]";

        private readonly By imageSearchRedirectBy = By.XPath("//button[./img and contains(@jsaction, \"heroHeaderImage\")]");
        private readonly By locationNameBy = By.XPath("//h1[@class=\"DUwDvf lfPIob\"]");
        private readonly By informationFilterBtnAreaBy = By.XPath("//div[@role=\"tablist\"]"); // optional
        private readonly By callToActionBtnAreaBy = By.XPath("//div[contains(@aria-label, \"Actions\")]");
        private readonly By locationInformationAreaBy = By.XPath("//div[contains(@aria-label, \"About\")]");
        private readonly By admissionInformationAreaBy = By.XPath("//div[@class=\"m6QErb XiKgde \"]");
        private readonly By atThisPlaceAreaBy = By.XPath("//div[@class=\"m6QErb XiKgde \"]"); // for addresses only

        public SideMenuPage() : base(_rootSelector)
        {
            Console.WriteLine("Loading Side Menu Page");
            InitializePageRoot();

            Assert.That(GetNullableElement(By.XPath(_rootSelector)).Displayed, Is.True, "Page root not found!");
        }

        public void CheckInvalidLocationHeader(string title) {
            IWebElement el = GetElement(By.XPath(_rootSelector));
            StringAssert.Contains("Google Maps can't find " + title, el.Text, $"A real location was somehow found?");
        }

        public void CheckLocationTitle(string title) {
            IWebElement el = GetElement(locationNameBy);
            Assert.That(el.Text, Is.EqualTo(title), "Wrong location found!");
        }

        public void CheckImageSearchRedirectPresent() {
            IWebElement el = GetNullableElement(imageSearchRedirectBy);
            Assert.That(el, !Is.Null, "Image redirect not loaded!");
            Assert.That(el.Displayed, Is.True, "Image redirect not present!");
        }
        public void CheckLocationNamePresent()
        {
            IWebElement el = GetNullableElement(locationNameBy);
            Assert.That(el, !Is.Null, "Location name not loaded!");
            Assert.That(el.Displayed, Is.True, "Location name not present!");
        }
        public void CheckInformationFilterBtnPresent()
        {
            IWebElement el = GetNullableElement(informationFilterBtnAreaBy);
            Assert.That(el, !Is.Null, "Information Filter Btn not loaded!");
            Assert.That(el.Displayed, Is.True, "Information Filter Btn not present!");
        }
        public void CheckCallToActionBtnAreaPresent()
        {
            IWebElement el = GetNullableElement(callToActionBtnAreaBy);
            Assert.That(el, !Is.Null, "Call To Action area not loaded!");
            Assert.That(el.Displayed, Is.True, "Call To Action area not present!");
        }
        public void CheckLocationInformationAreaPresent()
        {
            IWebElement el = GetNullableElement(locationInformationAreaBy);
            Assert.That(el, !Is.Null, "Location information area not loaded!");
            Assert.That(el.Displayed, Is.True, "Location information area not present!");
        }
        public void CheckAdmissionInformationAreaPresent()
        {
            IWebElement el = GetNullableElement(admissionInformationAreaBy);
            Assert.That(el, !Is.Null, "Admission Information area not loaded!");
            Assert.That(el.Displayed, Is.True, "Admission Information area not present!");
        }
        public void CheckAtThisPlaceAreaPresent() {
            IWebElement el = GetNullableElement(atThisPlaceAreaBy);
            Assert.That(el, !Is.Null, "At This Place area not loaded!");
            Assert.That(el.Displayed, Is.True, "At This Place area not present!");
        }

    }

    public class MapPage : Page
    {
        private static readonly string _rootSelector = "//div[@id=\"content-container\"]";

        private readonly By searchInputBy = By.Id("searchboxinput");
        private readonly By mapCanvasBy = By.XPath("//canvas[@class=\"aFsglc widget-scene-canvas\"]");

        public MapPage() : base(_rootSelector) {
            Console.WriteLine("Loading Map Page");
            InitPage();
        }

        public SideMenuPage GetSideMenu() {
            WaitForPageLoadComplete();
            return new SideMenuPage();
        }

        public void InitPage() {
            string expected = "https://www.google.com/maps";
            GetDriver().Navigate().GoToUrl(expected);

            // reject cookies on fresh chrome instance
            CookiePage cookiePage = new CookiePage();
            cookiePage.RejectCookies();

            string actual = GetDriver().Url;
            // make sure we are on the correct page
            StringAssert.Contains(expected, actual, $"Expected URL to be [{expected}] but got [{actual}]");
            
            InitializePageRoot();
            WaitForPageLoadComplete();

            Assert.That(GetElement(mapCanvasBy).Displayed, Is.True, "Map not present on screen!");
        }

        public void Search(string address)
        {
            GetElement(searchInputBy).SendKeys(address);
            GetElement(searchInputBy).SendKeys(Keys.Enter);

            WaitForPageLoadComplete();

            Assert.That(GetElement(mapCanvasBy).Displayed, Is.True, "Map not present on screen!");
        }
    }
}
