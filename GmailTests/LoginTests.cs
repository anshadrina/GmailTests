using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

namespace GmailTests
{
    public class loginTests 
    {
        private IWebDriver driver;
        string userName = "an_shadrina@mail.ru";
        string password = "Sh1524";
        string falseUserName = "lkenlanlcanlecna";
        string errorMessage = "Неверное имя ящика";
   
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
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            INavigation navigation = driver.Navigate();
            navigation.GoToUrl("https://mail.ru/");
            IWebElement userNameField = driver.FindElement(LoginLockators.userNameFieldXpath);
            userNameField.SendKeys(userName);
            IWebElement furtherButton = driver.FindElement(LoginLockators.furtherButtonXpath);
            furtherButton.Click();
            IWebElement passwordField = driver.FindElement(LoginLockators.passwordFieldXpath);
            passwordField.SendKeys(password);
            IWebElement loginButton = driver.FindElement(LoginLockators.loginButtonXpath);
            loginButton.Click();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            var res = wait.Until((d) => { return d.FindElement(LoginLockators.accountBlockXpath); });
            IWebElement loginProof = driver.FindElement(LoginLockators.accountBlockXpath));
            string proof = loginProof.GetAttribute("aria-label");
            Assert.AreEqual(userName, proof);
        }

        [Test]
        public void Test2()
        {
            INavigation navigation = driver.Navigate();
            navigation.GoToUrl("https://mail.ru/");
            IWebElement userNameField = driver.FindElement(LoginLockators.userNameFieldXpath) ;
            userNameField.SendKeys(falseUserName);
            IWebElement furtherButton = driver.FindElement(LoginLockators.furtherButtonXpath);
            furtherButton.Click();
            var startTime = DateTime.UtcNow;
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(1));
            wait.Until((d) => { return DateTime.UtcNow - startTime > TimeSpan.FromSeconds(1); }); 
            IWebElement error = driver.FindElement(LoginLockators.errorMessage);
            string borderColor = userNameField.GetCssValue("border-color");
            Assert.AreEqual("rgb(244, 78, 78)", borderColor);
            Assert.AreEqual(errorMessage,error.Text);
        }
    }
}