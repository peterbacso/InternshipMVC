﻿using InternshipMvc.Hubs;
using InternshipMvc.Models;
using InternshipMvc.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InternshipMvc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InternshipController : ControllerBase
    {
        private readonly IInternshipService internshipService;
        private readonly IHubContext<MessageHub> hubContext;

        public InternshipController(IInternshipService internshipService, IHubContext<MessageHub> hubContext)
        {
            this.internshipService = internshipService;
            this.hubContext = hubContext;
        }

        // GET: api/<InternshipController>
        [HttpGet]
        public IEnumerable<Intern> Get()
        {
            return internshipService.GetMembers();
        }

        // GET api/<InternshipController>/5
        [HttpGet("{id}")]
        public Intern Get(int id)
        {
            return internshipService.GetMemberById(id);
        }

        // POST api/<InternshipController>
        [HttpPost]
        public void Post([FromBody] Intern intern)
        {
            intern.RegistrationDateTime = DateTime.Now;
            var newMember = internshipService.AddMember(intern);
            hubContext.Clients.All.SendAsync("AddMember", newMember.Name, newMember.Id);
        }

        // PUT api/<InternshipController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Intern intern)
        {
            intern.Id = id;
            internshipService.EditMember(intern);
        }

        // DELETE api/<InternshipController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            hubContext.Clients.All.SendAsync("DeleteMember", id);
            internshipService.RemoveMember(id);
        }
    }
}
