using DefaultCorsPolicyNugetPackage;
using eAppointmentServer.Application;
using eAppointmentServer.Domain.Entities;
using eAppointmentServer.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDefaultCors();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using(var scoped = app.Services.CreateScope())
{
	var userManager = scoped.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
	if (!userManager.Users.Any())
	{
		userManager.CreateAsync(new()
		{
			FirstName = "Deneme",
			LastName = "User",
			Email = "admin@admin.com",
			UserName = "admin",

		}, "123Aa*!").Wait();
	}
}

app.Run();
