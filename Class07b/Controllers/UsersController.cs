using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Class07b.Data;
using Class07b.Models;

namespace Class07b.Controllers
{
    public class UsersController : Controller
    {
        private readonly Class07bContext _context;

        public UsersController(Class07bContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (ModelState.IsValid)
            {
                User u = _context.User.SingleOrDefault(u => u.Username == username && u.Password == password);
                if (u == null)
                    ModelState.AddModelError("username", "username or password are wrong");
                else
                {
                    // the user is authenticated
                    // the session variable "user" is created to recover the user identify at each request
                    HttpContext.Session.SetString("user", username);

                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Response.Cookies.Delete(".Class07b.Session");

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Preferences()
        {
            ViewBag.mode = HttpContext.Request.Cookies["viewMode"] ?? "light";

            return View();
        }

        [HttpPost]
        public IActionResult Preferences(string mode)
        {

            HttpContext.Response.Cookies.Append("viewMode", mode, new CookieOptions { Expires = DateTime.Now.AddYears(1) });
            return RedirectToAction("Index", "Home");
        }

        #region HOMEWORK

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User newUser)
        {
            if (ModelState.IsValid)
            {
                if (_context.User.Any(u => u.Username == newUser.Username))
                    ModelState.AddModelError("username", "Username already exists. Please define another.");
                else
                {
                    _context.Add(newUser);
                    _context.SaveChanges();

                    HttpContext.Session.SetString("user", newUser.Username); // After successful registration, automatic login is made
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }

        #endregion HOMEWORK
    }

}
