using System;
using System.Collections.Generic;
using System.Linq;
using InternshipMvc.Models;
using InternshipMVC.Models;

namespace InternshipMvc.Services
{
    public class InternshipService : IInternshipService
    {
        private readonly InternshipClass _internshipClass = new();

        public void RemoveMember(int index)
        {
            _internshipClass.Members.RemoveAt(index);
        }

        public int AddMember(string memberName)
        {
            var maxId = _internshipClass.Members.Max(_ => _.Id);
            var newId = maxId + 1;

            var intern = new Intern()
            {
                Id = maxId + 1,
                Name = memberName,
                RegistrationDateTime = DateTime.Now,
            };

            _internshipClass.Members.Add(intern);
            return newId;
        }

        public IList<Intern> GetMembers()
        {
            return _internshipClass.Members;
        }
    }
}