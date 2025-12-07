using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using HTask.Domain.Entities;

namespace HTask.Infrastructure.Data
{
    public class HTaskDbContext : DbContext
    {
        public HTaskDbContext(DbContextOptions options) : base(options)
        {
        }

        // DbSet para la entidad TaskItem
        public DbSet<HTaskItem> Tasks { get; set; }
    }
}
