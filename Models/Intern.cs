using System;
using System.Text.Json.Serialization;

namespace InternshipMvc.Models
{
    public class Intern
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime RegistrationDateTime { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Location Location { get; set; }
    }
}
