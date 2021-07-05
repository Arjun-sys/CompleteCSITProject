using LoginRegistration.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginRegistration
{
    public partial class DatabaseContext:DbContext
    {
        public DatabaseContext() { }
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

            public virtual DbSet<Invoices> Invoices { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {

                modelBuilder.Entity<Invoices>(entity =>
                {
                    entity.Property(x => x.Date).HasColumnType("datetime");
                    entity.Property(x => x.Value).HasColumnType("decimal(18,2)");
                });
                OnModelCreatingPartial(modelBuilder);
            }
            partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

            //add all the tables from the database like student table

        }
    }

