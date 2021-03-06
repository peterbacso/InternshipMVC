using System;
using System.Threading.Tasks;
using InternshipMvc.Models;
using InternshipMvc.Services;
using Microsoft.AspNetCore.SignalR;

namespace InternshipMvc.Hubs
{
    public class MessageHub : Hub, IAddMemberSubscriber
    {
        private readonly MessageService messageService;

        public MessageHub(MessageService messageService)
        {
            this.messageService = messageService;
        }

        public async void OnAddMember(Intern member)
        {
            await Clients.All.SendAsync("AddMember", member.Name, member.Id);
        }

        public async Task SendMessage(string user, string message)
        {
            messageService.AddMessage(user, message);
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}