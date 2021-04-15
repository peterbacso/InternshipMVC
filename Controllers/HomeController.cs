using System;
using System.Diagnostics;
using System.Linq;
using InternshipMvc.Data;
using InternshipMvc.Hubs;
using InternshipMvc.Models;
using InternshipMVC.Models;
using InternshipMvc.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace InternshipMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IInternshipService internshipService;
        private readonly IHubContext<MessageHub> hubContext;
        private readonly MessageService messageService;

        public HomeController(ILogger<HomeController> logger, IInternshipService internshipService, IHubContext<MessageHub> hubContext, MessageService messageService)
        {
            _logger = logger;
            this.internshipService = internshipService;
            this.hubContext = hubContext;
            this.messageService = messageService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Chat()
        {
            return View(messageService.GetAllMessages());
        }

        public IActionResult Internship()
        {
            return View(internshipService.GetMembers());
        }

        public IActionResult Weather()
        {
            return View(internshipService.GetMembers());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
