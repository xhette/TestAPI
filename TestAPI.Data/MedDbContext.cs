using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TestAPI.Data.Entities;

namespace TestAPI.Data
{
    public class MedDbContext: DbContext
    {
        private readonly string _connectionString;

        public MedDbContext(DbContextOptions<MedDbContext> options):base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cabinet>(entity =>
            {
                entity.HasIndex(entity => entity.Id).IsUnique();
                entity.Property(entity => entity.Id).ValueGeneratedOnAdd();
                entity.Property(entity => entity.CabinetNumber).IsRequired();
            });
            modelBuilder.Entity<Region>(entity =>
            {
                entity.HasIndex(entity => entity.Id).IsUnique();
                entity.Property(entity => entity.RegionNumber).IsRequired();
            });
            modelBuilder.Entity<Specialty>(entity =>
            {
                entity.HasIndex(entity => entity.Id).IsUnique();
                entity.Property(entity => entity.Id).ValueGeneratedOnAdd();
                entity.Property(entity => entity.SpecialtyName)
                .IsRequired()
                .HasMaxLength(250);
            });
            modelBuilder.Entity<Patient>(entity =>
            {
                entity.HasIndex(entity => entity.Id).IsUnique();
                entity.Property(entity => entity.Id).ValueGeneratedOnAdd();
                entity.Property(entity => entity.Surname)
                .IsRequired()
                .HasMaxLength(50);
                entity.Property(entity => entity.Name)
                .IsRequired()
                .HasMaxLength(50);
                entity.Property(entity => entity.Patronymic)
                .IsRequired()
                .HasMaxLength(50);
                entity.Property(entity => entity.Address)
                .IsRequired()
                .HasMaxLength(150);
                entity.Property(entity => entity.Gender)
                .IsRequired()
                .HasMaxLength(15);
                entity.Property(entity => entity.Birthday)
                .IsRequired();

                entity.HasOne(entity => entity.Region)
                .WithMany(entity => entity.Patients)
                .HasForeignKey(entity => entity.RegionId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            });
            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.HasIndex(entity => entity.Id).IsUnique();
                entity.Property(entity => entity.Id).ValueGeneratedOnAdd();
                entity.Property(entity => entity.Surname)
                .IsRequired()
                .HasMaxLength(50);
                entity.Property(entity => entity.Name)
                .IsRequired()
                .HasMaxLength(50);
                entity.Property(entity => entity.Patronymic)
                .IsRequired();

                entity.HasOne(entity => entity.Region)
                .WithMany(entity => entity.Doctors)
                .HasForeignKey(entity => entity.RegionId)
                .OnDelete(DeleteBehavior.ClientSetNull);
                entity.HasOne(entity => entity.Specialty)
                .WithMany(entity => entity.Doctors)
                .HasForeignKey(entity => entity.SpecialityId)
                .OnDelete(DeleteBehavior.ClientSetNull);
                entity.HasOne(entity => entity.Cabinet)
                .WithMany(entity => entity.Doctors)
                .HasForeignKey(entity => entity.CabinetId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            });
        }

        public DbSet<Cabinet> Cabinets { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Specialty> Specialties { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
    }
}
