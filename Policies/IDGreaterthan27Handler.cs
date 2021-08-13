using Auth.Repositeries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Auth.Policies
{
    public class IDGreaterthan27Handler : AuthorizationHandler<IDGreaterthan27Requirment>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IDGreaterthan27Requirment requirement)
        {
            var userid = context.User.HasClaim(v => v.Type == "id");
            var idValue = int.Parse(context.User.FindFirst(x => x.Type == "id").Value);
            if (!userid)
            {
                return Task.CompletedTask;
            }
            //var f = idValues.Where(a => int.Parse(a.Value) > requirement.ID).ToList();
            //List<int> flist = new List<int>();
            //foreach(var i in f)
            //{
            //    int v =  int.Parse(i.Value);
            //    flist.Add(v);
            //}
            if(idValue >= requirement.ID)
            {

            context.Succeed(requirement);
            }
            else
            {
                return Task.CompletedTask;
            }
            
            
          return  Task.FromResult(0);

        }
    }
}
