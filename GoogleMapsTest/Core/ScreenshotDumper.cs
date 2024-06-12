using NUnit.Framework.Interfaces;
using OpenQA.Selenium;

namespace GoogleMapsTest.Core
{
    class ScreenshotDumper {
        public static void TakeScreenshot(string fileName) {
            string testDir = TestContext.CurrentContext.TestDirectory;
            string ssName = Path.Combine(testDir, fileName + ".jpg");
            ((ITakesScreenshot) WebDriverManager.GetDriver()).GetScreenshot();

            TestContext.AddTestAttachment(ssName, fileName);
        }

        public static void TakeScreenshot() {
            TakeScreenshot(TestContext.CurrentContext.Test.Name);
        }
    }
}
