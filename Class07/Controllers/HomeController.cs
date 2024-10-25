using Class07.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Class07.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddCookies()
        {
            // this is just to test the use of web cookies
            // we should see them in the response messages and in the subsequent requests
            HttpContext.Response.Cookies.Append("Test1", "Value1"); // session cookie
            HttpContext.Response.Cookies.Append("Test2", "Value2",
                new CookieOptions() { Expires = DateTime.Now.AddSeconds(10) }); // persistant cookie (for 10 seconds)
            HttpContext.Response.Cookies.Append("Test3", "Value3",
                new CookieOptions() { Expires = DateTime.Now.AddDays(1) }); // persistant cookie (for 1 day)

            /// we could have some application logic code...
            /// 
            return RedirectToAction("Index");
        }

        public IActionResult DeleteCookies()
        {
            // delete all cookies from response (and client)
            foreach (var item in HttpContext.Request.Cookies.Keys)
            {
                HttpContext.Response.Cookies.Delete(item);
            }

            return RedirectToAction("Index");
        }

        public IActionResult AddSessionVariables()
        {
            // we can create variables of type string, int ou byte array. 
            HttpContext.Session.SetString("StringValue", "Text variable value");
            HttpContext.Session.SetInt32("IntegerValue", 100);
            // a session cookie '.AspNetCore.Session' will be automatically created and sended to the client

            return RedirectToAction("Index");
        }

        public IActionResult DeleteSessionVariables()
        {
            // delete all variables stored in session
            // this does not ends the session. For that it is necessary to delete the cookie
            foreach (var item in HttpContext.Session.Keys)
            {
                HttpContext.Session.Remove(item);
            }

            return RedirectToAction("Index");
        }

        public IActionResult DeleteSession()
        {
            // this really delete all session variables because it ends de session itself
            HttpContext.Response.Cookies.Delete(".AspNetCore.Session");
            // this is the default name (see service configuration)

            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}