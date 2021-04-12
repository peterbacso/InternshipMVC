using System.Collections.Generic;
using InternshipMvc.Hubs;
using InternshipMvc.Models;

namespace InternshipMvc.Services
{
    public interface IInternshipService
    {
        Intern AddMember(Intern member);
        void EditMember(Intern intern);
        IList<Intern> GetMembers();
        void RemoveMember(int index);
        void SubscribeToAddMember(IAddMemberSubscriber subscribers);
    }
}