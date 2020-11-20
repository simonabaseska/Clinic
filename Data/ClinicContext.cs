using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Clinic.Models;

namespace Clinic.Data
{
    public class ClinicContext : DbContext
    {
        public ClinicContext (DbContextOptions<ClinicContext> options)
            : base(options)
        {
        }

        public DbSet<Doctors> Doctors { get; set; }

        public DbSet<Patients> Patients { get; set; }

        public DbSet<Terms> Terms { get; set; }

        public DbSet<Scheduling> Scheduling { get; set; }
        public DbSet<PatientTerm> PatientTerm { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Scheduling>()
                .HasOne<Patients>(p => p.Patient)
                .WithMany(p => p.Terms)
                .HasForeignKey(p => p.PatientID);


            modelBuilder.Entity<Scheduling>()
                .HasOne<Terms>(d => d.Term)
                .WithMany(d => d.Patients)
                .HasForeignKey(d => d.TermID);

            modelBuilder.Entity<Terms>()
                .HasOne<Doctors>(p => p.Doctor)
                .WithMany(p => p.Doctor)
                .HasForeignKey(p => p.DoctorId);
        }

    }
}
