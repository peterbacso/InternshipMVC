using InternshipMvc.Data;
using InternshipMvc.Models;
using System.Collections.Generic;
using System.Linq;
using InternshipMvc.Hubs;

namespace InternshipMvc.Services
{
    public class InternshipService : IInternshipService
    {
        private readonly InternshipClass _internshipClass = new();

        public void RemoveMember(int index)
        {
            _internshipClass.Members.RemoveAt(index);
        }

        public Intern AddMember(Intern member)
        {
            _internshipClass.Members.Add(member);
            return member;
        }

        public void EditMember(Intern intern)
        {
            _internshipClass.Members[intern.Id] = intern;
        }

        public IList<Intern> GetMembers()
        {
            return _internshipClass.Members;
        }

        public void SubscribeToAddMember(IAddMemberSubscriber subscribers)
        {
            throw new System.NotImplementedException();
        }

        public Intern GetMemberById(int id)
        {
            var member = _internshipClass.Members.Single(_ => _.Id == id);
            return member;
        }

        public void UpdateLocation(int id, int locationId)
        {
            throw new System.NotImplementedException();
        }
    }
}