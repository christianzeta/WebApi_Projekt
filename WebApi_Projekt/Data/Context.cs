using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi_Projekt.Models;

namespace WebApi_Projekt.Data
{
    public class Context : IdentityDbContext<MyUser>
    {
        public Context(DbContextOptions options) : base(options)
        {
        }

        public DbSet<GeoMessage> GeoMessages { get; set; } 
        public DbSet<MyUser> MyUsers { get; set; }
    }
}
