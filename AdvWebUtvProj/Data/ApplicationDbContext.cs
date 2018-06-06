﻿using AdvWebUtvProj.Data.Entities;
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
    }
}