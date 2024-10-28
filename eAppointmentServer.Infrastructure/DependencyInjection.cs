﻿using eAppointmentServer.Application.Services;
using eAppointmentServer.Domain.Entities;
using eAppointmentServer.Domain.Repositories;
using eAppointmentServer.Infrastructure.Context;
using eAppointmentServer.Infrastructure.Repositories;
using eAppointmentServer.Infrastructure.Services;
using GenericRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eAppointmentServer.Infrastructure
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<ApplicationDbContext>(options =>
			{
				options.UseSqlServer(configuration.GetConnectionString("SqlServer"));
			});

			services.AddIdentity<AppUser, AppRole>(action =>
			{
				action.Password.RequiredLength = 1;
				action.Password.RequireUppercase = false;
				action.Password.RequireLowercase = false;
				action.Password.RequireDigit = false;
			}).AddEntityFrameworkStores<ApplicationDbContext>();

			services.AddScoped<IUnitOfWork>(srv => srv.GetRequiredService<ApplicationDbContext>());

			//services.AddScoped<IAppointmentRepository, AppointmentRepository>();
			//services.AddScoped<IDoctorRepository, DoctorRepository>();
			//services.AddScoped<IPatientRepository, PatientRepository>();

			services.Scan(action =>
			{
				action
				.FromAssemblies(typeof(DependencyInjection).Assembly)
				.AddClasses(publicOnly: false) //internal ve private olanlara da di gerektiği
				.AddClasses(publicOnly: false)
				.UsingRegistrationStrategy(registrationStrategy: RegistrationStrategy.Skip)
				.AsImplementedInterfaces()
				.WithScopedLifetime();
			}); 

			services.AddScoped<IJwtProvider, JwtProvider>();
			return services;
		}
	}
}