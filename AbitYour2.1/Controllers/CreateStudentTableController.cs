using System;
using System.Text;
using System.Threading.Tasks;
using AbitYour.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AbitYour.Controllers
{
    public class CreateStudentTableController : Controller
    {
        private readonly IAbitYourParser _abitYourParser;
        private static ILogger _logger;

        public CreateStudentTableController(IAbitYourParser abitYourParser, ILoggerFactory loggerFactory)
        {
            _abitYourParser = abitYourParser;

            _logger = loggerFactory.CreateLogger("logs.log");
        }
        
        
        [HttpPost]
        public async Task<IActionResult> Index(string url, string name, string score)
        {
            IResultList result;

            try
            {
                result = await _abitYourParser.ParseAsync(url, name, score);
                SetUserDataCookies(url, name, score);
            }
            catch (ArgumentException exc)
            {
                _logger.LogWarning(CreateLogMessage(exc, url, name, score));
                ViewBag.ErrorMessage = exc.Message;
                return PartialView("Error");
            }
            catch (Exception exc)
            {

                _logger.LogError(CreateLogMessage(exc, url, name, score, true));
                if (!(exc.InnerException is null))
                    _logger.LogError(CreateLogMessage(exc.InnerException, url, name, score, true));

                ViewBag.ErrorMessage = exc.Message;
                return PartialView("Error500");
            }


            return PartialView(result);
        }

        private string CreateLogMessage(Exception exc, string url, string name, string score, bool isUnexpected = false)
        {
            var message = new StringBuilder();
            message.Append(DateTime.Now.ToString("u"));
            message.Append(isUnexpected ? " [ERROR] " : " [WARNING] ");
            message.Append($" | URL:{url} | NAME:{name} | SCORE:{score} | ");
            message.Append(exc.Message);
            return message.ToString();
        }
        
        private void SetUserDataCookies(string url, string name, string score)
        {
            var cookieExpires = Convert.ToInt32(TimeSpan.FromDays(7).TotalMinutes);
            SetCookie("url", url, cookieExpires);
            SetCookie("name", name, cookieExpires);
            SetCookie("score", score, cookieExpires);
        }

        /// <summary>  
        /// set the cookie  
        /// </summary>  
        /// <param name="key">key (unique indentifier)</param>  
        /// <param name="value">value to store in cookie object</param>  
        /// <param name="expireTime">expiration time</param>  
        private void SetCookie(string key, string value, int? expireTime)
        {
            var option = new CookieOptions
            {
                Expires = expireTime.HasValue
                    ? DateTime.Now.AddMinutes(expireTime.Value)
                    : DateTime.Now.AddMinutes(TimeSpan.FromHours(2).TotalMinutes)
            };


            Response.Cookies.Append(key, value, option);
        }
    }
}