using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Policies
{
    public class IDGreaterthan27Requirment : IAuthorizationRequirement
    {
       public int ID  { get; set; }

        public IDGreaterthan27Requirment(int id)
        {
            ID = id;
        }

    }
}
