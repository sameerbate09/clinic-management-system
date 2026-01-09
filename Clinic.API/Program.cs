using Clinic.Infrastructure.Persistence;
using Clinic.Application.Services;
using Clinic.Infrastructure.Repositories;
using Clinic.Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ClinicDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// Register repositories and services
builder.Services.AddScoped<IPatientRepository, Clinic.Infrastructure.Repositories.PatientRepository>();
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<IVisitService, VisitService>();
builder.Services.AddScoped<IVisitRepository, Clinic.Infrastructure.Repositories.VisitRepository>();
builder.Services.AddScoped<ITherapyRepository, Clinic.Infrastructure.Repositories.TherapyRepository>();
builder.Services.AddScoped<ITherapyService, TherapyService>();
builder.Services.AddScoped<IMedicineRepository, Clinic.Infrastructure.Repositories.MedicineRepository>();
builder.Services.AddScoped<IMedicineService, MedicineService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
