using InternshipMvc.Data;
using InternshipMVC.Models;

namespace InternshipMvc.Services
{
    public class InternshipService
    {
        private readonly InternshipClass _internshipClass = new ();
        private ApplicationDbContext db;

        public InternshipService(ApplicationDbContext db)
        {
            this.db = db;
        }

        //public List<Intern> GetAllInterns()
        //{
        //    db.Find
        //}

        public void RemoveMember(int index)
        {
            _internshipClass.Members.RemoveAt(index);
        }

        public string AddMember(string member)
        {
            _internshipClass.Members.Add(member);
            return member;
        }

        public InternshipClass GetClass()
        {
            return _internshipClass;
        }
    }
}
