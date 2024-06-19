using Microsoft.EntityFrameworkCore;
using Project.Repository.Entities;
using Project.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DataContext
{
    public class DataContext:DbContext,IContext
    {
        public DbSet<Item> Items { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<User> Users { get; set; }
        public async Task Save()
        {
            await SaveChangesAsync();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {//.\\SQLEXPRESS
            
            optionsBuilder.UseSqlServer("Server=seminar-sql;Database=FINISH_PROJECT_SEMINAR;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
    }
}
