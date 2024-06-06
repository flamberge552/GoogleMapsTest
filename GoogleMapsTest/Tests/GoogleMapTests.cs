using NUnit.Framework;
using OpenQA.Selenium;

using GoogleMapsTest.Pages;
using GoogleMapsTest.Core;
using System.Runtime.CompilerServices;

namespace GoogleMapsTest.Tests
{
    [TestFixture]
    //[Parallelizable(ParallelScope.All)]
    public class Tests
    {
        public BrowserType browserType = BrowserType.CHROME;
        MapPage mapPage;

        [SetUp]
        public void Setup()
        {
            Console.WriteLine("Setting stuff up");

            WebDriverManager.CreateDriver(browserType);
            mapPage = new MapPage(WebDriverManager.GetDriver());
        }

        [TearDown]
        public void Teardown()
        {
            Console.WriteLine("Cleaning up");

            WebDriverManager.CloseDriver();
        }

        [Test]
        public void ShouldFindMountRushmoreOnTheMap()
        {
            mapPage.SearchForEstablishment("Mount Rushmore");
        }

        [Test]
        public void ShouldFindTheGreatWallOnTheMap()
        {
            mapPage.SearchForEstablishment("The Great Wall of China");
        }

        [Test]
        public void ShouldFindNiagraFallsOnTheMap()
        {
            mapPage.SearchForEstablishment("Niagra Falls");
        }

        [Test]
        public void ShouldFindBerlinOnTheMap()
        {
            mapPage.SearchForLocation("Berlin");
        }
    }
}