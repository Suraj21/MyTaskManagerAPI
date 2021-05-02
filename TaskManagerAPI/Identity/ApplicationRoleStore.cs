using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManagerAPI.Identity
{
    public class ApplicationRoleStore:RoleStore<ApplicationRole,ApplicationDbContext>
    {
        public ApplicationRoleStore(ApplicationDbContext applicationDbContext, IdentityErrorDescriber identityErrorDescriber):base (applicationDbContext, identityErrorDescriber)
        {

        }
    }
}
