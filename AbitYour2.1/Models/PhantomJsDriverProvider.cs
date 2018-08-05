using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenQA.Selenium.PhantomJS;

namespace AbitYour.Models
{
    public static class PhantomJsDriverProvider
    {
        private static readonly PhantomJSDriver _driver;

        static PhantomJsDriverProvider()
        {
            _driver = new PhantomJSDriver(AppDomain.CurrentDomain.BaseDirectory);
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(100);
        }

        public static string GetPageContent(string url)
        {
            _driver.Url = url;
            _driver.Navigate();

            return _driver.PageSource;
        }
    }
}
