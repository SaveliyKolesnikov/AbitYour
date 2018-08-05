using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AbitYour.Models;

namespace AbitYour.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        public IActionResult Index()
        {
            ViewBag.Name = ViewBag.Url = ViewBag.Score = string.Empty;
            

            if (Request.Cookies.ContainsKey("name") &&
                Request.Cookies.ContainsKey("url") &&
                Request.Cookies.ContainsKey("score"))
            {
                ViewBag.Name  = Request.Cookies["name"];
                ViewBag.Url   = Request.Cookies["url"];
                ViewBag.Score = Request.Cookies["score"];
            }

            return View();
        }

        public IActionResult Guide()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult EntranceInfo()
        {
            return View();
        }
        
        
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}