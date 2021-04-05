using System.Collections.Generic;
using InternshipMvc.Models;

namespace InternshipMvc.Services
{
    public interface IInternshipService
    {
        Intern AddMember(Intern member);
        void EditMember(Intern intern);
        IList<Intern> GetMembers();
        void RemoveMember(int index);
    }
}