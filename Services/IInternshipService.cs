using System.Collections.Generic;
using InternshipMvc.Models;
using InternshipMvc.WebAPI;

namespace InternshipMvc.Services
{
    public interface IInternshipService
    {
        int AddMember(string memberName);

        IList<Intern> GetMembers();

        void RemoveMember(int id);
    }
}