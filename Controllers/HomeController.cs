using System.Diagnostics;
using InternshipMvc.Data;
using InternshipMVC.Models;
using InternshipMvc.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using InternshipMvc.Models;
using System;
using System.Linq;

namespace InternMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IInternshipService internshipService;
        private readonly InternDbContext db;

        public HomeController(ILogger<HomeController> logger, IInternshipService internshipService, InternDbContext db)
        {
            _logger = logger;
            this.internshipService = internshipService;
            this.db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Internship()
        {
            var interns = db.Interns.ToList();
            return View(interns);
            //return View(internshipService.GetMembers());
        }

        public IActionResult Weather()
        {
            return View(internshipService.GetMembers());
        }

        [HttpDelete]
        public void RemoveMember(int index)
        {
            internshipService.RemoveMember(index);
        }

        [HttpGet]
        public Intern AddMember(string memberName)
        {
            Intern intern = new Intern();
            intern.Name = memberName;
            intern.RegistrationDateTime = DateTime.Now;
            return internshipService.AddMember(intern);
        }

        [HttpPut]
        public void EditMember(int index, string name)
        {
            Intern intern = new Intern();
            intern.Id = index;
            intern.Name = name;
            internshipService.EditMember(intern);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
