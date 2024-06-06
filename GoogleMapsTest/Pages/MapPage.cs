using OpenQA.Selenium;
using NUnit.Framework.Api;

namespace GoogleMapsTest.Pages
{
    public class MapPage : Page
    {
        private static readonly string _rootSelector = "//div[@id=\"content-container\"]";

        private By searchInputBy = By.Id("searchboxinput");
        private By imageSearchRedirectBy = By.XPath("//button[./img and contains(@jsaction, \"heroHeaderImage\")]");
        private By locationNameBy = By.XPath("//h1[@class=\"DUwDvf lfPIob\"]");
        private By informationFilterBtnAreaBy = By.XPath("//div[@role=\"tablist\"]");
        private By callToActionBtnAreaBy = By.XPath("//div[contains(@aria-label, \"Actions\")]");
        private By locationInformationAreaBy = By.XPath("//div[contains(@aria-label, \"About\")]");
        private By admissionInformationAreaBy = By.XPath("//div[@class=\"m6QErb XiKgde \"]");

        private IWebElement searchInputBtn;
        private IWebElement imageSearchRedirectBtn;
        private IWebElement locationNameText;
        private IWebElement informationFilterBtnArea;
        private IWebElement callToAction;
        private IWebElement locationInformationArea;
        private IWebElement admissionInformationArea;

        public MapPage(IWebDriver driver) : base(driver, _rootSelector) {
            InitPage();
        }

        public void InitPage() {
            string expected = "https://www.google.com/maps";
            GetDriver().Navigate().GoToUrl(expected);

            CookiePage cookiePage = new CookiePage(GetDriver());
            cookiePage.RejectCookies();

            string actual = GetDriver().Url;
            StringAssert.Contains(expected, actual, $"Expected URL to be [{expected}] but got [{actual}]");
            
            InitializePageRoot();
            WaitForPageLoadComplete();

            searchInputBtn = FindElement(searchInputBy);
        }

        public void SearchForLocation(string address)
        {
            searchInputBtn.SendKeys(address);
            searchInputBtn.SendKeys(Keys.Enter);

            VerifyLocationInformationElements();
        }

        public void SearchForEstablishment(string address)
        {
            searchInputBtn.SendKeys(address);
            searchInputBtn.SendKeys(Keys.Enter);

            VerifyEstablishmentInformationElements();
        }

        private void VerifyLocationInformationElements() {
            Assert.Multiple(() =>
            {
                Assert.That(FindElement(imageSearchRedirectBy).Displayed, Is.True);
                Assert.That(FindElement(locationNameBy).Displayed, Is.True);
                Assert.That(FindElement(callToActionBtnAreaBy).Displayed, Is.True);
            });
        }

        private void VerifyEstablishmentInformationElements()
        {
            Assert.Multiple(() =>
            {
                Assert.That(FindElement(imageSearchRedirectBy).Displayed, Is.True);
                Assert.That(FindElement(locationNameBy).Displayed, Is.True);
                Assert.That(FindElement(informationFilterBtnAreaBy).Displayed, Is.True);
                Assert.That(FindElement(callToActionBtnAreaBy).Displayed, Is.True);
                Assert.That(FindElement(locationInformationAreaBy).Displayed, Is.True);
                Assert.That(FindElement(admissionInformationAreaBy).Displayed, Is.True);
            });
        }
    }
}
