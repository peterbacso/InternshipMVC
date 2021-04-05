using System.Diagnostics;
using InternshipMvc.Data;
using InternshipMvc.Services;
using InternshipMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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
            var interns = db.Interns;
            return View(interns);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Internship()
        {
            return View(internshipService.GetMembers());
        }

        public IActionResult Weather()
        {
            return View();
        }

        [HttpDelete]
        public void RemoveMember(int id)
        {
            internshipService.RemoveMember(id);
        }

        [HttpGet]
        public int AddMember(string memberName)
        {
            return internshipService.AddMember(memberName);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
