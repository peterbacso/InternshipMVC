using InternshipMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternshipMvc.Services
{
    public interface IAddMemberSubscriber
    {
        void OnAddMember(Intern member);
    }
}
