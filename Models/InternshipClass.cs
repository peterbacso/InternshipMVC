using InternshipMvc.Models;
using System;
using System.Collections.Generic;

namespace InternshipMVC.Models
{
    public class InternshipClass
    {
        private List<Intern> _members;

        public InternshipClass()
        {
            _members = new List<Intern>
            {
                new Intern { Name = "Borys", RegistrationDateTime = DateTime.Parse("2021-04-01"), Id = 1 },
                new Intern { Name = "Liova", RegistrationDateTime = DateTime.Parse("2021-04-01"), Id = 2 },
                new Intern { Name = "Orest", RegistrationDateTime = DateTime.Parse("2021-03-31"), Id = 3 },
            };
        }

        public IList<Intern> Members
        {
            get { return _members; }
        }
    }
}
