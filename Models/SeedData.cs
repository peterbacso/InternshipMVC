using InternshipMvc.Data;
using InternshipMvc.Models;
using System;
using System.Linq;

namespace InternshipMvc.Data
{
    public static class SeedData
    {
        public static void Initialize(InternDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Interns.Any())
            {
                return;   // DB has been seeded
            }

            var interns = new Intern[]
            {
                new Intern { Name = "Borys", RegistrationDateTime = DateTime.Parse("2021-04-01") },
                new Intern { Name = "Liova", RegistrationDateTime = DateTime.Parse("2021-04-01") },
                new Intern { Name = "Orest", RegistrationDateTime = DateTime.Parse("2021-03-31") },
            };

            context.Interns.AddRange(interns);
            context.SaveChanges();
        }
    }
}
