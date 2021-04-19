using InternshipMvc.Data;
using InternshipMvc.Hubs;
using InternshipMvc.Models;
using Microsoft.Extensions.Configuration;
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
        private IConfiguration configuration;
        private Location defaultLocation;

        public InternshipDbService(InternDbContext db, IConfiguration configuration)
        {
            this.db = db;
            this.subscribers = new List<IAddMemberSubscriber>();
            this.configuration = configuration;
        }
        public Intern AddMember(Intern member)
        {
            if(member.Location == null)
            {
                member.Location = GetDefaultLocation();
            }
            db.Interns.AddRange(member);
            db.SaveChanges();
            //subscribers.ForEach(subscriber => subscriber.OnAddMember(member));
            return member;
        }

        private Location GetDefaultLocation()
        {
            if (defaultLocation == null)
            {
                var defaultLocationName = configuration["DefaultLocation"];
                defaultLocation = db.Locations.Where(_ => _.Name == defaultLocationName).OrderBy(_ => _.Id).FirstOrDefault();
            }

            return defaultLocation;
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
            var intern = db.Find<Intern>(id);
            db.Entry(intern).Reference(_ => _.Location).Load();
            return intern;
            //return db.Find<Intern>(id);
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
