using eAppointmentServer.Domain.Entities;
using GenericRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace eAppointmentServer.Infrastructure.Context
{
	internal sealed class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, Guid, IdentityUserClaim<Guid>,AppUserRole,IdentityUserLogin<Guid>,IdentityRoleClaim<Guid>, IdentityUserToken<Guid>> , IUnitOfWork
	{
        public ApplicationDbContext(DbContextOptions options) : base(options) //base db contexte gönderir
        {
             //options kısmını di'de dolduracağız
        }

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Ignore<IdentityUserClaim< Guid >>();
            builder.Ignore<IdentityRoleClaim< Guid >>();
            builder.Ignore<IdentityUserLogin< Guid >>();
            builder.Ignore<IdentityUserToken< Guid >>();

            //builder.Entity<Doctor>().Property(p => p.FirstName).HasColumnType("varchar(50)");
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
