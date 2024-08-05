using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Service;

namespace AppiumDemoProject
{
    public class SummatorAppTest
    {
        private AndroidDriver _driver;

        private AppiumLocalService _appiumLocalService;
        [OneTimeSetUp]
        public void Setup()
        {
            _appiumLocalService = new AppiumServiceBuilder()
                .WithIPAddress("127.0.0.1")
                .UsingPort(4723)
                .Build();
            _appiumLocalService.Start();

            var androidOptions = new AppiumOptions
            {
                PlatformName = "Android",
                AutomationName = "UiAutomator2",
                DeviceName = "AppiumTestPhone",
                App = @"C:\\Users\\User\\Desktop\\com.example.androidappsummator.apk",
                PlatformVersion = "13"

            };
            _driver = new AndroidDriver(_appiumLocalService, androidOptions);
        }
        [OneTimeTearDown]
        public void TearDown()
        { 
            _driver?.Quit();
            _driver?.Dispose();
            _appiumLocalService?.Dispose();
        
        }

        [Test]
        public void TestWithValidData()
        {
            IWebElement field1 = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editText1"));
            field1.Clear();
            field1.SendKeys("1");
            IWebElement field2 = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editText2"));
            field2.Clear();
            field2.SendKeys("2");
            IWebElement calcButton = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/buttonCalcSum"));
            calcButton.Click();
            IWebElement resultField = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editTextSum"));

            Assert.That(resultField.Text, Is.EqualTo("3"));
        }

        [Test]
        public void TestWithInValidData_ClickOnlyCalcButton()
        {
            IWebElement field1 = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editText1"));
            field1.Clear();
            IWebElement field2 = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editText2"));
            field2.Clear();
            IWebElement calcButton = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/buttonCalcSum"));
            calcButton.Click();
            IWebElement resultField = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editTextSum"));

            Assert.That(resultField.Text, Is.EqualTo("error"));
        }

        [Test]
        public void TestWithInValidData_FilldOnlyFirstField()
        {
            IWebElement field1 = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editText1"));
            field1.Clear();
            field1.SendKeys("1");
            IWebElement field2 = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editText2"));
            field2.Clear();
            IWebElement calcButton = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/buttonCalcSum"));
            calcButton.Click();
            IWebElement resultField = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editTextSum"));

            Assert.That(resultField.Text, Is.EqualTo("error"));
        }

        [Test]
        [TestCase("10", "10", "20")]
        [TestCase("100", "100", "200")]
        [TestCase("1000", "1000", "2000")]
        [TestCase("0", "10", "10")]
        [TestCase("10.1", "10.9", "21.0")]
        public void TestWithValidData_Parametrized(string input1, string input2, string expectedResult)
        {
            IWebElement field1 = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editText1"));
            field1.Clear();
            field1.SendKeys(input1);
            IWebElement field2 = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editText2"));
            field2.Clear();
            field2.SendKeys(input2);
            IWebElement calcButton = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/buttonCalcSum"));
            calcButton.Click();
            IWebElement resultField = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editTextSum"));

            Assert.That(resultField.Text, Is.EqualTo(expectedResult));
        }
    }
}