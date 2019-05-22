using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SignalRDemo.Data;

namespace SignalRDemo.Web.Controllers
{
    public class AccountController : Controller
    {
        private string _connectionString;
        public AccountController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }

        public IActionResult SignUp()
        {
            return View();
        }

        public IActionResult LogIn()
        {
            return View();
        }
       
        [HttpPost]
        public IActionResult Signup(User user)
        {
            UserRepository repo = new UserRepository(_connectionString);
            repo.AddUser(user);
            return Redirect("/home/index");
        }

        [HttpPost]
        public IActionResult LogIn(string email, string password)
        {
            UserRepository repo = new UserRepository(_connectionString);
            User user = repo.Verify(email, password);
            if (user == null)
            {
                return Redirect("/home/index");
            }
            var claims = new List<Claim> { new Claim("user", email) };

            HttpContext.SignInAsync(new ClaimsPrincipal(new ClaimsIdentity(claims, "Cookies", "user", "role"))).Wait();

            return Redirect("/home/index");
        }


        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync().Wait();

            return Redirect("/home/index");
        }
    }
}