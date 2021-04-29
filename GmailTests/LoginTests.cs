using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;

namespace GmailTests
{
    public class loginTests 
    {
        private IWebDriver driver;

        [OneTimeSetUp]
        public void Init()
        {
            driver = new ChromeDriver(new ChromeOptions { });
        }

        [OneTimeTearDown]
        public void Close()
        {
            driver.Quit();
        }

        
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void Test1()
        {
            INavigation navigation = driver.Navigate();
            navigation.GoToUrl("https://www.google.com/intl/de/gmail/about/#");
            IWebElement logInButton = driver.FindElement(By.XPath("/html/body/div[2]/div[1]/div[4]/ul[1]/li[2]/a"));
            logInButton.Click();
            Assert.Pass();
        }
    }
}