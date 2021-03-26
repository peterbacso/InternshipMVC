using System.Collections.Generic;

namespace InternshipMVC.Models
{
    public class InternshipClass
    {
        private List<string> _members;

        public InternshipClass()
        {
            _members = new List<string>
            {
                "Peter",
                "Radu",
                "Fabi",
            };
        }

        public IList<string> Members
        {
            get { return _members; }
        }
    }
}
