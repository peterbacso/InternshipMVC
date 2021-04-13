using InternshipMvc.Data;
using InternshipMvc.Hubs;
using InternshipMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternshipMvc.Services
{
    public class InternshipDbService : IInternshipService
    {
        private readonly InternDbContext db;
        private readonly List<IAddMemberSubscriber> subscribers;

        public InternshipDbService(InternDbContext db)
        {
            this.db = db;
            this.subscribers = new List<IAddMemberSubscriber>();
        }
        public Intern AddMember(Intern member)
        {
            db.Interns.AddRange(member);
            db.SaveChanges();
            subscribers.ForEach(subscriber => subscriber.OnAddMember(member));
            return member;
        }

        public void EditMember(Intern intern)
        {
            var itemToBeUpdated = GetMemberById(intern.Id);
            itemToBeUpdated.Name = intern.Name;
            itemToBeUpdated.RegistrationDateTime = DateTime.Now;
            db.Interns.Update(itemToBeUpdated);
            db.SaveChanges();
        }

        public Intern GetMemberById(int id)
        {
            return db.Find<Intern>(id);
        }

        public IList<Intern> GetMembers()
        {
            var interns = db.Interns.ToList();
            return interns;
        }

        public void RemoveMember(int id)
        {
            var intern = GetMemberById(id);
            db.Remove<Intern>(intern);
            db.SaveChanges();
        }

        public void SubscribeToAddMember(IAddMemberSubscriber subscriber)
        {
            subscribers.Add(subscriber);
        }
    }
}
