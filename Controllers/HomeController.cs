using System.Diagnostics;
using InternshipMvc.Data;
using InternshipMVC.Models;
using InternshipMvc.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using InternshipMvc.Models;
using System;
using System.Linq;
using Microsoft.AspNetCore.SignalR;
using InternshipMvc.Hubs;

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

        [HttpDelete]
        public void RemoveMember(int index)
        {
            var internsList = internshipService.GetMembers();
            Intern intern = internsList.FirstOrDefault(intern => intern.Id == index);

            if (intern == null)
            {
                return;
            }

            internshipService.RemoveMember(intern.Id);
        }

        [HttpGet]
        public Intern AddMember(string memberName)
        {
            Intern intern = new Intern
            {
                Name = memberName,
                RegistrationDateTime = DateTime.Now,
            };
            var newMember = internshipService.AddMember(intern);
            hubContext.Clients.All.SendAsync("AddMember", newMember.Name, newMember.Id);
            return newMember;
        }

        [HttpPut]
        public void EditMember(int index, string name)
        {
            var internsList = internshipService.GetMembers();
            Intern intern = internsList.FirstOrDefault(intern => intern.Id == index);
            if (intern == null)
            {
                return;
            }

            intern.Name = name;
            intern.RegistrationDateTime = DateTime.Now;
            internshipService.EditMember(intern);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
