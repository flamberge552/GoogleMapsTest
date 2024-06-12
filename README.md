# Google Maps Tests

## Description
Automation testing assingment. A simple testing framework written in C#, NUnit, and Selenium for browser automation.

### Project Depdenencies
The project uses several external NUGet packages, namely:<br>
    - Microsoft.NET.Test.Sdk 17.8.0<br>
    - NUnit 3.14.0<br>
    - NUnit3TestAdapter 4.5.0<br>
    - Selenium.WebDriver 4.21.0<br>

To build, open in Visual Studio and hit Build.

### Project Scaffolding
The project contains 3 folders, Core, Pages, and Tests.<br>

<code>Core<br>
|- ScreenshotDumper.cs<br>
|- WebDriverManager.cs<br>
Pages<br>
|- CookiePage.cs<br>
|- MapPage.cs<br>
|- Page.cs<br>
Tests<br>
|- GoogleMapTests.cs<br>
</code>

### Running Tests
Tests can be run using the NUnit adapter in Visual Studio 2019/2022, from Test -> Run All Tests.
NUnit configuration can be found in the test.runsettings file, where the test output folder is specified.

Tests can also be run using the following command:<br>
<code>cd "checkoutDir"</code><br>
<code>dotnet test --settings:test.runsettings</code>

### Additional Information
All tests are run in seperate browser instances. They are currently run sequentially. Tested with Chrome and Firefox.
The browser is used with a simple Factory/Manager design, references to the object are kept to a minimum.
Each page will inherit from the Page class, and re-use the GetElement and GetNullableElement methods.
Waits are implemented at the Page level.
The ScreenshotDumper class uses the current test name to determine the screenshot name.
Screenshots are saved under <code>"checkoutDir/TestResults/(username)_(computername)_timestamp/In"</code> folder.

## Good to have
Some notes from the developer: 
1. Better handling of Test cases would have been good, some automated ways of doing cleanup instead of stuffing everything in a single TestFixture
2. Screenshots would have been better handles by having an Observer to get notified when a test step is complete.
3. Test data handling is practically inexistent, tried to using App.config method, but it felt like I was going way too deep into Microsoft implementation and would risk breaking multiplatform support
4. CI integration was something I spent some time on, but hit a brick wall when trying to run the tests using nunit3-console, only supported report output was XML, nothing I could quickly hack together which allowed me to save screenshots directly in the report (not that the current implementation supports that either way)
