using GoogleMapsTest.Pages;
using GoogleMapsTest.Core;
using System.Configuration;

namespace GoogleMapsTest.Tests
{ 

    [TestFixture]
    public class Tests
    {
        MapPage mapPage;
        SideMenuPage sideMenuPage;

        [SetUp]
        public void Setup()
        {
            Console.WriteLine("Setup");

            WebDriverManager.CreateDriver(BrowserType.CHROME);
            mapPage = new MapPage();
        }

        [TearDown]
        public void Teardown()
        {
            Console.WriteLine("TearDown");
            ScreenshotDumper.TakeScreenshot();
            WebDriverManager.CloseDriver();
        }

        [Test]
        public void ShouldFindMountRushmoreOnTheMap() // this test is green
        {
            mapPage.Search("Mount Rushmore");

            sideMenuPage = mapPage.GetSideMenu();

            sideMenuPage.CheckImageSearchRedirectPresent();
            sideMenuPage.CheckLocationNamePresent();
            sideMenuPage.CheckInformationFilterBtnPresent();
            sideMenuPage.CheckCallToActionBtnAreaPresent();
            sideMenuPage.CheckLocationInformationAreaPresent();
            sideMenuPage.CheckAdmissionInformationAreaPresent();
            sideMenuPage.CheckAtThisPlaceAreaPresent();

            sideMenuPage.CheckLocationTitle("Mount Rushmore National Memorial");
        }

       [Test]
        public void ShouldFindTheBMWOfficeInMunich() // this test is green
        {
            mapPage.Search("Frankfurter Ring 35");

            sideMenuPage = mapPage.GetSideMenu();

            sideMenuPage.CheckLocationNamePresent();
            sideMenuPage.CheckCallToActionBtnAreaPresent();
            sideMenuPage.CheckAtThisPlaceAreaPresent();

            sideMenuPage.CheckLocationTitle("Frankfurter Ring 35");
        }

        [Test]
        public void ShouldFindBerlinOnTheMap() // this test is red due to failing CheckInformationFilterBtnPresent
        {
            mapPage.Search("Berlin");

            sideMenuPage = mapPage.GetSideMenu();

            sideMenuPage.CheckImageSearchRedirectPresent();
            sideMenuPage.CheckLocationNamePresent();
            sideMenuPage.CheckInformationFilterBtnPresent();
            sideMenuPage.CheckCallToActionBtnAreaPresent();
            sideMenuPage.CheckLocationInformationAreaPresent();
            sideMenuPage.CheckAdmissionInformationAreaPresent();
            sideMenuPage.CheckAtThisPlaceAreaPresent();

            sideMenuPage.CheckLocationTitle("Berlin");
        }

        [Test]
        public void ShouldNotFindAnything()
        {
            mapPage.Search("======================");

            sideMenuPage = mapPage.GetSideMenu();

            sideMenuPage.CheckInvalidLocationHeader("======================");
        }
    }
}