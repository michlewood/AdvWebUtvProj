using AdvWebUtvProj.Data.Entities;
using AdvWebUtvProj.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvWebUtvProj.Data.Entities
{
    public partial class ApplicationDbContext : IdentityDbContext<User>
    {
        public DbSet<Thing> Things { get; set; }

        public List<UserVM> GetAllUsers
        {
            get
            {
                List<UserVM> users = new List<UserVM>();
                foreach (var user in Users)
                {
                    users.Add(new UserVM(user.Email, user.UserName));
                }
                return users;
            }
        }
    }
}