using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using InternshipMvc.Data;
using InternshipMVC.Models;
using InternshipMVC.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace InternMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly InternshipService intershipService;
        private readonly InternDbContext db;

        public HomeController(ILogger<HomeController> logger, InternshipService intershipService, InternDbContext db)
        {
            _logger = logger;
            this.intershipService = intershipService;
            this.db = db;
        }

        public IActionResult Index()
        {
            var interns = db.Interns;
            return View(interns);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Internship()
        {
            return View(intershipService.GetClass());
        }

        public IActionResult Weather()
        {
            return View();
        }

        [HttpDelete]
        public void RemoveMember(int index)
        {
            intershipService.RemoveMember(index);
        }

        [HttpGet]
        public string AddMember(string member)
        {
            return intershipService.AddMember(member);
        }

        [HttpPut]
        public void EditMember(int index, string name)
        {
            intershipService.EditMember(index, name);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
