using System;

namespace InternshipMvc.Models
{
    public class Intern
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime RegistrationDateTime { get; set; }

        public Location Location { get; set; }
    }
}
