using System;
using System.Collections.Generic;
using System.Text;
using Fuddit.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Fuddit.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<ApplicationUser> _users { get; set; }
        public DbSet<Votes> Votes { get; set; }
    }
}
