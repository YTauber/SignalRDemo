using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SignalRDemo.Data;
using SignalRDemo.Web.Models;

namespace SignalRDemo.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private string _connectionString;
        public HomeController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }

        public IActionResult Index()
        {
            IndexViewModel vm = new IndexViewModel();
            UserRepository repoU = new UserRepository(_connectionString);
            JobRepository repoJ = new JobRepository(_connectionString);

            vm.CurrentUser = repoU.GetUserByEmail(User.Identity.Name);
            vm.Jobs = repoJ.GetJobs();
            return View(vm);
        }

    }
}
