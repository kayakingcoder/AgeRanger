using AgeRanger.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;

namespace AgeRanger.Data
{
    public class AgeRangerContext : DbContext
    {

        public virtual DbSet<AgeGroup> AgeGroup { get; set; }
        public virtual DbSet<Person> Person { get; set; }

        public AgeRangerContext(DbContextOptions<AgeRangerContext> options)
        : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var projectFolder = Directory.GetCurrentDirectory();
                string connString = string.Format("Filename={0}\\AgeRanger.db", projectFolder);
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlite(connString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AgeGroup>(entity =>
            {
                entity.HasIndex(e => e.Id)
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description).IsRequired();
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasIndex(e => e.Id)
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();
            });
        }
    }
}
