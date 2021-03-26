using InternshipMVC.Models;
using System.Linq;

namespace InternshipMVC.Services
{
    public class InternshipService
    {
        private InternshipClass _internshipClass = new();

        public void RemoveMember(int index)
        {
            _internshipClass.Members.RemoveAt(index);
        }

        public string AddMember(string member)
        {
            _internshipClass.Members.Add(member);
            return member;
        }

        public void EditMember(int index, string name)
        {
            _internshipClass.Members[index] = name;
        }

        public InternshipClass GetClass()
        {
            return _internshipClass;
        }
    }
}
